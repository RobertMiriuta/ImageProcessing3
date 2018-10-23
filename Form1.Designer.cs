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
            this.saveButton.Location = new System.Drawing.Point(1264, 14);
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
            "fourier descriptor"});
            this.comboBox1.Location = new System.Drawing.Point(688, 17);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(161, 24);
            this.comboBox1.TabIndex = 8;
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
            // INFOIBV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1696, 1055);
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
    }
}

