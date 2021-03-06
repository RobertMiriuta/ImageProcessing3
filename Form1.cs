﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace INFOIBV

{
    public partial class INFOIBV : Form
    {
        private readonly Tuple<int, int>[] clockwiseRotation =
        {
            new Tuple<int, int>(-1, -1), new Tuple<int, int>(0, -1), new Tuple<int, int>(1, -1),
            new Tuple<int, int>(1, 0), new Tuple<int, int>(1, 1), new Tuple<int, int>(0, 1),
            new Tuple<int, int>(-1, 1), new Tuple<int, int>(-1, 0)
        };

        private readonly Tuple<int, int>[] counterClockwiseRotation =
        {
            new Tuple<int, int>(1, 0), new Tuple<int, int>(1, -1), new Tuple<int, int>(0, -1),
            new Tuple<int, int>(-1, -1), new Tuple<int, int>(-1, 0), new Tuple<int, int>(-1, 1),
            new Tuple<int, int>(0, 1), new Tuple<int, int>(1, 1)
        };

        private readonly int globalAccuracy = 600;

        private Bitmap InputImage;
        private Bitmap OutputImage;

        public INFOIBV()
        {
            InitializeComponent();
        }

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            histoIn.Series.Clear();
            if (openImageDialog.ShowDialog() == DialogResult.OK) // Open File Dialog
            {
                var file = openImageDialog.FileName; // Get the file name
                imageFileName.Text = file; // Show file name
                if (InputImage != null) InputImage.Dispose(); // Reset image
                InputImage = new Bitmap(file); // Create new Bitmap from file
                pictureBox1.Image = InputImage; // Display input image
                var result = calculateHistogramFromImage(InputImage);
                var rArray = result.Item1;
                var gArray = result.Item2;
                var bArray = result.Item3;

                var rSeries = histoIn.Series.Add("RedHistogram");
                rSeries.BorderWidth = 0;
                rSeries.Color = Color.IndianRed;
                var gSeries = histoIn.Series.Add("GreenHistogram");
                gSeries.BorderWidth = 0;
                gSeries.Color = Color.LightSeaGreen;
                var bSeries = histoIn.Series.Add("BlueHistogram");
                bSeries.BorderWidth = 0;
                bSeries.Color = Color.DeepSkyBlue;

                var max = 0;

                for (var i = 0; i < 256; i++)
                {
                    rSeries.Points.Add(new DataPoint(i, rArray[i]));
                    gSeries.Points.Add(new DataPoint(i, gArray[i]));
                    bSeries.Points.Add(new DataPoint(i, bArray[i]));
                    if (max < rArray[i])
                        max = rArray[i];
                    if (max < gArray[i])
                        max = gArray[i];
                    if (max < bArray[i])
                        max = bArray[i];
                }

                histoIn.ChartAreas[0].AxisX.Minimum = 0;
                histoIn.ChartAreas[0].AxisX.Maximum = 255;

                histoIn.ChartAreas[0].AxisY.Minimum = 0;
                histoIn.ChartAreas[0].AxisY.Maximum = max;
            }
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            if (InputImage == null) return; // Get out if no input image
            if (OutputImage != null) OutputImage.Dispose(); // Reset output image
            OutputImage = new Bitmap(InputImage.Size.Width, InputImage.Size.Height); // Create new output image
            var Image = new Color[InputImage.Size.Width,
                InputImage.Size.Height]; // Create array to speed-up operations (Bitmap functions are very slow)

            // Copy input Bitmap to array            
            for (var x = 0; x < InputImage.Size.Width; x++)
            for (var y = 0; y < InputImage.Size.Height; y++)
                Image[x, y] = InputImage.GetPixel(x, y); // Set pixel color in array at (x,y)

            // Setup progress bar
            progressBar.Visible = true;
            progressBar.Minimum = 1;
            progressBar.Maximum = InputImage.Size.Width * InputImage.Size.Height;
            progressBar.Value = 1;
            progressBar.Step = 1;

            //Reads the combobox to decide which conversion should be done on the input image.
            switch (comboBox1.Text)
            {
                case "pipeline":
                    //Here the magic happens
                    Image = applyPipeline(Image);
                    //Jk it's still trash
                    break;
                default:
                    Debugger.debug(1, "Nothing matched");
                    break;
            }


            // Copy array to output Bitmap
            if (Image == null) Image = makeBinaryImage();
            for (var x = 0; x < Image.GetLength(0); x++)
            for (var y = 0; y < Image.GetLength(1); y++)
                OutputImage.SetPixel(x, y, Image[x, y]); // Set the pixel color at coordinate (x,y)

            pictureBox2.Image = OutputImage; // Display output image

            histoOut.Series.Clear();
            var result = calculateHistogramFromImage(OutputImage); //Calculates histogram for the output image.
            var rArray = result.Item1;
            var gArray = result.Item2;
            var bArray = result.Item3;

            var rSeries = histoOut.Series.Add("RedHistogram");
            rSeries.Color = Color.IndianRed;
            var gSeries = histoOut.Series.Add("GreenHistogram");
            gSeries.Color = Color.LightSeaGreen;
            var bSeries = histoOut.Series.Add("BlueHistogram");
            bSeries.Color = Color.DeepSkyBlue;

            var max = 0;

            for (var i = 0; i < 256; i++)
            {
                rSeries.Points.Add(new DataPoint(i, rArray[i]));
                gSeries.Points.Add(new DataPoint(i, gArray[i]));
                bSeries.Points.Add(new DataPoint(i, bArray[i]));
                if (max < rArray[i])
                    max = rArray[i];
                if (max < gArray[i])
                    max = gArray[i];
                if (max < bArray[i])
                    max = bArray[i];
            }

            histoOut.ChartAreas[0].AxisX.Minimum = 0;
            histoOut.ChartAreas[0].AxisX.Maximum = 255;

            histoOut.ChartAreas[0].AxisY.Minimum = 0;
            histoOut.ChartAreas[0].AxisY.Maximum = histoIn.ChartAreas[0].AxisY.Maximum;

            progressBar.Visible = false; // Hide progress bar
        }

        //_______Main Functionality_________

        //Shows the progress picture
        private void progressPicture(Color[,] image)
        {
            Bitmap stum = new Bitmap(InputImage.Size.Width, InputImage.Size.Height);
            for (var x = 0; x < image.GetLength(0); x++)
            for (var y = 0; y < image.GetLength(1); y++)
                stum.SetPixel(x, y, image[x, y]); // Set the pixel color at coordinate (x,y)
            Debugger.debug(1, "Showing the intermediate output image");

            pictureBox2.Image = stum;
            pictureBox2.Update();
        }

        //Applies phase one
        private Color[,] applyPhaseOne(Color[,] image)
        {
            //Conversion to Grayscale
            image = conversionGrayscale(image);
            progressPicture(image);
            progressBar.Value = 1;
            //Conversion to specific threshold
            image = conversionPercentageThreshold(image);
            progressPicture(image);
            progressBar.Value = 1;

            //Remove background noise
            Color[,] compareImage = image.Clone() as Color[,]; //for geodesic dilation
            compareImage = conversionErosionBinary(compareImage, convertInputToTuplesBinary(false)); //opening the image
            compareImage =
                conversionDilationBinary(compareImage, convertInputToTuplesBinary(false)); //opening the image
            progressPicture(image);
            progressBar.Value = 1;

            //Part 2 of removing background noise
            image = conversionGeodesicDilation(image, true, compareImage, false);
            progressPicture(image);
            progressBar.Value = 1;
            //Return something nice
            return image;
        }

        //Applies phase two
        private Color[,] applyPhaseTwo(Color[,] image, Color[,] ogImage)
        {
            //Start Phase2
            int[,] labelShapeImages = labelShapes(image).Item1;
            List<Color[,]> subImages = extractSubImageFromLabeledShapes(labelShapeImages);
            List<List<int>> listOfChaincodes = calcChaincodeMany(labelShapeImages);

            //add area to image, to filter on later
            List<Tuple<double, Color[,]>> areas = new List<Tuple<double, Color[,]>>();
            for (int i = 0; i < listOfChaincodes.Count; i++)
            {
                double area = calcAreaFromChaincode(listOfChaincodes.ElementAt(i));
                areas.Add(new Tuple<double, Color[,]>(area, subImages.ElementAt(i)));
            }

            //If the area is above 10, which means there are more than 10 shapes, remove the smallest ones
            Debugger.debug(1, "Subimages here before filtering on amount of shapes " + areas.Count);
            if (areas.Count > 10)
            {
                areas.Sort((x, y) => y.Item1.CompareTo(x.Item1));
                int length = areas.Count;
                for (int x = 10; x < length; x++) areas.RemoveAt(10);
            }

            List<Color[,]> filteredImagesOnSize = new List<Color[,]>();
            foreach (var element in areas) filteredImagesOnSize.Add(element.Item2);
            Debugger.debug(1, "Subimages left after filtering on amount of shapes " + filteredImagesOnSize.Count);
            //Filter images on holes inside the image, if that is above the threshold, add image to the output.
            List<Color[,]> filteredSubImages = new List<Color[,]>();
            foreach (var element in filteredImagesOnSize)
                if (subImageHasAmountOfHolesInShape((Color[,]) element.Clone(), 4))
                    filteredSubImages.Add(element);
            Debugger.debug(1, "Subimages left after filtering on amount of holes " + filteredSubImages.Count);

            Color[,] outputImage = combineSubImages(filteredSubImages);
            return outputImage;
        }

        //Applies phase three
        private Color[,] applyPhaseThree(Color[,] image)
        {
            return image;
        }

        //Applies, in order, phase one -> two -> three
        private Color[,] applyPipeline(Color[,] image)
        {
            Color[,] ogImage = (Color[,]) image.Clone();
            image = applyPhaseOne(image);
            return applyPhaseTwo(image, ogImage);
        }

        //Operates on binary images, travers the shape looking for white pixels, then applies shape labelling
        //Shapes that are connected are stored in a tree data structure.
        //This functions runs really long during our pipeline
        private Tuple<int[,], int> labelShapes(Color[,] image)
        {
            int backgroundNumber = 0;
            int unlabeledNumber = 1;
            int currentLabelNumber = 2;
            int[,] shapes = new int[image.GetLength(0), image.GetLength(1)];
            //Initialize shapes array with 0 for background and 1 for foreground
            for (int x = 0; x < image.GetLength(0); x++)
            for (int y = 0; y < image.GetLength(1); y++)
                if (image[x, y] == Color.FromArgb(255, 255, 255))
                    shapes[x, y] = unlabeledNumber;

            UF connectionTree = new UF(500); //probably less than 100 shapes

            //top left to bottom right
            for (int y = 0; y < shapes.GetLength(1); y++)
            for (int x = 0; x < shapes.GetLength(0); x++)
                if (shapes[x, y] != backgroundNumber)
                    try
                    {
                        int[] topNeighborhood =
                            {shapes[x - 1, y], shapes[x - 1, y - 1], shapes[x, y - 1], shapes[x + 1, y - 1]};

                        int max = topNeighborhood.Max();
                        if (max == 0) //first pixel of shape
                        {
                            shapes[x, y] = currentLabelNumber++;
                        }
                        else //grow shape
                        {
                            shapes[x, y] = max;
                            List<int> unionLabels = getLabelFromNeighbourhood(topNeighborhood);
                            if (unionLabels.Count > 1 && shapes[x, y] > backgroundNumber)
                                foreach (var elem in unionLabels)
                                    connectionTree.merge(elem, topNeighborhood.Max());
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Debugger.debug(2, "Shape Labeling: An Index was out of Range");
                    }

            //resolve collisions
            for (int y = 0; y < shapes.GetLength(1); y++)
            for (int x = 0; x < shapes.GetLength(0); x++)
                shapes[x, y] = connectionTree.find(shapes[x, y]);
            return new Tuple<int[,], int>(shapes, currentLabelNumber - 1);
        }

        //Adds multiple sub images together
        private Color[,] combineSubImages(List<Color[,]> imageList)
        {
            Color[,] outputImage = makeBinaryImage();
            int colorStep = 25;

            Random rnd = new Random();
            foreach (var image in imageList)
            {
                int ranNum1 = rnd.Next(1, 255);
                int ranNum2 = rnd.Next(1, 255);
                int ranNum3 = rnd.Next(1, 255);
                for (int x = 0; x < image.GetLength(0); x++)
                for (int y = 0; y < image.GetLength(1); y++)
                    if (image[x, y].R != 0)
                        outputImage[x, y] = Color.FromArgb(ranNum1 * colorStep % 255,
                            ranNum2 * colorStep % 255, ranNum3 * colorStep % 255);
            }

            return outputImage;
        }

        //Not used, gives labels to hough shapes, so connected areas will be detected
        private int[,] houghLabelShapes(int[,] houghGraph)
        {
            int backgroundNumber = 0;
            int unlabeledNumber = 1;
            int currentLabelNumber = 2;
            int[,] shapes = new int[houghGraph.GetLength(0), houghGraph.GetLength(1)];
            //Initialize shapes array with 0 for background and 1 for foreground
            for (int x = 0; x < houghGraph.GetLength(0); x++)
            for (int y = 0; y < houghGraph.GetLength(1); y++)
                if (houghGraph[x, y] > 0)
                    shapes[x, y] = unlabeledNumber;
                else
                    shapes[x, y] = backgroundNumber;

            UF connectionTree = new UF(1000); //probably less than 100 shapes

            //top left to bottom right
            for (int y = 0; y < shapes.GetLength(1); y++)
            for (int x = 0; x < shapes.GetLength(0); x++)
                if (shapes[x, y] != backgroundNumber)
                    try
                    {
                        int[] topNeighborhood =
                            {shapes[x - 1, y], shapes[x - 1, y - 1], shapes[x, y - 1], shapes[x + 1, y - 1]};
                        if (topNeighborhood.Max() == 0) //first pixel of shape
                        {
                            shapes[x, y] = currentLabelNumber++;
                        }
                        else //grow shape
                        {
                            shapes[x, y] = topNeighborhood.Max();
                            List<int> unionLabels = getLabelFromNeighbourhood(topNeighborhood);
                            if (unionLabels.Count > 1 && shapes[x, y] > backgroundNumber)
                                foreach (var elem in unionLabels)
                                    connectionTree.merge(elem, topNeighborhood.Max());
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Debugger.debug(2, "Shape Labeliing: An Index was out of Range");
                    }

            //resolve collisions
            for (int y = 0; y < shapes.GetLength(1); y++)
            for (int x = 0; x < shapes.GetLength(0); x++)
                shapes[x, y] = connectionTree.find(shapes[x, y]);
            return shapes;
        }

        //Calculates the centroid of every connected shape
        private int[,] centroidOfLabeledHoughGraph(int[,] labeledhoughGraph)
        {
            List<int[,]> subHoughs = new List<int[,]>();
            int[,] outputHoughGraph = new int[labeledhoughGraph.GetLength(0), labeledhoughGraph.GetLength(1)];
            List<int> labels = getLabelsFromIntMatrix(labeledhoughGraph);

            foreach (var label in labels)
            {
                int[,] currImage = new int[labeledhoughGraph.GetLength(0), labeledhoughGraph.GetLength(1)];
                for (int x = 0; x < labeledhoughGraph.GetLength(0); x++)
                for (int y = 0; y < labeledhoughGraph.GetLength(1); y++)
                    if (labeledhoughGraph[x, y] == label)
                        currImage[x, y] = 255;
                subHoughs.Add(currImage);
            }

            foreach (var subHough in subHoughs)
            {
                Tuple<int, int> center = calcCentroidFromHoughMass(subHough);
                outputHoughGraph[center.Item1, center.Item2] = 255;
            }

            return outputHoughGraph;
        }

        //Calculates the centroid a single connected shape
        private Tuple<int, int> calcCentroidFromHoughMass(int[,] houghGraph)
        {
            int xCenter = 0;
            int yCenter = 0;
            int count = 0;
            for (int x = 0; x < houghGraph.GetLength(0); x++)
            for (int y = 0; y < houghGraph.GetLength(1); y++)
                if (houghGraph[x, y] == 255)
                {
                    xCenter += x;
                    yCenter += y;
                    count++;
                }

            if (count != 0)
            {
                xCenter = xCenter / count;
                yCenter = yCenter / count;
                return new Tuple<int, int>(xCenter, yCenter);
            }

            return new Tuple<int, int>(0, 0);
        }

        //Calculates the centroid a single connected shape
        private Tuple<int, int> calcCentroidFromShape(Color[,] binaryShape)
        {
            int xCenter = 0;
            int yCenter = 0;
            int count = 0;
            for (int x = 0; x < binaryShape.GetLength(0); x++)
            for (int y = 0; y < binaryShape.GetLength(1); y++)
                if (binaryShape[x, y] == Color.FromArgb(255, 255, 255))
                {
                    xCenter += x;
                    yCenter += y;
                    count++;
                }

            if (count != 0)
            {
                xCenter = xCenter / count;
                yCenter = yCenter / count;
                return new Tuple<int, int>(xCenter, yCenter);
            }

            return new Tuple<int, int>(0, 0);
        }

        //Looks at a subimage and if it contains a hole.
        private bool subImageHasHoleInShape(Color[,] image)
        {
            for (int x = 0; x < image.GetLength(0); x++)
            {
                bool insideShape = false;
                bool visitedShape = false;
                for (int y = 0; y < image.GetLength(1); y++)
                {
                    if (image[x, y].R == 255)
                    {
                        insideShape = true;
                        if (visitedShape) return true;
                    }

                    if (image[x, y].R == 0 && insideShape)
                    {
                        insideShape = false;
                        visitedShape = true;
                    }
                }
            }

            return false;
        }

        //Looks at subimage and if it contains more than an x amount of holes (and less than 25)
        private bool subImageHasAmountOfHolesInShape(Color[,] image, int amountWanted)
        {
            image = conversionComplement(image);
            Tuple<int[,], int> thing = labelShapes(image);
            int amountOfInnerHoles = extractSubImageFromLabeledShapes(thing.Item1).Count;
            Debugger.debug(2, "Amount of holes found = " + amountOfInnerHoles);
            if (amountOfInnerHoles >= amountWanted && amountOfInnerHoles < 25) return true;
            return false;
        }

        //Gets all the labels in the neighbourhood
        private List<int> getLabelFromNeighbourhood(int[] neighbourhood)
        {
            List<int> output = new List<int>();
            foreach (var element in neighbourhood)
                if (element != 0 && element != 1)
                    output.Add(element);
            return output;
        }

        //Returns individual images based on the labels that were given
        private List<Color[,]> extractSubImageFromLabeledShapes(int[,] labeledShapes)
        {
            List<Color[,]> subimages = new List<Color[,]>();
            List<int> labels = getLabelsFromIntMatrix(labeledShapes);

            foreach (var label in labels)
            {
                Color[,] currImage = makeBinaryImage(labeledShapes.GetLength(0), labeledShapes.GetLength(1));
                for (int x = 0; x < labeledShapes.GetLength(0); x++)
                for (int y = 0; y < labeledShapes.GetLength(1); y++)
                    if (labeledShapes[x, y] == label)
                        currImage[x, y] = Color.White;
                subimages.Add(currImage);
            }

            return subimages;
        }

        //Calculates the chaincode of multiple labeled shapes
        private List<List<int>> calcChaincodeMany(int[,] labeledShapes)
        {
            List<Color[,]> subimages = extractSubImageFromLabeledShapes(labeledShapes);
            List<List<int>> outputEncodings = new List<List<int>>();

            foreach (var image in subimages)
                outputEncodings.Add(calcRunLengthEnncodingSingleImage(image));

            return outputEncodings;
        }

        //Calculates the chaincode of a single image
        private List<int> calcRunLengthEnncodingSingleImage(Color[,] shape)
        {
            var startPoint = getStartPoint(shape);
            var startPointx = startPoint.Item1;
            var startPointy = startPoint.Item2;
            var listOfThings = getShapeCoordinates(shape, startPointx, startPointy);

            List<int> runLengthCode = new List<int>();
            for (var x = 0; x < listOfThings.Count - 1; x++)
            {
                var newPt = new Tuple<int, int>(listOfThings.ElementAt(x + 1).Item1 - listOfThings.ElementAt(x).Item1,
                    listOfThings.ElementAt(x + 1).Item2 - listOfThings.ElementAt(x).Item2);
                runLengthCode.Add(getIndexAtElem(newPt));
            }

            var lastPt = new Tuple<int, int>(
                listOfThings.ElementAt(0).Item1 - listOfThings.ElementAt(listOfThings.Count - 1).Item1,
                listOfThings.ElementAt(0).Item2 - listOfThings.ElementAt(listOfThings.Count - 1).Item2);
            runLengthCode.Add(getIndexAtElem(lastPt));

            return runLengthCode;
        }

        //Converts chaincode to differential chaincode so that rotation does not matter
        private List<int> convertToDifferentialEncoding(List<int> runLengthEncoding)
        {
            List<int> diffEncoding = new List<int>();
            for (int x = 0; x < runLengthEncoding.Count; x++)
                if (x == runLengthEncoding.Count)
                    diffEncoding.Add(runLengthEncoding.ElementAt(0) - runLengthEncoding.ElementAt(x));
                else
                    diffEncoding.Add(runLengthEncoding.ElementAt(x + 1) - runLengthEncoding.ElementAt(x));
            return diffEncoding;
        }

        //Calculates the perimeter based on the chaincode
        private double calcPerimeterFromChaincode(List<int> encoding)
        {
            double perimeter = 0.0;
            for (int x = 0; x < encoding.Count; x++)
                if (encoding.ElementAt(x) % 2 == 1)
                    perimeter += Math.Sqrt(2);
                else
                    perimeter += 1;
            Debugger.debug(2, "Perimeter: " + perimeter);
            return perimeter;
        }

        //approximate area, because chaincode used in slides is wrong, we think :) .
        private double calcAreaFromChaincode(List<int> encoding)
        {
            int area = 0;
            int ypos = 0;
            for (int x = 0; x < encoding.Count; x++)
                switch (encoding.ElementAt(x))
                {
                    case 0:
                        area -= ypos;
                        break;
                    case 1:
                        area -= --ypos;
                        break;
                    case 2:
                        ypos--;
                        break;
                    case 3:
                        area += --ypos;
                        break;
                    case 4:
                        area += ypos;
                        break;
                    case 5:
                        area += ++ypos;
                        break;
                    case 6:
                        ypos++;
                        break;
                    case 7:
                        area -= ++ypos;
                        break;
                    default:
                        Debugger.debug(2, "default");
                        break;
                }
            Debugger.debug(2, "Area: " + area);
            return area;
        }

        //Gets unique labels from an int matrix
        private List<int> getLabelsFromIntMatrix(int[,] labeledShapes)
        {
            List<int> labels = new List<int>();

            for (int x = 0; x < labeledShapes.GetLength(0); x++)
            for (int y = 0; y < labeledShapes.GetLength(1); y++)
            {
                int num = labeledShapes[x, y];
                if (!labels.Contains(num) && num != 0 && num != 1)
                    labels.Add(num);
            }

            return labels;
        }

        //Here automatic thresholding bernsen is implemented
        private Color[,] conversionThresholdBernsen(Color[,] image, int contrastThreshold)
        {
            int size = 3;
            int halfSize = (size - 1) / 2;
            for (int x = 0; x < InputImage.Size.Width; x++)
            for (int y = 0; y < InputImage.Size.Height; y++)
            {
                int[] pixelVector = new int[size * size - 1];
                int pixelVectorIndex = 0;
                for (int xFilter = -halfSize; xFilter <= halfSize; xFilter++)
                for (int yFilter = -halfSize; yFilter <= halfSize; yFilter++)
                    try
                    {
                        if (xFilter == 0 && yFilter == 0)
                        {
                            Debugger.debug(2, "Stepping over the center value");
                        }
                        else
                        {
                            Color filterColor = image[x - xFilter, y - yFilter];
                            Debugger.debug(2, "YOUR NEW COLOR " + filterColor.R);
                            pixelVector[pixelVectorIndex] = filterColor.R;
                            pixelVectorIndex++;
                            Debugger.debug(2, "YOUR NEW VECTOR INDEX " + pixelVectorIndex);
                        }
                    }
                    catch (IndexOutOfRangeException IOORE)
                    {
                        Debugger.debug(2, "Threshold bernsen was out of bounds, but that is okay " + pixelVectorIndex);
                        Debugger.debug(3, IOORE.Message);
                        pixelVector[pixelVectorIndex] = 0;
                        pixelVectorIndex++;
                    }

                int newColor;
                int max = pixelVector.Max();
                int min = pixelVector.Min();
                if (max - min < contrastThreshold)
                {
                    newColor = 0;
                }
                else
                {
                    int threshold = (min + max) / 2;
                    if (x + y % 100 == 0) Debugger.debug(2, "The selected threshold is: " + threshold);
                    int pixelColor = image[x, y].R;
                    newColor = pixelColor > threshold
                        ? 255
                        : 0; //Uses the red color to calculate the threshold, since all channels are the same.
                }

                Color updatedColor =
                    Color.FromArgb(newColor, newColor,
                        newColor); // Pixel is either 255 or 0, depending on the threshold.
                image[x, y] = updatedColor; // Set the new pixel color at coordinate (x,y)
                progressBar.PerformStep(); // Increment progress bar
            }

            return image;
        }

        private int getIndexAtElem(Tuple<int, int> coordinate)
        {
            if (counterClockwiseRotation.Contains(coordinate))
                for (int i = 0; i < counterClockwiseRotation.Length; i++)
                    if (counterClockwiseRotation.ElementAt(i).Equals(coordinate))
                        return i;
            return -1;
        }

        private Color[,] conversionPercentageThreshold(Color[,] image)
        {
            Tuple<int[], int[], int[]>
                histogram = calculateHistogramFromImage(
                    InputImage); //picking the green channel since image is greyscale
            double percentile = Convert.ToDouble(image.GetLength(0) + image.GetLength(1)) * 0.1;
            int[] onechannel = histogram.Item2;
            for (int i = 255; i > 0; i--)
                if (onechannel[i] > percentile)
                    return conversionThreshold(image, Convert.ToInt16(Convert.ToDouble(i) * 0.7));
            Debugger.debug(2, "Applying default threshold, no color with 20 values found");
            return conversionThreshold(image, 180); //default threshold
        }

        //Calculates the circularity based on a shape
        private double calcCircularity(double area, double perimeter)
        {
            return Math.PI * 4 * area / (perimeter * perimeter);
        }

        //Applies sobel edge detection, we changed it based on your earlier feedback. Hope it is good now haha.
        private Color[,] conversionEdgeDetection(Color[,] image)
        {
            int[,] sobelFilterX = {{-1, 0, 1}, {-2, 0, 2}, {-1, 0, 1}};
            int[,] sobelFilterY = {{-1, -2, -1}, {0, 0, 0}, {1, 2, 1}};
            int size = sobelFilterY.GetLength(0);
            int halfSize = (size - 1) / 2;
            double[,] imageSobelX = new double[InputImage.Size.Width, InputImage.Size.Height];
            for (int x = 0; x < InputImage.Size.Width; x++)
            for (int y = 0; y < InputImage.Size.Height; y++)
            {
                double newColor = 0.0;
                if (x < halfSize || y < halfSize || y >= InputImage.Size.Height - halfSize ||
                    x >= InputImage.Size.Width - halfSize)
                    newColor = 128.0;
                else
                    for (int xFilter = -halfSize; xFilter <= halfSize; xFilter++)
                    for (int yFilter = -halfSize; yFilter <= halfSize; yFilter++)
                    {
                        Color filterColor = image[x - xFilter, y - yFilter];
                        newColor += sobelFilterX[xFilter + halfSize, yFilter + halfSize] * filterColor.R;
                    }

                imageSobelX[x, y] = newColor; // Set the new pixel color at coordinate (x,y)
                progressBar.PerformStep(); // Increment progress bar
            }

            progressBar.Value = 1;
            double[,] imageSobelY = new double[InputImage.Size.Width, InputImage.Size.Height];
            for (int x = 0; x < InputImage.Size.Width; x++)
            for (int y = 0; y < InputImage.Size.Height; y++)
            {
                double newColor = 0.0;
                if (x < halfSize || y < halfSize || y >= InputImage.Size.Height - halfSize ||
                    x >= InputImage.Size.Width - halfSize)
                    newColor = 128.0;
                else
                    for (int xFilter = -halfSize; xFilter <= halfSize; xFilter++)
                    for (int yFilter = -halfSize; yFilter <= halfSize; yFilter++)
                    {
                        Color filterColor = image[x - xFilter, y - yFilter];
                        newColor += sobelFilterY[xFilter + halfSize, yFilter + halfSize] * filterColor.R;
                    }

                imageSobelY[x, y] = newColor; // Set the new pixel color at coordinate (x,y)
            }


            progressBar.Value = 1;
            Color[,] newImage = new Color[InputImage.Size.Width, InputImage.Size.Height];
            for (int x = 0; x < InputImage.Size.Width; x++)
            for (int y = 0; y < InputImage.Size.Height; y++)
            {
                double newColor;
                if (x < halfSize || y < halfSize || y >= InputImage.Size.Height - halfSize ||
                    x >= InputImage.Size.Width - halfSize)
                {
                    newColor = 128.0;
                }
                else
                {
                    newColor = Math.Sqrt(Math.Pow(imageSobelX[x, y], 2) + Math.Pow(imageSobelY[x, y], 2));
                    if (newColor > 255)
                        newColor = 255.0;
                    else if (newColor < 0) newColor = 0.0;
                }

                int convertedNewColor = Convert.ToInt16(newColor);
                Color updatedColor = Color.FromArgb(convertedNewColor, convertedNewColor, convertedNewColor);
                newImage[x, y] = updatedColor; // Set the new pixel color at coordinate (x,y)
            }

            return newImage;
        }

        private Color[,] conversionThreshold(Color[,] image, int threshold)
        {
            image = conversionGrayscale(
                image); // Convert image to grayscale, even though it already is a grayscale image.
            progressBar.Value = 1;
            for (int x = 0; x < InputImage.Size.Width; x++)
            for (int y = 0; y < InputImage.Size.Height; y++)
            {
                Color pixelColor = image[x, y]; // Get the pixel color at coordinate (x,y)
                int newColor =
                    pixelColor.R > threshold
                        ? 255
                        : 0; //Uses the red color to calculate the threshold, since all channels are the same.
                Color updatedColor =
                    Color.FromArgb(newColor, newColor,
                        newColor); // Pixel is either 255 or 0, depending on the threshold.
                image[x, y] = updatedColor; // Set the new pixel color at coordinate (x,y)
                progressBar.PerformStep(); // Increment progress bar
            }

            return image;
        }

        private Color[,] conversionGrayscale(Color[,] image)
        {
            for (int x = 0; x < InputImage.Size.Width; x++)
            for (int y = 0; y < InputImage.Size.Height; y++)
            {
                Color pixelColor = image[x, y]; // Get the pixel color at coordinate (x,y)
                int convertedRedColor = (int) (pixelColor.R * 0.299);
                int convertedGreenColor = (int) (pixelColor.G * 0.587);
                int convertedBlueColor = (int) (pixelColor.B * 0.114);
                int Y = convertedRedColor + convertedGreenColor + convertedBlueColor;
                if (Y < 0) Y = 0;

                if (Y > 255) Y = 255;

                Color updatedColor = Color.FromArgb(Y, Y, Y);
                image[x, y] = updatedColor; // Set the new pixel color at coordinate (x,y)
                progressBar.PerformStep(); // Increment progress bar
            }

            return image;
        }

        //Draws lines based on the hough graph
        private Color[,] drawLinesFromHoughOnImage(List<Tuple<int, int>> coordinates, int accuracy, Color[,] image)
        {
            Debugger.debug(2, "Amount of coordinates is " + coordinates.Count);
            foreach (var element in coordinates)
            {
                int theta = element.Item1;
                int r = element.Item2;
                int previousy = 0;
                for (int x = 0; x < InputImage.Size.Width; x++)
                {
                    if (x == 0) previousy = getYfromTheta(theta, r, accuracy, x);
                    try
                    {
                        int y = getYfromTheta(theta, r, accuracy, x);
                        int ydelta = y - previousy;
                        if (!(y > InputImage.Size.Height || y < 0))
                        {
                            if (ydelta < 0 && y != previousy)
                                for (int pointer = previousy; y < pointer; pointer--)
                                    try
                                    {
                                        image[x, pointer] = Color.FromArgb(57, 255, 20);
                                    }
                                    catch (IndexOutOfRangeException)
                                    {
                                        Debugger.debug(2,
                                            "Index out of range at drawLinesFromHoughOnImage, negative ydelta");
                                    }
                            else if (ydelta > 0 && y != previousy)
                                for (int pointer = previousy; y > pointer; pointer++)
                                    try
                                    {
                                        image[x, pointer] = Color.FromArgb(57, 255, 20);
                                    }
                                    catch (IndexOutOfRangeException)
                                    {
                                        Debugger.debug(2,
                                            "Index out of range at drawLinesFromHoughOnImage, positive ydelta");
                                    }
                            else
                                try
                                {
                                    image[x, y] = Color.FromArgb(57, 255, 20);
                                }
                                catch (IndexOutOfRangeException)
                                {
                                    Debugger.debug(2,
                                        "Index out of range at drawLinesFromHoughOnImage, positive ydelta");
                                }
                        }

                        previousy = y;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Debugger.debug(2, "Index out of range at drawLinesFromHoughOnImage");
                    }
                }
            }

            return image;
        }

        //Helper function for draw lines from hough on image
        private int getYfromTheta(int step, int r, int accuracy, int x)
        {
            int xCtr = InputImage.Size.Width / 2;
            int yCtr = InputImage.Size.Height / 2;
            int cRad = accuracy / 2;
            double rMax = Math.Sqrt(xCtr * xCtr + yCtr * yCtr);
            double dRad = 2.0 * rMax / accuracy;
            double theta = step * (Math.PI / accuracy);
            int newx = x - xCtr;

            return (int) (((r - cRad) * dRad - newx * Math.Cos(theta)) / Math.Sin(theta)) + yCtr;
        }

        //from a houghgraph, gets back the 'original' values.
        private List<Tuple<int, int>> getCoordinatesWhitePixels(int[,] houghGraph)
        {
            List<Tuple<int, int>> coordinates = new List<Tuple<int, int>>();
            for (int x = 0; x < houghGraph.GetLength(0); x++)
            for (int y = 0; y < houghGraph.GetLength(1); y++)
                if (houghGraph[x, y] == 255)
                {
                    Tuple<int, int> coordinate = new Tuple<int, int>(x, y);
                    coordinates.Add(coordinate);
                }

            return coordinates;
        }

        //Non max supression used with a hough graph
        private int[,] nonMaxSupression(int[,] houghGraph)
        {
            int filterx = 3;
            int filtery = 3;
            int halfsizex = (filterx - 1) / 2;
            int halfsizey = (filtery - 1) / 2;
            List<int> values;
            for (int theta = 0; theta < houghGraph.GetLength(0); theta++)
            for (int r = 0; r < houghGraph.GetLength(1); r++)
            {
                values = new List<int>();
                for (int x = -halfsizex; x < halfsizex; x++)
                for (int y = -halfsizey; y < halfsizex; y++)
                {
                    int transformedx = x + theta;
                    int transformedy = y + r;
                    try
                    {
                        values.Add(houghGraph[transformedx, transformedy]);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Debugger.debug(2, "Index out of range exception thrown in the nonMaxSupression");
                    }
                }

                int maximumvalue = getMaximumValue(values);

                if (houghGraph[theta, r] != maximumvalue) houghGraph[theta, r] = 0;
            }

            return houghGraph;
        }

        //Threshold applied to a houghgraph
        private int[,] thresholdHoughGraph(int[,] houghGraph, int threshold)
        {
            for (int theta = 0; theta < houghGraph.GetLength(0); theta++)
            for (int r = 0; r < houghGraph.GetLength(1); r++)
                if (!(houghGraph[theta, r] > threshold))
                    houghGraph[theta, r] = 0;
                else
                    houghGraph[theta, r] = 255;

            return houghGraph;
        }

        private int[,] conversionDilationInt(int[,] houghGraph, Coordinates kernelList)
        {
            int[,] houghGraphDilated = new int[houghGraph.GetLength(0), houghGraph.GetLength(1)];
            for (int step = 0; step < houghGraph.GetLength(0); step++)
            for (int r = 0; r < houghGraph.GetLength(1); r++)
                if (houghGraph[step, r] == 255)
                    for (var index = 0; index < kernelList.getLength(); index++)
                    {
                        var structureStep = step + kernelList.getX(index);
                        var structureR = r + kernelList.getY(index);

                        if (!(structureStep < 0 || structureR < 0 || structureR >= houghGraph.GetLength(1) - 1 ||
                              structureStep >= houghGraph.GetLength(0) - 1))
                            houghGraphDilated[structureStep, structureR] = 255;
                    }

            return houghGraphDilated;
        }

        private int[,] conversionErosionInt(int[,] houghGraph, Coordinates kernelList)
        {
            int[,] houghGraphEroded = new int[houghGraph.GetLength(0), houghGraph.GetLength(1)];
            for (int step = 0; step < houghGraph.GetLength(0); step++)
            for (int r = 0; r < houghGraph.GetLength(1); r++)
                if (houghGraph[step, r] == 255)
                {
                    var doesKernelFit = true;
                    for (var index = 0; index < kernelList.getLength(); index++)
                    {
                        var structureStep = step + kernelList.getX(index);
                        var structureR = r + kernelList.getY(index);

                        if (!(structureStep < 0 || structureR < 0 || structureR >= houghGraph.GetLength(1) - 1 ||
                              structureStep >= houghGraph.GetLength(0) - 1))
                            doesKernelFit = doesKernelFit && houghGraph[structureStep, structureR] == 255;

                        if (!doesKernelFit) break;
                    }

                    if (doesKernelFit) houghGraphEroded[step, r] = 255;
                }

            return houghGraphEroded;
        }

        private int[,] applyClosingToTresholdedHoughGraph(int[,] houghGraph)
        {
            Coordinates kernelList = new Coordinates();
            kernelList.addCoordinate(-1, -1);
            kernelList.addCoordinate(-1, 0);
            kernelList.addCoordinate(-1, 1);
            kernelList.addCoordinate(0, -1);
            kernelList.addCoordinate(0, 0);
            kernelList.addCoordinate(0, 1);
            kernelList.addCoordinate(1, -1);
            kernelList.addCoordinate(1, 0);
            kernelList.addCoordinate(1, 1);

            int amountOfIterations = 7;
            for (int index = 0; index < amountOfIterations; index++)
                houghGraph = conversionDilationInt(houghGraph, kernelList);
            for (int index = 0; index < amountOfIterations; index++)
                houghGraph = conversionErosionInt(houghGraph, kernelList);
            return houghGraph;
        }

        private int[,] conversionHough(Color[,] image, int accuracy)
        {
            int xCtr = InputImage.Size.Width / 2;
            int yCtr = InputImage.Size.Height / 2;
            int nAng = accuracy;
            double dAng = Math.PI / nAng;
            int nRad = accuracy;
            int cRad = nRad / 2;
            double rMax = Math.Sqrt(xCtr * xCtr + yCtr * yCtr);
            double dRad = 2.0 * rMax / nRad;
            int[,] houghGraph = new int[nAng, nRad];

            for (int u = 0; u < InputImage.Size.Width; u++)
            {
                int x = u - xCtr;
                for (int v = 0; v < InputImage.Size.Height; v++)
                {
                    int y = v - yCtr;
                    if (image[u, v].R == 255)
                        for (int step = 0; step < nAng; step++)
                        {
                            double theta = dAng * step;
                            int r = cRad + (int) Math.Round((x * Math.Cos(theta) + y * Math.Sin(theta)) / dRad);
                            if (r >= 0 && r < nRad) houghGraph[step, r]++;
                        }
                }
            }

            return houghGraph;
        }

        private Color[,] imageFromHoughGraph(int[,] graph)
        {
            Color[,] outputimage = makeBinaryImage();
            double xFactor = (double) InputImage.Size.Width / graph.GetLength(0);
            double yFactor = (double) InputImage.Size.Height / graph.GetLength(1);
            for (int theta = 0; theta < graph.GetLength(0); theta++)
            for (int r = 0; r < graph.GetLength(1); r++)
            {
                int valueFromGraph = graph[theta, r];
                if (valueFromGraph > 0)
                {
                    int x = (int) (xFactor * theta);
                    int y = (int) (yFactor * r);
                    if (x > InputImage.Size.Width - 1) x = InputImage.Size.Width - 1;
                    if (y > InputImage.Size.Height - 1) y = InputImage.Size.Height - 1;
                    if (valueFromGraph > 255) valueFromGraph = 255;
                    Color newColor = Color.FromArgb(valueFromGraph, valueFromGraph, valueFromGraph);
                    outputimage[x, y] = newColor;
                }
            }

            return outputimage;
        }

        private double convertToRadians(double degree)
        {
            return degree * (Math.PI / 180.0);
        }


        //Applies a geodesic erosion to an image, given a check image
        private Color[,] conversionGeodesicErosion(Color[,] image, bool isBinary, Color[,] checkImage, bool newKernel)
        {
            return conversionMax(conversionErosion(image, isBinary, newKernel), checkImage);
        }

        //Applies a geodesic dilation to an image, given a check image
        private Color[,] conversionGeodesicDilation(Color[,] image, bool isBinary, Color[,] checkImage, bool newKernel)
        {
            return conversionMin(conversionDilation(image, isBinary, newKernel), checkImage);
        }

        //Acts as a switch between erosiojn applied to a binary or grayscale image
        private Color[,] conversionErosion(Color[,] image, bool isBinary, bool newKernel)
        {
            try
            {
                if (isBinary)
                {
                    var kernel = convertInputToTuplesBinary(newKernel);
                    return conversionErosionBinary(image, kernel);
                }
            }
            catch (Exception E)
            {
                MessageBox.Show("Did you input a correct filter?");
                Debugger.debug(2, E.Message);
            }

            return null;
        }

        //Acts as a switch between dilation applied to a binary or grayscale image
        private Color[,] conversionDilation(Color[,] image, bool isBinary, bool newKernel)
        {
            try
            {
                if (isBinary)
                {
                    var kernel = convertInputToTuplesBinary(newKernel);
                    return conversionDilationBinary(image, kernel);
                }
            }
            catch (Exception E)
            {
                MessageBox.Show("Did you input a correct filter?");
                Debugger.debug(2, E.Message);
            }

            return null;
        }

        //Applies dilation to a binary image, with a given kernel (x,y)
        private Color[,] conversionDilationBinary(Color[,] image, Tuple<int, int>[] kernel)
        {
            var newImage = makeBinaryImage();
            for (var x = 0; x < InputImage.Size.Width; x++)
            for (var y = 0; y < InputImage.Size.Height; y++)
            {
                var valueList = new List<int>();
                if (image[x, y].R == 255)
                    for (var structureIndex = 0; structureIndex < kernel.Length; structureIndex++)
                    {
                        var structureX = x + kernel[structureIndex].Item1;
                        var structureY = y + kernel[structureIndex].Item2;

                        if (!(structureX < 0 || structureY < 0 || structureY > InputImage.Size.Height - 1 ||
                              structureX > InputImage.Size.Width - 1))
                            newImage[structureX, structureY] = Color.FromArgb(255, 255, 255);
                    }
            }

            return newImage;
        }

        //Applies erosion to a binary image, with a given kernel (x,y)
        private Color[,] conversionErosionBinary(Color[,] image, Tuple<int, int>[] kernel)
        {
            var newImage = makeBinaryImage();
            for (var x = 0; x < InputImage.Size.Width; x++)
            for (var y = 0; y < InputImage.Size.Height; y++)
            {
                var valueList = new List<int>();
                if (image[x, y].R == 255)
                {
                    var doesKernelFit = true;
                    for (var structureIndex = 0; structureIndex < kernel.Length; structureIndex++)
                    {
                        var structureX = x + kernel[structureIndex].Item1;
                        var structureY = y + kernel[structureIndex].Item2;

                        if (!(structureX < 0 || structureY < 0 || structureY > InputImage.Size.Height - 1 ||
                              structureX > InputImage.Size.Width - 1))
                            doesKernelFit = doesKernelFit && image[structureX, structureY].R == 255;

                        if (!doesKernelFit) break;
                    }

                    if (doesKernelFit) newImage[x, y] = Color.White;
                }
            }

            return newImage;
        }

        //Applies erosion to a grayscale image, with a given kernel (x,y,weight)
        private Color[,] conversionErosionGrayscale(Color[,] image, Tuple<int, int, int>[] kernel)
        {
            var newImage = new Color[InputImage.Size.Width, InputImage.Size.Height];
            for (var x = 0; x < InputImage.Size.Width; x++)
            for (var y = 0; y < InputImage.Size.Height; y++)
            {
                var valueList = new List<int>();
                for (var structureIndex = 0; structureIndex < kernel.Length; structureIndex++)
                {
                    var structureX = x + kernel[structureIndex].Item1;
                    var structureY = y + kernel[structureIndex].Item2;

                    if (!(structureX < 0 || structureY < 0 || structureY > InputImage.Size.Height - 1 ||
                          structureX > InputImage.Size.Width - 1))
                        valueList.Add(image[structureX, structureY].R - kernel[structureIndex].Item3);
                }

                var newColor = getMinimumValue(valueList);
                if (newColor < 0) newColor = 0;
                newImage[x, y] = Color.FromArgb(newColor, newColor, newColor);
            }

            return newImage;
        }

        //Applies dilation to a grayscale image, with a given kernel (x,y,weight)
        private Color[,] conversionDilationGrayscale(Color[,] image, Tuple<int, int, int>[] kernel)
        {
            var newImage = new Color[InputImage.Size.Width, InputImage.Size.Height];
            for (var x = 0; x < InputImage.Size.Width; x++)
            for (var y = 0; y < InputImage.Size.Height; y++)
            {
                var valueList = new List<int>();
                for (var structureIndex = 0; structureIndex < kernel.Length; structureIndex++)
                {
                    var structureX = x + kernel[structureIndex].Item1;
                    var structureY = y + kernel[structureIndex].Item2;

                    if (!(structureX < 0 || structureY < 0 || structureY > InputImage.Size.Height - 1 ||
                          structureX > InputImage.Size.Width - 1))
                        valueList.Add(image[structureX, structureY].R + kernel[structureIndex].Item3);
                }

                var newColor = getMaximumValue(valueList);
                if (newColor > 255) newColor = 255;
                newImage[x, y] = Color.FromArgb(newColor, newColor, newColor);
            }

            Debugger.debug(2, "Conversion dilation grayscale has been executed");


            return newImage;
        }

        //Returns an image with the boundary of the first shape found.
        private Color[,] conversionBoundary(Color[,] image)
        {
            var startCoordinate = getStartPoint(image);
            if (startCoordinate == null)
            {
                Debugger.debug(2, "No shape detected");
                return image;
            }

            var newImage = makeBinaryImage();
            var startPointx = startCoordinate.Item1;
            var startPointy = startCoordinate.Item2;
            var listofThings = getShapeCoordinates(image, startPointx, startPointy);

            var arrayList = listofThings.ToArray();
            //Draws the boundary with a neon green colour.
            foreach (var elem in arrayList)
                try
                {
                    newImage[elem.Item1, elem.Item2] = Color.FromArgb(57, 255, 20);
                }
                catch
                {
                    Debugger.debug(2, "Exception thrown in conversion Boundary");
                }

            return newImage;
        }

        //Calculates the fourrier descriptor, prints the coefficients and returns an empty image
        private Color[,] conversionFourier(Color[,] image)
        {
            var startx = getStartPoint(image).Item1;
            var starty = getStartPoint(image).Item2;
            var shapeCoordinateArray = getShapeCoordinates(image, startx, starty).ToArray();

            var decimatedList = new List<Tuple<int, int>>();

            int index = 0;
            foreach (var elem in shapeCoordinateArray)
            {
                if (index % 25 == 0) decimatedList.Add(elem);
                index++;
            }

            var fourierCoefficientArray = createFourierDescriptor(decimatedList.ToArray());

            foreach (var value in fourierCoefficientArray) Debugger.debug(2, "Fourier value, real: " + value.Item1);
            foreach (var value in fourierCoefficientArray)
                Debugger.debug(2, "Fourier value, imaginary: " + value.Item2);
            var newImage = makeBinaryImage();
            return newImage;
        }

        //Takes a grayscale image as input and returns its complementary image
        private Color[,] conversionComplement(Color[,] image)
        {
            return conversionNegative(image); //It's actually the same thing
        }

        //Returns a 'combined' image, where the lowest value is selected for every channel
        private Color[,] conversionMin(Color[,] image1, Color[,] image2)
        {
            if (isImageSameSize(image1, image2))
            {
                Color[,] output = new Color[image1.GetLength(0), image1.GetLength(1)];

                for (var x = 0; x < image1.GetLength(0); x++)
                for (var y = 0; y < image1.GetLength(1); y++)
                {
                    Color pixelColor1 = image1[x, y]; // Get the pixel color at coordinate (x,y) of the first image
                    Color pixelColor2 = image2[x, y]; //Get the pixel color at coordinate (x,y) of the second image
                    var updatedColor = Color.FromArgb(Math.Min(pixelColor1.R, pixelColor2.R),
                        Math.Min(pixelColor1.G, pixelColor2.G),
                        Math.Min(pixelColor1.B, pixelColor2.B)); //Selecting the min values for every channel
                    output[x, y] = updatedColor; // Set the new pixel color at coordinate (x,y)
                    progressBar.PerformStep(); // Increment progress bar
                }

                return output;
            }

            //If the images don't match, return null
            return null;
        }

        //Returns a 'combined' image, where the highest value is selected for every channel
        private Color[,] conversionMax(Color[,] image1, Color[,] image2)
        {
            if (isImageSameSize(image1, image2))
            {
                Color[,] output = new Color[image1.GetLength(0), image1.GetLength(1)];

                for (var x = 0; x < image1.GetLength(0); x++)
                for (var y = 0; y < image1.GetLength(1); y++)
                {
                    Color pixelColor1 = image1[x, y]; // Get the pixel color at coordinate (x,y) of the first image
                    Color pixelColor2 = image2[x, y]; //Get the pixel color at coordinate (x,y) of the second image
                    Color updatedColor = Color.FromArgb(Math.Max(pixelColor1.R, pixelColor2.R),
                        Math.Max(pixelColor1.G, pixelColor2.G),
                        Math.Max(pixelColor1.B, pixelColor2.B)); //Selecting the max values for every channel
                    output[x, y] = updatedColor; // Set the new pixel color at coordinate (x,y)
                    progressBar.PerformStep(); // Increment progress bar
                }

                return output;
            }

            //If the images don't match, return null
            return null;
        }

        //This function takes an image and outputs a negative image
        private Color[,] conversionNegative(Color[,] image)
        {
            for (var x = 0; x < InputImage.Size.Width; x++)
            for (var y = 0; y < InputImage.Size.Height; y++)
            {
                var pixelColor = image[x, y]; // Get the pixel color at coordinate (x,y)
                var updatedColor =
                    Color.FromArgb(255 - pixelColor.R, 255 - pixelColor.G, 255 - pixelColor.B); // Negative image
                image[x, y] = updatedColor; // Set the new pixel color at coordinate (x,y)
                progressBar.PerformStep(); // Increment progress bar
            }

            return image;
        }

        private Tuple<int, int>[] complexToTupleArray(Complex[] array)
        {
            var output = new Tuple<int, int>[array.Length];
            var i = 0;
            foreach (var elem in array)
            {
                output[i] = new Tuple<int, int>((int) elem.Real, (int) elem.Imaginary);
                i++;
            }

            return output;
        }

        //_______Helper Functions_________
        //Returns a list with contour coordinates of the shape at the start coordinates.
        private List<Tuple<int, int>> getShapeCoordinates(Color[,] image, int startx, int starty)
        {
            var listOfCoordinates = new List<Tuple<int, int>>();
            listOfCoordinates.Add(new Tuple<int, int>(startx, starty));
            int currentx = startx;
            int currenty = starty;
            bool done = false;
            int direction = 1;
            while (!done)
            {
                direction = (direction + 6) % 8;
                direction = getNextPoint(image, currentx, currenty, direction);
                if (direction > 8) break; //No next point could be found, so break
                currentx = currentx + clockwiseRotation[direction].Item1;
                currenty = currenty + clockwiseRotation[direction].Item2;
                done = currentx == startx && currenty == starty;
                if (!done) listOfCoordinates.Add(new Tuple<int, int>(currentx, currenty));
            }

            return listOfCoordinates;
        }

        private List<Tuple<int, int>> getShapeCoordinates(int[,] image, int startx, int starty)
        {
            var listOfCoordinates = new List<Tuple<int, int>>();
            listOfCoordinates.Add(new Tuple<int, int>(startx, starty));
            int currentx = startx;
            int currenty = starty;
            bool done = false;
            int direction = 1;
            while (!done)
            {
                direction = (direction + 6) % 8;
                direction = getNextPoint(image, currentx, currenty, direction);
                if (direction > 8) break; //No next point could be found, so break
                currentx = currentx + clockwiseRotation[direction].Item1;
                currenty = currenty + clockwiseRotation[direction].Item2;
                done = currentx == startx && currenty == starty;
                if (!done) listOfCoordinates.Add(new Tuple<int, int>(currentx, currenty));
            }

            return listOfCoordinates;
        }

        //Counts the amount of distinct values of the InputImage ;we assume that the image is a grayscale
        //Will count the amount of distinct red values when applied to a colored image
        private int countDistinctValues()
        {
            int distinctValues = 0;
            int[] histogramRed = calculateHistogramFromImage(InputImage).Item1;

            foreach (int amountOfPixels in histogramRed) // Count values bigger than zero
                if (amountOfPixels > 0)
                    distinctValues++;

            return distinctValues;
        }

        //Retrieves a second image via file search
        private Color[,] getSecondImage()
        {
            if (openImageDialog.ShowDialog() == DialogResult.OK) // Open File Dialog
            {
                var file = openImageDialog.FileName; // Get the file name
                var imgBmap = new Bitmap(file); // Create new Bitmap from file
                var image = new Color[imgBmap.Size.Width, imgBmap.Size.Height];
                for (var x = 0; x < imgBmap.Size.Width; x++)
                for (var y = 0; y < imgBmap.Size.Height; y++)
                    image[x, y] = imgBmap.GetPixel(x, y); // Set pixel color in array at (x,y)
                return image;
            }

            return null;
        }

        //Checks if two images are of the same size
        private bool isImageSameSize(Color[,] image1, Color[,] image2)
        {
            Debugger.debug(2,
                image1.GetLength(0) + " " + image1.GetLength(1) + " " + image2.GetLength(0) + " " +
                image2.GetLength(1));
            if (image1.GetLength(0) != image2.GetLength(0) || image1.GetLength(1) != image2.GetLength(1)
            ) //images should be of the same size
            {
                MessageBox.Show("Image sizes didn't match, please try again");
                return false;
            }

            return true;
        }

        //Creates a black image, with the size of the InputImage
        private Color[,] makeBinaryImage()
        {
            var newBinaryImage = new Color[InputImage.Size.Width, InputImage.Size.Height];
            for (var x = 0; x < InputImage.Size.Width; x++)
            for (var y = 0; y < InputImage.Size.Height; y++)
                newBinaryImage[x, y] = Color.Black;

            return newBinaryImage;
        }

        private Color[,] makeBinaryImage(int sizex, int sizey)
        {
            var newBinaryImage = new Color[sizex, sizey];
            for (var x = 0; x < sizex; x++)
            for (var y = 0; y < sizey; y++)
                newBinaryImage[x, y] = Color.Black;

            return newBinaryImage;
        }

        //Returns the fourier shape descriptor for a given set of points
        private Tuple<double, double>[] createFourierDescriptor(Tuple<int, int>[] borderPoints)
        {
            var n = borderPoints.Length;
            var output = new Tuple<double, double>[n];
            var complexList = tupleToComplexArray(borderPoints);

            for (var k = 0; k < n; k++) //loops the output elements
            {
                Complex pt = 0;

                for (var j = 0; j < n; j++) //loops the input elements
                {
                    var exponent = 2 * Math.PI * k * j / n; // calculating the exponent
                    pt += complexList[j] * Complex.Exp(new Complex(0, -exponent)); //applying the exponential function
                }

                output[k] = new Tuple<double, double>(pt.Real / n,
                    pt.Imaginary / n); //converting back from complex to int tuples
            }

            return output;
        }

        private Complex[] tupleToComplexArray(Tuple<int, int>[] list)
        {
            var output = new Complex[list.Length];
            var i = 0;
            foreach (var elem in list)
            {
                output[i] = new Complex(elem.Item1, elem.Item2);
                i = i + 1;
            }

            return output;
        }

        //Returns the direction where the next point can be found.
        private int getNextPoint(Color[,] image, int currentX, int currentY, int dir)
        {
            for (var y = 0; y < clockwiseRotation.Length; y++)
            {
                int direction = (y + dir) % 8;
                int structureX = currentX + clockwiseRotation[direction].Item1;
                int structureY = currentY + clockwiseRotation[direction].Item2;
                int colour = 600;
                try
                {
                    colour = image[structureX, structureY].R;
                }
                catch
                {
                    Debugger.debug(2, "Exception thrown in getNextPoint, this means the 'pointer' is out of bounds");
                }

                if (colour == 255) return direction;
            }

            return 8; //Impossible value, if no other pixel has been found.
        }

        private int getNextPoint(int[,] image, int currentX, int currentY, int dir)
        {
            for (var y = 0; y < clockwiseRotation.Length; y++)
            {
                int direction = (y + dir) % 8;
                int structureX = currentX + clockwiseRotation[direction].Item1;
                int structureY = currentY + clockwiseRotation[direction].Item2;
                int colour = 600;
                try
                {
                    colour = image[structureX, structureY];
                }
                catch
                {
                    Debugger.debug(2, "Exception thrown in getNextPoint, this means the 'pointer' is out of bounds");
                }

                if (colour == 255) return direction;
            }

            return 8; //Impossible value, if no other pixel has been found.
        }

        //Traverses the image until a foreground pixel is found, returns the coordinates of the pixel.
        private Tuple<int, int> getStartPoint(Color[,] image)
        {
            for (var x = 0; x < InputImage.Size.Width; x++)
            for (var y = 0; y < InputImage.Size.Height; y++)
                if (image[x, y].R == 255)
                    return new Tuple<int, int>(x, y);

            return null;
        }

        private Tuple<int, int> getStartPoint(int[,] image)
        {
            for (var x = 0; x < image.GetLength(0); x++)
            for (var y = 0; y < image.GetLength(1); y++)
                if (image[x, y] == 255)
                    return new Tuple<int, int>(x, y);

            return null;
        }

        //Returns the lowest value of a list.
        private int getMinimumValue(List<int> valueList)
        {
            var lowValue = 255;
            foreach (var element in valueList)
                if (element < lowValue)
                    lowValue = element;

            return lowValue;
        }

        //Returns the lowest value of a list.
        private int getMinimumValue(int[] valueList)
        {
            var lowValue = 255;
            foreach (var element in valueList)
                if (element < lowValue)
                    lowValue = element;

            return lowValue;
        }

        //Returns the highest value of a list.
        private int getMaximumValue(List<int> valueList)
        {
            var highValue = 0;
            foreach (var element in valueList)
                if (element > highValue)
                    highValue = element;

            return highValue;
        }

        //Returns the highest value of a list.
        private int getMaximumValue(int[] valueList)
        {
            var highValue = 0;
            foreach (var element in valueList)
                if (element > highValue)
                    highValue = element;

            return highValue;
        }

        //This functions takes an image, and returns a tuple with histogram arrays
        private Tuple<int[], int[], int[]> calculateHistogramFromImage(Bitmap image)
        {
            var Image = new Color[image.Size.Width,
                image.Size.Height]; // Create array to speed-up operations (Bitmap functions are very slow)
            // Copy input Bitmap to array            
            for (var x = 0; x < image.Size.Width; x++)
            for (var y = 0; y < image.Size.Height; y++)
                Image[x, y] = image.GetPixel(x, y); // Set pixel color in array at (x,y)
            var histogramRed = new int[256];
            var histogramGreen = new int[256];
            var histogramBlue = new int[256];
            for (var x = 0; x < InputImage.Size.Width; x++)
            for (var y = 0; y < InputImage.Size.Height; y++)
            {
                var pixelColor = Image[x, y]; // Get the pixel color at coordinate (x,y)
                histogramRed[pixelColor.R]++;
                histogramGreen[pixelColor.G]++;
                histogramBlue[pixelColor.B]++;
            }

            return Tuple.Create(histogramRed, histogramGreen, histogramBlue);
        }

        private Tuple<int, int>[] convertInputToTuplesBinary(bool newKernel)
        {
            return new Tuple<int, int>[5]
            {
                new Tuple<int, int>(-1, 0), new Tuple<int, int>(0, -1), new Tuple<int, int>(0, 0),
                new Tuple<int, int>(1, 0), new Tuple<int, int>(0, 1)
            };
        }

        //This function saves the image
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (OutputImage == null) return; // Get out if no output image
            if (saveImageDialog.ShowDialog() == DialogResult.OK)
                OutputImage.Save(saveImageDialog.FileName); // Save the output image
        }

        //This function shows optional input if the combobox options requires that.
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        { 
        }

        private void outToInButton_Click(object sender, EventArgs e)
        {
            if (OutputImage == null)
                return;
            InputImage = (Bitmap) OutputImage.Clone();
            pictureBox1.Image = InputImage;
        }
    }

    public class Coordinates
    {
        public Coordinates()
        {
            coordinateList = new List<Tuple<int, int>>();
        }

        public Coordinates(int x, int y)
        {
            coordinateList = new List<Tuple<int, int>>();
            coordinateList.Add(new Tuple<int, int>(x, y));
        }

        public List<Tuple<int, int>> coordinateList { get; set; }

        public void addCoordinate(int x, int y)
        {
            coordinateList.Add(new Tuple<int, int>(x, y));
        }

        public int getLength()
        {
            return coordinateList.Count;
        }

        public int getX(int position)
        {
            return coordinateList.ElementAt(position).Item1;
        }

        public int getY(int position)
        {
            return coordinateList.ElementAt(position).Item2;
        }
    }

    public class Debugger
    {
        private static readonly int debuglevel = 0;

        public static void debug(int level, string message)
        {
            if (debuglevel >= level) Console.WriteLine(message);
        }
    }
}