using System;
using System.Collections.Generic;
using System.Drawing;
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
                if (InputImage.Size.Height <= 0 || InputImage.Size.Width <= 0 ||
                    InputImage.Size.Height > 512 || InputImage.Size.Width > 512) // Dimension check
                {
                    MessageBox.Show("Error in image dimensions (have to be > 0 and <= 512)");
                }
                else
                {
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
            var checkeredcheckbox = checkBox1.Checked;
            Color[,] secondImage;
            switch (comboBox1.Text)
            {
                case "erosion":
                    Image = conversionErosion(Image, checkeredcheckbox);
                    break;
                case "dilation":
                    Image = conversionDilation(Image, checkeredcheckbox);
                    break;
                case "geodesic erosion":
                    secondImage = getSecondImage();
                    if (secondImage == null) return;
                    Image = conversionGeodesicErosion(Image, checkeredcheckbox, secondImage);
                    break;
                case "geodesic dilation":
                    secondImage = getSecondImage();
                    if (secondImage == null) return;
                    Image = conversionGeodesicDilation(Image, checkeredcheckbox, secondImage);
                    break;
                case "opening":
                    Image = conversionErosion(Image, checkeredcheckbox);
                    Image = conversionDilation(Image, checkeredcheckbox);
                    break;
                case "closing":
                    Image = conversionDilation(Image, checkeredcheckbox);
                    Image = conversionErosion(Image, checkeredcheckbox);
                    break;
                case "complement":
                    Image = conversionComplement(Image);
                    break;
                case "min":
                    secondImage = getSecondImage();
                    if (secondImage == null) return;
                    Image = conversionMin(Image, secondImage);
                    break;
                case "max":
                    secondImage = getSecondImage();
                    if (secondImage == null) return;
                    Image = conversionMax(Image, secondImage);
                    break;
                case "value counting":
                    ValuesBox.Text = countDistinctValues().ToString();
                    break;
                case "boundary trace":
                    Image = conversionBoundary(Image);
                    break;
                case "fourier descriptor":
                    Image = conversionFourier(Image);
                    break;
                default:
                    Console.WriteLine("Nothing matched");
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

        //Assignment 2 functionality
        //_______Main Functionality_________

        //Applies a geodesic erosion to an image, given a check image
        private Color[,] conversionGeodesicErosion(Color[,] image, bool isBinary, Color[,] checkImage)
        {
            return conversionMax(conversionErosion(image, isBinary), checkImage);
        }

        //Applies a geodesic dilation to an image, given a check image
        private Color[,] conversionGeodesicDilation(Color[,] image, bool isBinary, Color[,] checkImage)
        {
            return conversionMin(conversionDilation(image, isBinary), checkImage);
        }

        //Acts as a switch between erosiojn applied to a binary or grayscale image
        private Color[,] conversionErosion(Color[,] image, bool isBinary)
        {
            try
            {
                if (isBinary)
                {
                    var kernel = convertInputToTuplesBinary();
                    return conversionErosionBinary(image, kernel);
                }
                else
                {
                    var kernel = convertInputToTuplesGrayscale();
                    return conversionErosionGrayscale(image, kernel);
                }
            }
            catch (Exception E)
            {
                MessageBox.Show("Did you input a correct filter?");
                Console.WriteLine(E.Message);
            }
            return null;
        }

        //Acts as a switch between dilation applied to a binary or grayscale image
        private Color[,] conversionDilation(Color[,] image, bool isBinary)
        {
            
                try
                {
                    if (isBinary)
                    {
                        var kernel = convertInputToTuplesBinary();
                        return conversionDilationBinary(image, kernel);
                    }
                    else
                    {
                        var kernel = convertInputToTuplesGrayscale();
                        return conversionDilationGrayscale(image, kernel);
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show("Did you input a correct filter?");
                    Console.WriteLine(E.Message);
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

            Console.WriteLine("Breakpoint");

            return newImage;
        }

        //Returns an image with the boundary of the first shape found.
        private Color[,] conversionBoundary(Color[,] image)
        {
            var startCoordinate = getStartPoint(image);
            if (startCoordinate == null)
            {
                Console.WriteLine("No shape detected");
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
                    Console.Write("Whoops");
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
                if (index % 25 == 0)
                {
                    decimatedList.Add(elem);
                }
                index++;
            }
            var fourierCoefficientArray = createFourierDescriptor(decimatedList.ToArray());

            var counter = 0;
            foreach (var value in fourierCoefficientArray)
            {
                //Console.Write(counter + " ");
                Console.WriteLine(value.Item1);
                counter++;
            }
            counter = 0;
            Console.WriteLine("eleGiggle");
            foreach (var value in fourierCoefficientArray)
            {
                //Console.Write(counter + " ");
                Console.WriteLine(value.Item2);
                counter++;
            }
            var newImage = makeBinaryImage();
            return newImage;
        }

        //Takes a greyscale image as input and returns its complementary image
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
                        Math.Min(pixelColor1.G, pixelColor2.G), Math.Min(pixelColor1.B, pixelColor2.B)); //Selecting the min values for every channel
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
                        Math.Max(pixelColor1.G, pixelColor2.G), Math.Max(pixelColor1.B, pixelColor2.B)); //Selecting the max values for every channel
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

        //Counts the amount of distinct values of the InputImage ;we assume that the image is a greyscale
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
                if (imgBmap.Size.Height <= 0 || imgBmap.Size.Width <= 0 ||
                    imgBmap.Size.Height > 512 || imgBmap.Size.Width > 512) // Dimension check
                {
                    Console.WriteLine(imgBmap.Size.Height + "::" + imgBmap.Size.Width);
                    MessageBox.Show("Error in image dimensions (have to be > 0 and <= 512)");
                    return null;
                }

                return image;
            }

            return null;
        }

        //Checks if two images are of the same size
        private bool isImageSameSize(Color[,] image1, Color[,] image2)
        {
            if (image1.GetLength(0) != image2.GetLength(0) || image1.GetLength(1) != image2.GetLength(1)
            ) //images should be of the same size
            {
                MessageBox.Show("Image sizes didn't match, please try again");
                return false;
            }
            else
            {
                return true;
            }
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
                    Console.WriteLine("Out of bounds");
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

        //Returns the lowest value of a list.
        private int getMinimumValue(List<int> valueList)
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

        private Tuple<int, int>[] convertInputToTuplesBinary()
        {
            var allCoordinates = richTextBox1.Text;
            var coordinatePairs = allCoordinates.Split(' ');
            var coordinateTupleArray = new Tuple<int, int>[coordinatePairs.Length];
            for (var x = 0; x < coordinatePairs.Length; x++)
            {
                var coordinates = coordinatePairs[x].Split(',');
                int xCoordinate = Convert.ToInt16(coordinates[0]);
                int yCoordinate = Convert.ToInt16(coordinates[1]);
                coordinateTupleArray[x] = Tuple.Create(xCoordinate, yCoordinate);
                Console.WriteLine("X: " + xCoordinate + " Y: " + yCoordinate);
            }

            return coordinateTupleArray;
        }

        private Tuple<int, int, int>[] convertInputToTuplesGrayscale()
        {
            var allCoordinates = richTextBox1.Text;
            var coordinatePairs = allCoordinates.Split(' ');
            var coordinateTupleArray = new Tuple<int, int, int>[coordinatePairs.Length];
            for (var x = 0; x < coordinatePairs.Length; x++)
            {
                var coordinates = coordinatePairs[x].Split(',');
                int xCoordinate = Convert.ToInt16(coordinates[0]);
                int yCoordinate = Convert.ToInt16(coordinates[1]);
                int weight = Convert.ToInt16(coordinates[2]);
                coordinateTupleArray[x] = Tuple.Create(xCoordinate, yCoordinate, weight);
                Console.WriteLine("X: " + xCoordinate + " Y: " + yCoordinate + " weight: " + weight);
            }

            return coordinateTupleArray;
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
            if (comboBox1.Text.Equals("erosion") || comboBox1.Text.Equals("dilation") ||
                comboBox1.Text.Equals("opening") || comboBox1.Text.Equals("closing") ||
                comboBox1.Text.Equals("geodesic erosion") || comboBox1.Text.Equals("geodesic dilation"))
                checkBox1.Visible = true;
            else
                checkBox1.Visible = false;
        }

        //Adjusts the label for the input based on the value of the 'binary' checkbox
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Visible)
            {
                if (checkBox1.Checked)
                    label1.Text = "Enter the structuring element. Example: 0,0 1,0 x,y ";
                else
                    label1.Text = "Enter the structuring element and the weight. Example: 0,0,2 1,0,0 x,y,w";
            }
        }
    }
}