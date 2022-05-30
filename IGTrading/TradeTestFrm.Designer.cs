namespace IGTrading
{
    partial class TradeTestFrm
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
            this.btnStartTest = new System.Windows.Forms.Button();
            this.txtEpic = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgTurningPoints = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.cbResolution = new System.Windows.Forms.ComboBox();
            this.btnShowChart = new System.Windows.Forms.Button();
            this.txtMajorMove = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMinorMove = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cbTradePlans = new System.Windows.Forms.ComboBox();
            this.btnRefreshTradePlans = new System.Windows.Forms.Button();
            this.btnEditTradePlan = new System.Windows.Forms.Button();
            this.chkUseStreamData = new System.Windows.Forms.CheckBox();
            this.lvPosList = new System.Windows.Forms.ListView();
            this.colPosDealRef = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPosDir = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPosType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPosCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPosDt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPosPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPosGain = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPosPL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtLotSize = new System.Windows.Forms.TextBox();
            this.txtTotalGain = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTotalProfit = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnAnalyzeTurnPoints = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.txtAnalyzeMovement = new System.Windows.Forms.TextBox();
            this.txtMA = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.chkAnalyzeTrendOnly = new System.Windows.Forms.CheckBox();
            this.chkAnalyzeWithMajorPullback = new System.Windows.Forms.CheckBox();
            this.txtPullbackPercRequired = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgTurningPoints)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStartTest
            // 
            this.btnStartTest.Location = new System.Drawing.Point(473, 176);
            this.btnStartTest.Name = "btnStartTest";
            this.btnStartTest.Size = new System.Drawing.Size(75, 23);
            this.btnStartTest.TabIndex = 0;
            this.btnStartTest.Text = "Start Test";
            this.btnStartTest.UseVisualStyleBackColor = true;
            this.btnStartTest.Click += new System.EventHandler(this.btnStartTest_Click);
            // 
            // txtEpic
            // 
            this.txtEpic.Location = new System.Drawing.Point(66, 12);
            this.txtEpic.Name = "txtEpic";
            this.txtEpic.Size = new System.Drawing.Size(131, 20);
            this.txtEpic.TabIndex = 1;
            this.txtEpic.Text = "IX.D.SAF.IFMM.IP";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Epic";
            // 
            // dgTurningPoints
            // 
            this.dgTurningPoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTurningPoints.Location = new System.Drawing.Point(33, 145);
            this.dgTurningPoints.Name = "dgTurningPoints";
            this.dgTurningPoints.Size = new System.Drawing.Size(371, 425);
            this.dgTurningPoints.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Turning Points";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(257, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Resolution";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "End Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Start Date";
            // 
            // dtEnd
            // 
            this.dtEnd.CustomFormat = "dd MMM yyyy HH:mm:ss";
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnd.Location = new System.Drawing.Point(66, 62);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(161, 20);
            this.dtEnd.TabIndex = 24;
            // 
            // dtStart
            // 
            this.dtStart.CustomFormat = "dd MMM yyyy HH:mm:ss";
            this.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStart.Location = new System.Drawing.Point(66, 36);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(161, 20);
            this.dtStart.TabIndex = 23;
            // 
            // cbResolution
            // 
            this.cbResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbResolution.FormattingEnabled = true;
            this.cbResolution.Location = new System.Drawing.Point(319, 12);
            this.cbResolution.Name = "cbResolution";
            this.cbResolution.Size = new System.Drawing.Size(161, 21);
            this.cbResolution.TabIndex = 22;
            // 
            // btnShowChart
            // 
            this.btnShowChart.Location = new System.Drawing.Point(319, 114);
            this.btnShowChart.Name = "btnShowChart";
            this.btnShowChart.Size = new System.Drawing.Size(75, 23);
            this.btnShowChart.TabIndex = 29;
            this.btnShowChart.Text = "Show Chart";
            this.btnShowChart.UseVisualStyleBackColor = true;
            this.btnShowChart.Click += new System.EventHandler(this.btnShowChart_Click);
            // 
            // txtMajorMove
            // 
            this.txtMajorMove.Location = new System.Drawing.Point(592, 14);
            this.txtMajorMove.Name = "txtMajorMove";
            this.txtMajorMove.Size = new System.Drawing.Size(131, 20);
            this.txtMajorMove.TabIndex = 1;
            this.txtMajorMove.Text = "500";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(523, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Major Move";
            // 
            // txtMinorMove
            // 
            this.txtMinorMove.Location = new System.Drawing.Point(818, 16);
            this.txtMinorMove.Name = "txtMinorMove";
            this.txtMinorMove.Size = new System.Drawing.Size(131, 20);
            this.txtMinorMove.TabIndex = 1;
            this.txtMinorMove.Text = "0.0008";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(739, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Minor Move";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(470, 124);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 13);
            this.label11.TabIndex = 31;
            this.label11.Text = "Trade Plans";
            // 
            // cbTradePlans
            // 
            this.cbTradePlans.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTradePlans.FormattingEnabled = true;
            this.cbTradePlans.Location = new System.Drawing.Point(473, 140);
            this.cbTradePlans.Name = "cbTradePlans";
            this.cbTradePlans.Size = new System.Drawing.Size(240, 21);
            this.cbTradePlans.TabIndex = 30;
            // 
            // btnRefreshTradePlans
            // 
            this.btnRefreshTradePlans.Location = new System.Drawing.Point(734, 138);
            this.btnRefreshTradePlans.Name = "btnRefreshTradePlans";
            this.btnRefreshTradePlans.Size = new System.Drawing.Size(92, 23);
            this.btnRefreshTradePlans.TabIndex = 32;
            this.btnRefreshTradePlans.Text = "Refresh";
            this.btnRefreshTradePlans.UseVisualStyleBackColor = true;
            this.btnRefreshTradePlans.Click += new System.EventHandler(this.btnRefreshTradePlans_Click);
            // 
            // btnEditTradePlan
            // 
            this.btnEditTradePlan.Location = new System.Drawing.Point(842, 138);
            this.btnEditTradePlan.Name = "btnEditTradePlan";
            this.btnEditTradePlan.Size = new System.Drawing.Size(91, 23);
            this.btnEditTradePlan.TabIndex = 33;
            this.btnEditTradePlan.Text = "Edit";
            this.btnEditTradePlan.UseVisualStyleBackColor = true;
            this.btnEditTradePlan.Click += new System.EventHandler(this.btnEditTradePlan_Click);
            // 
            // chkUseStreamData
            // 
            this.chkUseStreamData.AutoSize = true;
            this.chkUseStreamData.Location = new System.Drawing.Point(571, 179);
            this.chkUseStreamData.Name = "chkUseStreamData";
            this.chkUseStreamData.Size = new System.Drawing.Size(107, 17);
            this.chkUseStreamData.TabIndex = 34;
            this.chkUseStreamData.Text = "Use Stream Data";
            this.chkUseStreamData.UseVisualStyleBackColor = true;
            // 
            // lvPosList
            // 
            this.lvPosList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colPosDealRef,
            this.colPosDir,
            this.colPosType,
            this.colPosCount,
            this.colPosDt,
            this.colPosPrice,
            this.colPosGain,
            this.colPosPL});
            this.lvPosList.Location = new System.Drawing.Point(434, 285);
            this.lvPosList.Name = "lvPosList";
            this.lvPosList.Size = new System.Drawing.Size(616, 322);
            this.lvPosList.TabIndex = 35;
            this.lvPosList.UseCompatibleStateImageBehavior = false;
            this.lvPosList.View = System.Windows.Forms.View.Details;
            // 
            // colPosDealRef
            // 
            this.colPosDealRef.Text = "Deal Ref";
            // 
            // colPosDir
            // 
            this.colPosDir.Text = "Direction";
            // 
            // colPosType
            // 
            this.colPosType.Text = "Type";
            // 
            // colPosCount
            // 
            this.colPosCount.Text = "Count";
            // 
            // colPosDt
            // 
            this.colPosDt.Text = "DateTime";
            this.colPosDt.Width = 100;
            // 
            // colPosPrice
            // 
            this.colPosPrice.Text = "Price";
            // 
            // colPosGain
            // 
            this.colPosGain.Text = "Gain";
            // 
            // colPosPL
            // 
            this.colPosPL.Text = "Profit Loss";
            this.colPosPL.Width = 100;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(431, 269);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 36;
            this.label8.Text = "Position List";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(710, 181);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "Lot Size";
            // 
            // txtLotSize
            // 
            this.txtLotSize.Location = new System.Drawing.Point(761, 179);
            this.txtLotSize.Name = "txtLotSize";
            this.txtLotSize.Size = new System.Drawing.Size(115, 20);
            this.txtLotSize.TabIndex = 1;
            this.txtLotSize.Text = "1";
            // 
            // txtTotalGain
            // 
            this.txtTotalGain.Location = new System.Drawing.Point(493, 237);
            this.txtTotalGain.Name = "txtTotalGain";
            this.txtTotalGain.Size = new System.Drawing.Size(104, 20);
            this.txtTotalGain.TabIndex = 1;
            this.txtTotalGain.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(431, 239);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 36;
            this.label10.Text = "Total Gain";
            // 
            // txtTotalProfit
            // 
            this.txtTotalProfit.Location = new System.Drawing.Point(675, 238);
            this.txtTotalProfit.Name = "txtTotalProfit";
            this.txtTotalProfit.Size = new System.Drawing.Size(115, 20);
            this.txtTotalProfit.TabIndex = 1;
            this.txtTotalProfit.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(611, 241);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 13);
            this.label12.TabIndex = 36;
            this.label12.Text = "Total Profit";
            // 
            // btnAnalyzeTurnPoints
            // 
            this.btnAnalyzeTurnPoints.Location = new System.Drawing.Point(153, 114);
            this.btnAnalyzeTurnPoints.Name = "btnAnalyzeTurnPoints";
            this.btnAnalyzeTurnPoints.Size = new System.Drawing.Size(134, 23);
            this.btnAnalyzeTurnPoints.TabIndex = 37;
            this.btnAnalyzeTurnPoints.Text = "Analyze TurnPoints";
            this.btnAnalyzeTurnPoints.UseVisualStyleBackColor = true;
            this.btnAnalyzeTurnPoints.Click += new System.EventHandler(this.btnAnalyzeTurnPoints_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(47, 581);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(79, 13);
            this.label13.TabIndex = 38;
            this.label13.Text = "Avg Movement";
            // 
            // txtAnalyzeMovement
            // 
            this.txtAnalyzeMovement.Location = new System.Drawing.Point(132, 578);
            this.txtAnalyzeMovement.Name = "txtAnalyzeMovement";
            this.txtAnalyzeMovement.Size = new System.Drawing.Size(114, 20);
            this.txtAnalyzeMovement.TabIndex = 39;
            // 
            // txtMA
            // 
            this.txtMA.Location = new System.Drawing.Point(592, 40);
            this.txtMA.Name = "txtMA";
            this.txtMA.Size = new System.Drawing.Size(131, 20);
            this.txtMA.TabIndex = 1;
            this.txtMA.Text = "18";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(501, 43);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(85, 13);
            this.label14.TabIndex = 2;
            this.label14.Text = "Moving Average";
            // 
            // chkAnalyzeTrendOnly
            // 
            this.chkAnalyzeTrendOnly.AutoSize = true;
            this.chkAnalyzeTrendOnly.Location = new System.Drawing.Point(302, 43);
            this.chkAnalyzeTrendOnly.Name = "chkAnalyzeTrendOnly";
            this.chkAnalyzeTrendOnly.Size = new System.Drawing.Size(118, 17);
            this.chkAnalyzeTrendOnly.TabIndex = 40;
            this.chkAnalyzeTrendOnly.Text = "Analyze Trend Only";
            this.chkAnalyzeTrendOnly.UseVisualStyleBackColor = true;
            // 
            // chkAnalyzeWithMajorPullback
            // 
            this.chkAnalyzeWithMajorPullback.AutoSize = true;
            this.chkAnalyzeWithMajorPullback.Checked = true;
            this.chkAnalyzeWithMajorPullback.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAnalyzeWithMajorPullback.Location = new System.Drawing.Point(302, 65);
            this.chkAnalyzeWithMajorPullback.Name = "chkAnalyzeWithMajorPullback";
            this.chkAnalyzeWithMajorPullback.Size = new System.Drawing.Size(161, 17);
            this.chkAnalyzeWithMajorPullback.TabIndex = 40;
            this.chkAnalyzeWithMajorPullback.Text = "Analyze With Major Pullback";
            this.chkAnalyzeWithMajorPullback.UseVisualStyleBackColor = true;
            // 
            // txtPullbackPercRequired
            // 
            this.txtPullbackPercRequired.Location = new System.Drawing.Point(818, 43);
            this.txtPullbackPercRequired.Name = "txtPullbackPercRequired";
            this.txtPullbackPercRequired.Size = new System.Drawing.Size(131, 20);
            this.txtPullbackPercRequired.TabIndex = 1;
            this.txtPullbackPercRequired.Text = "0.3";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(736, 45);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(76, 13);
            this.label15.TabIndex = 2;
            this.label15.Text = "Pullback Perc ";
            // 
            // TradeTestFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 648);
            this.Controls.Add(this.chkAnalyzeWithMajorPullback);
            this.Controls.Add(this.chkAnalyzeTrendOnly);
            this.Controls.Add(this.txtAnalyzeMovement);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnAnalyzeTurnPoints);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lvPosList);
            this.Controls.Add(this.chkUseStreamData);
            this.Controls.Add(this.btnEditTradePlan);
            this.Controls.Add(this.btnRefreshTradePlans);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cbTradePlans);
            this.Controls.Add(this.btnShowChart);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtEnd);
            this.Controls.Add(this.dtStart);
            this.Controls.Add(this.cbResolution);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgTurningPoints);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTotalProfit);
            this.Controls.Add(this.txtTotalGain);
            this.Controls.Add(this.txtLotSize);
            this.Controls.Add(this.txtPullbackPercRequired);
            this.Controls.Add(this.txtMA);
            this.Controls.Add(this.txtMinorMove);
            this.Controls.Add(this.txtMajorMove);
            this.Controls.Add(this.txtEpic);
            this.Controls.Add(this.btnStartTest);
            this.Name = "TradeTestFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TradeTestFrm";
            this.Load += new System.EventHandler(this.TradeTestFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgTurningPoints)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartTest;
        private System.Windows.Forms.TextBox txtEpic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgTurningPoints;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.DateTimePicker dtStart;
        private System.Windows.Forms.ComboBox cbResolution;
        private System.Windows.Forms.Button btnShowChart;
        private System.Windows.Forms.TextBox txtMajorMove;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMinorMove;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbTradePlans;
        private System.Windows.Forms.Button btnRefreshTradePlans;
        private System.Windows.Forms.Button btnEditTradePlan;
        private System.Windows.Forms.CheckBox chkUseStreamData;
        private System.Windows.Forms.ListView lvPosList;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ColumnHeader colPosDealRef;
        private System.Windows.Forms.ColumnHeader colPosDt;
        private System.Windows.Forms.ColumnHeader colPosPrice;
        private System.Windows.Forms.ColumnHeader colPosGain;
        private System.Windows.Forms.ColumnHeader colPosPL;
        private System.Windows.Forms.ColumnHeader colPosCount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtLotSize;
        private System.Windows.Forms.TextBox txtTotalGain;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTotalProfit;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ColumnHeader colPosDir;
        private System.Windows.Forms.ColumnHeader colPosType;
        private System.Windows.Forms.Button btnAnalyzeTurnPoints;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtAnalyzeMovement;
        private System.Windows.Forms.TextBox txtMA;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox chkAnalyzeTrendOnly;
        private System.Windows.Forms.CheckBox chkAnalyzeWithMajorPullback;
        private System.Windows.Forms.TextBox txtPullbackPercRequired;
        private System.Windows.Forms.Label label15;
    }
}