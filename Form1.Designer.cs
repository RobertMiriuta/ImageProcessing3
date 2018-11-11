namespace INFOIBV
{
    partial class INFOIBV
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.LoadImageButton = new System.Windows.Forms.Button();
            this.openImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.imageFileName = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.applyButton = new System.Windows.Forms.Button();
            this.saveImageDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveButton = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.histoIn = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.histoOut = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.ValuesBox = new System.Windows.Forms.TextBox();
            this.valuesLabel = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.matrix1 = new System.Windows.Forms.TextBox();
            this.matrix6 = new System.Windows.Forms.TextBox();
            this.matrix2 = new System.Windows.Forms.TextBox();
            this.matrix13 = new System.Windows.Forms.TextBox();
            this.matrix12 = new System.Windows.Forms.TextBox();
            this.matrix11 = new System.Windows.Forms.TextBox();
            this.matrix8 = new System.Windows.Forms.TextBox();
            this.matrix7 = new System.Windows.Forms.TextBox();
            this.matrix3 = new System.Windows.Forms.TextBox();
            this.matrix4 = new System.Windows.Forms.TextBox();
            this.matrix18 = new System.Windows.Forms.TextBox();
            this.matrix17 = new System.Windows.Forms.TextBox();
            this.matrix16 = new System.Windows.Forms.TextBox();
            this.matrix23 = new System.Windows.Forms.TextBox();
            this.matrix22 = new System.Windows.Forms.TextBox();
            this.matrix21 = new System.Windows.Forms.TextBox();
            this.matrix19 = new System.Windows.Forms.TextBox();
            this.matrix14 = new System.Windows.Forms.TextBox();
            this.matrix9 = new System.Windows.Forms.TextBox();
            this.matrix20 = new System.Windows.Forms.TextBox();
            this.matrix15 = new System.Windows.Forms.TextBox();
            this.matrix10 = new System.Windows.Forms.TextBox();
            this.matrix5 = new System.Windows.Forms.TextBox();
            this.matrix24 = new System.Windows.Forms.TextBox();
            this.matrix25 = new System.Windows.Forms.TextBox();
            this.outToInButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.histoIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.histoOut)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadImageButton
            // 
            this.LoadImageButton.Location = new System.Drawing.Point(16, 15);
            this.LoadImageButton.Margin = new System.Windows.Forms.Padding(4);
            this.LoadImageButton.Name = "LoadImageButton";
            this.LoadImageButton.Size = new System.Drawing.Size(131, 28);
            this.LoadImageButton.TabIndex = 0;
            this.LoadImageButton.Text = "Load image...";
            this.LoadImageButton.UseVisualStyleBackColor = true;
            this.LoadImageButton.Click += new System.EventHandler(this.LoadImageButton_Click);
            // 
            // openImageDialog
            // 
            this.openImageDialog.Filter = "Bitmap files (*.bmp;*.gif;*.jpg;*.png;*.tiff;*.jpeg)|*.bmp;*.gif;*.jpg;*.png;*.ti" +
    "ff;*.jpeg";
            this.openImageDialog.InitialDirectory = "..\\..\\images";
            // 
            // imageFileName
            // 
            this.imageFileName.Location = new System.Drawing.Point(155, 17);
            this.imageFileName.Margin = new System.Windows.Forms.Padding(4);
            this.imageFileName.Name = "imageFileName";
            this.imageFileName.ReadOnly = true;
            this.imageFileName.Size = new System.Drawing.Size(368, 22);
            this.imageFileName.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(17, 55);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(683, 630);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(856, 15);
            this.applyButton.Margin = new System.Windows.Forms.Padding(4);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(137, 28);
            this.applyButton.TabIndex = 3;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // saveImageDialog
            // 
            this.saveImageDialog.Filter = "Bitmap file (*.bmp)|*.bmp";
            this.saveImageDialog.InitialDirectory = "..\\..\\images";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(1264, 3);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(127, 28);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save as BMP...";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(708, 55);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(683, 630);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(1001, 17);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(255, 25);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 6;
            this.progressBar.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "negative",
            "grayscale",
            "shape labeling",
            "contrastadjustment",
            "gaussian",
            "threshold",
            "threshold percentage",
            "threshold bernsen",
            "linear",
            "median",
            "edge detection",
            "erosion",
            "dilation",
            "geodesic erosion",
            "geodesic dilation",
            "opening",
            "closing",
            "complement",
            "min",
            "max",
            "value counting",
            "boundary trace",
            "fourier descriptor",
            "pipeline",
            "applyFunction1",
            "applyFunction2",
            "applyFunction3",
            "phase one",
            "phase two",
            "phase three"});
            this.comboBox1.Location = new System.Drawing.Point(688, 17);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(161, 24);
            this.comboBox1.TabIndex = 8;
            this.comboBox1.Text = "pipeline";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // histoIn
            // 
            chartArea1.AxisX.LabelStyle.Enabled = false;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorGrid.LineWidth = 0;
            chartArea1.AxisX.MajorTickMark.Enabled = false;
            chartArea1.AxisX.Maximum = 255D;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.MajorTickMark.Enabled = false;
            chartArea1.BorderWidth = 0;
            chartArea1.Name = "ChartArea1";
            this.histoIn.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.histoIn.Legends.Add(legend1);
            this.histoIn.Location = new System.Drawing.Point(17, 692);
            this.histoIn.Name = "histoIn";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.histoIn.Series.Add(series1);
            this.histoIn.Size = new System.Drawing.Size(308, 145);
            this.histoIn.TabIndex = 41;
            this.histoIn.Text = "chart1";
            // 
            // histoOut
            // 
            chartArea2.AxisX.LabelStyle.Enabled = false;
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisX.MajorGrid.LineWidth = 0;
            chartArea2.AxisX.MajorTickMark.Enabled = false;
            chartArea2.AxisX.Maximum = 255D;
            chartArea2.AxisX.Minimum = 0D;
            chartArea2.AxisY.MajorGrid.Enabled = false;
            chartArea2.AxisY.MajorTickMark.Enabled = false;
            chartArea2.BorderWidth = 0;
            chartArea2.Name = "ChartArea1";
            this.histoOut.ChartAreas.Add(chartArea2);
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.histoOut.Legends.Add(legend2);
            this.histoOut.Location = new System.Drawing.Point(1083, 696);
            this.histoOut.Name = "histoOut";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.histoOut.Series.Add(series2);
            this.histoOut.Size = new System.Drawing.Size(308, 145);
            this.histoOut.TabIndex = 44;
            this.histoOut.Text = "chart1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1398, 21);
            this.label1.MaximumSize = new System.Drawing.Size(200, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 50);
            this.label1.TabIndex = 45;
            this.label1.Text = "Enter the structuring element and the weight. Example: 0,0,2 1,0,0 x,y,w";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(1398, 92);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(216, 193);
            this.richTextBox1.TabIndex = 46;
            this.richTextBox1.Text = "";
            // 
            // ValuesBox
            // 
            this.ValuesBox.Location = new System.Drawing.Point(1515, 299);
            this.ValuesBox.Name = "ValuesBox";
            this.ValuesBox.ReadOnly = true;
            this.ValuesBox.Size = new System.Drawing.Size(106, 22);
            this.ValuesBox.TabIndex = 45;
            // 
            // valuesLabel
            // 
            this.valuesLabel.AutoSize = true;
            this.valuesLabel.Location = new System.Drawing.Point(1404, 302);
            this.valuesLabel.Name = "valuesLabel";
            this.valuesLabel.Size = new System.Drawing.Size(105, 17);
            this.valuesLabel.TabIndex = 46;
            this.valuesLabel.Text = "Distinct Values:";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(530, 18);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(70, 21);
            this.checkBox1.TabIndex = 47;
            this.checkBox1.Text = "Binary";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(597, 17);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(85, 22);
            this.textBox1.TabIndex = 48;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(641, 2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(41, 22);
            this.textBox2.TabIndex = 49;
            this.textBox2.Visible = false;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(641, 30);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(41, 22);
            this.textBox3.TabIndex = 50;
            this.textBox3.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(594, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 17);
            this.label2.TabIndex = 51;
            this.label2.Text = "sigma";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(594, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 17);
            this.label3.TabIndex = 52;
            this.label3.Text = "kernel";
            this.label3.Visible = false;
            // 
            // matrix1
            // 
            this.matrix1.Location = new System.Drawing.Point(1408, 356);
            this.matrix1.Margin = new System.Windows.Forms.Padding(4);
            this.matrix1.Name = "matrix1";
            this.matrix1.Size = new System.Drawing.Size(29, 22);
            this.matrix1.TabIndex = 53;
            this.matrix1.Text = "0";
            // 
            // matrix6
            // 
            this.matrix6.Location = new System.Drawing.Point(1408, 388);
            this.matrix6.Margin = new System.Windows.Forms.Padding(4);
            this.matrix6.Name = "matrix6";
            this.matrix6.Size = new System.Drawing.Size(29, 22);
            this.matrix6.TabIndex = 54;
            this.matrix6.Text = "0";
            // 
            // matrix2
            // 
            this.matrix2.Location = new System.Drawing.Point(1446, 356);
            this.matrix2.Margin = new System.Windows.Forms.Padding(4);
            this.matrix2.Name = "matrix2";
            this.matrix2.Size = new System.Drawing.Size(29, 22);
            this.matrix2.TabIndex = 55;
            this.matrix2.Text = "0";
            // 
            // matrix13
            // 
            this.matrix13.Location = new System.Drawing.Point(1485, 420);
            this.matrix13.Margin = new System.Windows.Forms.Padding(4);
            this.matrix13.Name = "matrix13";
            this.matrix13.Size = new System.Drawing.Size(29, 22);
            this.matrix13.TabIndex = 56;
            this.matrix13.Text = "0";
            // 
            // matrix12
            // 
            this.matrix12.Location = new System.Drawing.Point(1446, 420);
            this.matrix12.Margin = new System.Windows.Forms.Padding(4);
            this.matrix12.Name = "matrix12";
            this.matrix12.Size = new System.Drawing.Size(29, 22);
            this.matrix12.TabIndex = 57;
            this.matrix12.Text = "0";
            // 
            // matrix11
            // 
            this.matrix11.Location = new System.Drawing.Point(1408, 420);
            this.matrix11.Margin = new System.Windows.Forms.Padding(4);
            this.matrix11.Name = "matrix11";
            this.matrix11.Size = new System.Drawing.Size(29, 22);
            this.matrix11.TabIndex = 58;
            this.matrix11.Text = "0";
            // 
            // matrix8
            // 
            this.matrix8.Location = new System.Drawing.Point(1485, 388);
            this.matrix8.Margin = new System.Windows.Forms.Padding(4);
            this.matrix8.Name = "matrix8";
            this.matrix8.Size = new System.Drawing.Size(29, 22);
            this.matrix8.TabIndex = 59;
            this.matrix8.Text = "0";
            // 
            // matrix7
            // 
            this.matrix7.Location = new System.Drawing.Point(1446, 388);
            this.matrix7.Margin = new System.Windows.Forms.Padding(4);
            this.matrix7.Name = "matrix7";
            this.matrix7.Size = new System.Drawing.Size(29, 22);
            this.matrix7.TabIndex = 60;
            this.matrix7.Text = "0";
            // 
            // matrix3
            // 
            this.matrix3.Location = new System.Drawing.Point(1485, 356);
            this.matrix3.Margin = new System.Windows.Forms.Padding(4);
            this.matrix3.Name = "matrix3";
            this.matrix3.Size = new System.Drawing.Size(29, 22);
            this.matrix3.TabIndex = 61;
            this.matrix3.Text = "0";
            // 
            // matrix4
            // 
            this.matrix4.Location = new System.Drawing.Point(1524, 356);
            this.matrix4.Margin = new System.Windows.Forms.Padding(4);
            this.matrix4.Name = "matrix4";
            this.matrix4.Size = new System.Drawing.Size(29, 22);
            this.matrix4.TabIndex = 62;
            this.matrix4.Text = "0";
            // 
            // matrix18
            // 
            this.matrix18.Location = new System.Drawing.Point(1485, 452);
            this.matrix18.Margin = new System.Windows.Forms.Padding(4);
            this.matrix18.Name = "matrix18";
            this.matrix18.Size = new System.Drawing.Size(29, 22);
            this.matrix18.TabIndex = 63;
            this.matrix18.Text = "0";
            // 
            // matrix17
            // 
            this.matrix17.Location = new System.Drawing.Point(1446, 452);
            this.matrix17.Margin = new System.Windows.Forms.Padding(4);
            this.matrix17.Name = "matrix17";
            this.matrix17.Size = new System.Drawing.Size(29, 22);
            this.matrix17.TabIndex = 64;
            this.matrix17.Text = "0";
            // 
            // matrix16
            // 
            this.matrix16.Location = new System.Drawing.Point(1408, 452);
            this.matrix16.Margin = new System.Windows.Forms.Padding(4);
            this.matrix16.Name = "matrix16";
            this.matrix16.Size = new System.Drawing.Size(29, 22);
            this.matrix16.TabIndex = 65;
            this.matrix16.Text = "0";
            // 
            // matrix23
            // 
            this.matrix23.Location = new System.Drawing.Point(1485, 484);
            this.matrix23.Margin = new System.Windows.Forms.Padding(4);
            this.matrix23.Name = "matrix23";
            this.matrix23.Size = new System.Drawing.Size(29, 22);
            this.matrix23.TabIndex = 66;
            this.matrix23.Text = "0";
            // 
            // matrix22
            // 
            this.matrix22.Location = new System.Drawing.Point(1446, 484);
            this.matrix22.Margin = new System.Windows.Forms.Padding(4);
            this.matrix22.Name = "matrix22";
            this.matrix22.Size = new System.Drawing.Size(29, 22);
            this.matrix22.TabIndex = 67;
            this.matrix22.Text = "0";
            // 
            // matrix21
            // 
            this.matrix21.Location = new System.Drawing.Point(1408, 484);
            this.matrix21.Margin = new System.Windows.Forms.Padding(4);
            this.matrix21.Name = "matrix21";
            this.matrix21.Size = new System.Drawing.Size(29, 22);
            this.matrix21.TabIndex = 68;
            this.matrix21.Text = "0";
            // 
            // matrix19
            // 
            this.matrix19.Location = new System.Drawing.Point(1524, 452);
            this.matrix19.Margin = new System.Windows.Forms.Padding(4);
            this.matrix19.Name = "matrix19";
            this.matrix19.Size = new System.Drawing.Size(29, 22);
            this.matrix19.TabIndex = 69;
            this.matrix19.Text = "0";
            // 
            // matrix14
            // 
            this.matrix14.Location = new System.Drawing.Point(1524, 420);
            this.matrix14.Margin = new System.Windows.Forms.Padding(4);
            this.matrix14.Name = "matrix14";
            this.matrix14.Size = new System.Drawing.Size(29, 22);
            this.matrix14.TabIndex = 70;
            this.matrix14.Text = "0";
            // 
            // matrix9
            // 
            this.matrix9.Location = new System.Drawing.Point(1524, 388);
            this.matrix9.Margin = new System.Windows.Forms.Padding(4);
            this.matrix9.Name = "matrix9";
            this.matrix9.Size = new System.Drawing.Size(29, 22);
            this.matrix9.TabIndex = 71;
            this.matrix9.Text = "0";
            // 
            // matrix20
            // 
            this.matrix20.Location = new System.Drawing.Point(1562, 452);
            this.matrix20.Margin = new System.Windows.Forms.Padding(4);
            this.matrix20.Name = "matrix20";
            this.matrix20.Size = new System.Drawing.Size(29, 22);
            this.matrix20.TabIndex = 72;
            this.matrix20.Text = "0";
            // 
            // matrix15
            // 
            this.matrix15.Location = new System.Drawing.Point(1562, 420);
            this.matrix15.Margin = new System.Windows.Forms.Padding(4);
            this.matrix15.Name = "matrix15";
            this.matrix15.Size = new System.Drawing.Size(29, 22);
            this.matrix15.TabIndex = 73;
            this.matrix15.Text = "0";
            // 
            // matrix10
            // 
            this.matrix10.Location = new System.Drawing.Point(1562, 388);
            this.matrix10.Margin = new System.Windows.Forms.Padding(4);
            this.matrix10.Name = "matrix10";
            this.matrix10.Size = new System.Drawing.Size(29, 22);
            this.matrix10.TabIndex = 74;
            this.matrix10.Text = "0";
            // 
            // matrix5
            // 
            this.matrix5.Location = new System.Drawing.Point(1562, 356);
            this.matrix5.Margin = new System.Windows.Forms.Padding(4);
            this.matrix5.Name = "matrix5";
            this.matrix5.Size = new System.Drawing.Size(29, 22);
            this.matrix5.TabIndex = 75;
            this.matrix5.Text = "0";
            // 
            // matrix24
            // 
            this.matrix24.Location = new System.Drawing.Point(1524, 484);
            this.matrix24.Margin = new System.Windows.Forms.Padding(4);
            this.matrix24.Name = "matrix24";
            this.matrix24.Size = new System.Drawing.Size(29, 22);
            this.matrix24.TabIndex = 76;
            this.matrix24.Text = "0";
            // 
            // matrix25
            // 
            this.matrix25.Location = new System.Drawing.Point(1562, 484);
            this.matrix25.Margin = new System.Windows.Forms.Padding(4);
            this.matrix25.Name = "matrix25";
            this.matrix25.Size = new System.Drawing.Size(29, 22);
            this.matrix25.TabIndex = 77;
            this.matrix25.Text = "0";
            // 
            // outToInButton
            // 
            this.outToInButton.Location = new System.Drawing.Point(1264, 31);
            this.outToInButton.Name = "outToInButton";
            this.outToInButton.Size = new System.Drawing.Size(127, 23);
            this.outToInButton.TabIndex = 78;
            this.outToInButton.Text = "Out to In";
            this.outToInButton.UseVisualStyleBackColor = true;
            this.outToInButton.Click += new System.EventHandler(this.outToInButton_Click);
            // 
            // INFOIBV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1696, 1055);
            this.Controls.Add(this.outToInButton);
            this.Controls.Add(this.matrix25);
            this.Controls.Add(this.matrix24);
            this.Controls.Add(this.matrix5);
            this.Controls.Add(this.matrix10);
            this.Controls.Add(this.matrix15);
            this.Controls.Add(this.matrix20);
            this.Controls.Add(this.matrix9);
            this.Controls.Add(this.matrix14);
            this.Controls.Add(this.matrix19);
            this.Controls.Add(this.matrix21);
            this.Controls.Add(this.matrix22);
            this.Controls.Add(this.matrix23);
            this.Controls.Add(this.matrix16);
            this.Controls.Add(this.matrix17);
            this.Controls.Add(this.matrix18);
            this.Controls.Add(this.matrix4);
            this.Controls.Add(this.matrix3);
            this.Controls.Add(this.matrix7);
            this.Controls.Add(this.matrix8);
            this.Controls.Add(this.matrix11);
            this.Controls.Add(this.matrix12);
            this.Controls.Add(this.matrix13);
            this.Controls.Add(this.matrix2);
            this.Controls.Add(this.matrix6);
            this.Controls.Add(this.matrix1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.valuesLabel);
            this.Controls.Add(this.ValuesBox);
            this.Controls.Add(this.histoOut);
            this.Controls.Add(this.histoIn);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.imageFileName);
            this.Controls.Add(this.LoadImageButton);
            this.Location = new System.Drawing.Point(10, 10);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "INFOIBV";
            this.ShowIcon = false;
            this.Text = "INFOIBV";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.histoIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.histoOut)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadImageButton;
        private System.Windows.Forms.OpenFileDialog openImageDialog;
        private System.Windows.Forms.TextBox imageFileName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.SaveFileDialog saveImageDialog;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataVisualization.Charting.Chart histoIn;
        private System.Windows.Forms.DataVisualization.Charting.Chart histoOut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox ValuesBox;
        private System.Windows.Forms.Label valuesLabel;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox matrix1;
        private System.Windows.Forms.TextBox matrix6;
        private System.Windows.Forms.TextBox matrix2;
        private System.Windows.Forms.TextBox matrix13;
        private System.Windows.Forms.TextBox matrix12;
        private System.Windows.Forms.TextBox matrix11;
        private System.Windows.Forms.TextBox matrix8;
        private System.Windows.Forms.TextBox matrix7;
        private System.Windows.Forms.TextBox matrix3;
        private System.Windows.Forms.TextBox matrix4;
        private System.Windows.Forms.TextBox matrix18;
        private System.Windows.Forms.TextBox matrix17;
        private System.Windows.Forms.TextBox matrix16;
        private System.Windows.Forms.TextBox matrix23;
        private System.Windows.Forms.TextBox matrix22;
        private System.Windows.Forms.TextBox matrix21;
        private System.Windows.Forms.TextBox matrix19;
        private System.Windows.Forms.TextBox matrix14;
        private System.Windows.Forms.TextBox matrix9;
        private System.Windows.Forms.TextBox matrix20;
        private System.Windows.Forms.TextBox matrix15;
        private System.Windows.Forms.TextBox matrix10;
        private System.Windows.Forms.TextBox matrix5;
        private System.Windows.Forms.TextBox matrix24;
        private System.Windows.Forms.TextBox matrix25;
        private System.Windows.Forms.Button outToInButton;
    }
}

