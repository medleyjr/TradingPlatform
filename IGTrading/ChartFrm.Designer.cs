namespace IGTrading
{
    partial class ChartFrm
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
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, "5000,5500,5100,5300");
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBarDt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBarValue = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnMaxYup = new System.Windows.Forms.Button();
            this.btnMaxYdown = new System.Windows.Forms.Button();
            this.btnMinYdown = new System.Windows.Forms.Button();
            this.btnMinYup = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dtDownloadFrom = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtDownloadTo = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.cbResolution = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtVolume = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart1.BorderlineWidth = 0;
            chartArea1.AxisX.LabelAutoFitMaxFontSize = 9;
            chartArea1.AxisX.LabelStyle.Interval = 0D;
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            chartArea1.AxisX.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.ScaleView.MinSize = 2000D;
            chartArea1.AxisY.ScaleView.Position = 2000D;
            chartArea1.AxisY.ScaleView.SmallScrollMinSize = 2000D;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(12, 41);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.CustomProperties = "PriceDownColor=Red, PriceUpColor=Green";
            series1.EmptyPointStyle.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.Points.Add(dataPoint1);
            series1.YValuesPerPoint = 4;
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(1112, 508);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            this.chart1.AxisViewChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this.chart1_AxisViewChanged);
            this.chart1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Datetime";
            // 
            // txtBarDt
            // 
            this.txtBarDt.Location = new System.Drawing.Point(65, 8);
            this.txtBarDt.Name = "txtBarDt";
            this.txtBarDt.Size = new System.Drawing.Size(218, 20);
            this.txtBarDt.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Value";
            // 
            // txtBarValue
            // 
            this.txtBarValue.Location = new System.Drawing.Point(65, 34);
            this.txtBarValue.Name = "txtBarValue";
            this.txtBarValue.Size = new System.Drawing.Size(218, 20);
            this.txtBarValue.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btnMaxYup);
            this.panel1.Controls.Add(this.btnMaxYdown);
            this.panel1.Controls.Add(this.btnMinYdown);
            this.panel1.Controls.Add(this.btnMinYup);
            this.panel1.Controls.Add(this.txtVolume);
            this.panel1.Controls.Add(this.txtBarValue);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtBarDt);
            this.panel1.Location = new System.Drawing.Point(12, 566);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1112, 61);
            this.panel1.TabIndex = 3;
            // 
            // btnMaxYup
            // 
            this.btnMaxYup.Location = new System.Drawing.Point(436, 35);
            this.btnMaxYup.Name = "btnMaxYup";
            this.btnMaxYup.Size = new System.Drawing.Size(98, 23);
            this.btnMaxYup.TabIndex = 3;
            this.btnMaxYup.Text = "Max Y Up";
            this.btnMaxYup.UseVisualStyleBackColor = true;
            this.btnMaxYup.Click += new System.EventHandler(this.btnMaxYup_Click);
            // 
            // btnMaxYdown
            // 
            this.btnMaxYdown.Location = new System.Drawing.Point(321, 34);
            this.btnMaxYdown.Name = "btnMaxYdown";
            this.btnMaxYdown.Size = new System.Drawing.Size(98, 23);
            this.btnMaxYdown.TabIndex = 3;
            this.btnMaxYdown.Text = "Max Y Down";
            this.btnMaxYdown.UseVisualStyleBackColor = true;
            this.btnMaxYdown.Click += new System.EventHandler(this.btnMaxYdown_Click);
            // 
            // btnMinYdown
            // 
            this.btnMinYdown.Location = new System.Drawing.Point(435, 7);
            this.btnMinYdown.Name = "btnMinYdown";
            this.btnMinYdown.Size = new System.Drawing.Size(98, 23);
            this.btnMinYdown.TabIndex = 3;
            this.btnMinYdown.Text = "Min Y Down";
            this.btnMinYdown.UseVisualStyleBackColor = true;
            this.btnMinYdown.Click += new System.EventHandler(this.btnMinYdown_Click);
            // 
            // btnMinYup
            // 
            this.btnMinYup.Location = new System.Drawing.Point(321, 6);
            this.btnMinYup.Name = "btnMinYup";
            this.btnMinYup.Size = new System.Drawing.Size(98, 23);
            this.btnMinYup.TabIndex = 3;
            this.btnMinYup.Text = "Min Y Up";
            this.btnMinYup.UseVisualStyleBackColor = true;
            this.btnMinYup.Click += new System.EventHandler(this.btnMinYup_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Date From";
            // 
            // dtDownloadFrom
            // 
            this.dtDownloadFrom.CustomFormat = "dd MMM yyyy HH:mm:ss";
            this.dtDownloadFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDownloadFrom.Location = new System.Drawing.Point(75, 9);
            this.dtDownloadFrom.Name = "dtDownloadFrom";
            this.dtDownloadFrom.Size = new System.Drawing.Size(161, 20);
            this.dtDownloadFrom.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(249, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Date To";
            // 
            // dtDownloadTo
            // 
            this.dtDownloadTo.CustomFormat = "dd MMM yyyy HH:mm:ss";
            this.dtDownloadTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDownloadTo.Location = new System.Drawing.Point(311, 9);
            this.dtDownloadTo.Name = "dtDownloadTo";
            this.dtDownloadTo.Size = new System.Drawing.Size(161, 20);
            this.dtDownloadTo.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(478, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Resolution";
            // 
            // cbResolution
            // 
            this.cbResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbResolution.FormattingEnabled = true;
            this.cbResolution.Location = new System.Drawing.Point(540, 9);
            this.cbResolution.Name = "cbResolution";
            this.cbResolution.Size = new System.Drawing.Size(161, 21);
            this.cbResolution.TabIndex = 26;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(718, 9);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 28;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(561, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Volume";
            // 
            // txtVolume
            // 
            this.txtVolume.Location = new System.Drawing.Point(616, 8);
            this.txtVolume.Name = "txtVolume";
            this.txtVolume.Size = new System.Drawing.Size(218, 20);
            this.txtVolume.TabIndex = 2;
            // 
            // ChartFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 635);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbResolution);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtDownloadTo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtDownloadFrom);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chart1);
            this.Name = "ChartFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ChartFrm";
            this.Load += new System.EventHandler(this.ChartFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBarDt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBarValue;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtDownloadFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtDownloadTo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbResolution;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnMaxYdown;
        private System.Windows.Forms.Button btnMinYup;
        private System.Windows.Forms.Button btnMaxYup;
        private System.Windows.Forms.Button btnMinYdown;
        private System.Windows.Forms.TextBox txtVolume;
        private System.Windows.Forms.Label label6;
    }
}