namespace IGTrading
{
    partial class TradeFrm
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
            this.btnGetOpenPositions = new System.Windows.Forms.Button();
            this.btnOpenPosition = new System.Windows.Forms.Button();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStopLevel = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lvPositions = new System.Windows.Forms.ListView();
            this.colPosName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPosDirection = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPosSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPosOpen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPosCurrent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPosDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPosPL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPosGain = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbDirection = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLimitDistance = new System.Windows.Forms.TextBox();
            this.txtLimitLevel = new System.Windows.Forms.TextBox();
            this.txtStopDistance = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClosePosition = new System.Windows.Forms.Button();
            this.txtCloseQty = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnChangePosition = new System.Windows.Forms.Button();
            this.txtUpdateLimitLevel = new System.Windows.Forms.TextBox();
            this.txtUpdateStopLevel = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cbTradePlans = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnStartTradePlan = new System.Windows.Forms.Button();
            this.btnAddTradePlan = new System.Windows.Forms.Button();
            this.btnEditTradePlan = new System.Windows.Forms.Button();
            this.btnDeleteTradePlan = new System.Windows.Forms.Button();
            this.txtTradelimit = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGetOpenPositions
            // 
            this.btnGetOpenPositions.Location = new System.Drawing.Point(15, 12);
            this.btnGetOpenPositions.Name = "btnGetOpenPositions";
            this.btnGetOpenPositions.Size = new System.Drawing.Size(187, 23);
            this.btnGetOpenPositions.TabIndex = 0;
            this.btnGetOpenPositions.Text = "Get Positions";
            this.btnGetOpenPositions.UseVisualStyleBackColor = true;
            this.btnGetOpenPositions.Click += new System.EventHandler(this.btnGetOpenPositions_Click);
            // 
            // btnOpenPosition
            // 
            this.btnOpenPosition.Location = new System.Drawing.Point(22, 222);
            this.btnOpenPosition.Name = "btnOpenPosition";
            this.btnOpenPosition.Size = new System.Drawing.Size(187, 23);
            this.btnOpenPosition.TabIndex = 0;
            this.btnOpenPosition.Text = "Open Position";
            this.btnOpenPosition.UseVisualStyleBackColor = true;
            this.btnOpenPosition.Click += new System.EventHandler(this.btnOpenPosition_Click);
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(82, 19);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(100, 20);
            this.txtQty.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "qty";
            // 
            // txtStopLevel
            // 
            this.txtStopLevel.Location = new System.Drawing.Point(84, 77);
            this.txtStopLevel.Name = "txtStopLevel";
            this.txtStopLevel.Size = new System.Drawing.Size(100, 20);
            this.txtStopLevel.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Stop Level";
            // 
            // lvPositions
            // 
            this.lvPositions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colPosName,
            this.colPosDirection,
            this.colPosSize,
            this.colPosOpen,
            this.colPosCurrent,
            this.colPosDate,
            this.colPosPL,
            this.colPosGain});
            this.lvPositions.Location = new System.Drawing.Point(15, 65);
            this.lvPositions.Name = "lvPositions";
            this.lvPositions.Size = new System.Drawing.Size(758, 97);
            this.lvPositions.TabIndex = 3;
            this.lvPositions.UseCompatibleStateImageBehavior = false;
            this.lvPositions.View = System.Windows.Forms.View.Details;
            this.lvPositions.SelectedIndexChanged += new System.EventHandler(this.lvPositions_SelectedIndexChanged);
            // 
            // colPosName
            // 
            this.colPosName.Text = "Name";
            this.colPosName.Width = 100;
            // 
            // colPosDirection
            // 
            this.colPosDirection.Text = "Direction";
            // 
            // colPosSize
            // 
            this.colPosSize.Text = "Size";
            // 
            // colPosOpen
            // 
            this.colPosOpen.Text = "Open";
            this.colPosOpen.Width = 100;
            // 
            // colPosCurrent
            // 
            this.colPosCurrent.Text = "Current";
            this.colPosCurrent.Width = 100;
            // 
            // colPosDate
            // 
            this.colPosDate.Text = "Date Opened";
            this.colPosDate.Width = 100;
            // 
            // colPosPL
            // 
            this.colPosPL.Text = "Profit/Loss";
            this.colPosPL.Width = 100;
            // 
            // colPosGain
            // 
            this.colPosGain.Text = "Gain";
            this.colPosGain.Width = 100;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Open Positions";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbDirection);
            this.groupBox1.Controls.Add(this.txtQty);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnOpenPosition);
            this.groupBox1.Controls.Add(this.txtTradelimit);
            this.groupBox1.Controls.Add(this.txtLimitDistance);
            this.groupBox1.Controls.Add(this.txtLimitLevel);
            this.groupBox1.Controls.Add(this.txtStopDistance);
            this.groupBox1.Controls.Add(this.txtStopLevel);
            this.groupBox1.Location = new System.Drawing.Point(18, 181);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(235, 251);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Open  Position";
            // 
            // cbDirection
            // 
            this.cbDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDirection.FormattingEnabled = true;
            this.cbDirection.Items.AddRange(new object[] {
            "BUY",
            "SELL"});
            this.cbDirection.Location = new System.Drawing.Point(84, 47);
            this.cbDirection.Name = "cbDirection";
            this.cbDirection.Size = new System.Drawing.Size(98, 21);
            this.cbDirection.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Direction";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 158);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Limit Distance";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Limit Level";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Stop Distance";
            // 
            // txtLimitDistance
            // 
            this.txtLimitDistance.Location = new System.Drawing.Point(84, 155);
            this.txtLimitDistance.Name = "txtLimitDistance";
            this.txtLimitDistance.Size = new System.Drawing.Size(100, 20);
            this.txtLimitDistance.TabIndex = 1;
            // 
            // txtLimitLevel
            // 
            this.txtLimitLevel.Location = new System.Drawing.Point(84, 129);
            this.txtLimitLevel.Name = "txtLimitLevel";
            this.txtLimitLevel.Size = new System.Drawing.Size(100, 20);
            this.txtLimitLevel.TabIndex = 1;
            // 
            // txtStopDistance
            // 
            this.txtStopDistance.Location = new System.Drawing.Point(84, 103);
            this.txtStopDistance.Name = "txtStopDistance";
            this.txtStopDistance.Size = new System.Drawing.Size(100, 20);
            this.txtStopDistance.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClosePosition);
            this.groupBox2.Controls.Add(this.txtCloseQty);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(280, 189);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(216, 89);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Close Position";
            // 
            // btnClosePosition
            // 
            this.btnClosePosition.Location = new System.Drawing.Point(48, 53);
            this.btnClosePosition.Name = "btnClosePosition";
            this.btnClosePosition.Size = new System.Drawing.Size(112, 27);
            this.btnClosePosition.TabIndex = 3;
            this.btnClosePosition.Text = "Close Position";
            this.btnClosePosition.UseVisualStyleBackColor = true;
            this.btnClosePosition.Click += new System.EventHandler(this.btnClosePosition_Click);
            // 
            // txtCloseQty
            // 
            this.txtCloseQty.Location = new System.Drawing.Point(45, 19);
            this.txtCloseQty.Name = "txtCloseQty";
            this.txtCloseQty.Size = new System.Drawing.Size(125, 20);
            this.txtCloseQty.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "qty";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnChangePosition);
            this.groupBox3.Controls.Add(this.txtUpdateLimitLevel);
            this.groupBox3.Controls.Add(this.txtUpdateStopLevel);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Location = new System.Drawing.Point(526, 189);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(213, 127);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Edit Position";
            // 
            // btnChangePosition
            // 
            this.btnChangePosition.Location = new System.Drawing.Point(58, 92);
            this.btnChangePosition.Name = "btnChangePosition";
            this.btnChangePosition.Size = new System.Drawing.Size(113, 23);
            this.btnChangePosition.TabIndex = 3;
            this.btnChangePosition.Text = "Change Position";
            this.btnChangePosition.UseVisualStyleBackColor = true;
            this.btnChangePosition.Click += new System.EventHandler(this.btnChangePosition_Click);
            // 
            // txtUpdateLimitLevel
            // 
            this.txtUpdateLimitLevel.Location = new System.Drawing.Point(83, 53);
            this.txtUpdateLimitLevel.Name = "txtUpdateLimitLevel";
            this.txtUpdateLimitLevel.Size = new System.Drawing.Size(100, 20);
            this.txtUpdateLimitLevel.TabIndex = 1;
            // 
            // txtUpdateStopLevel
            // 
            this.txtUpdateStopLevel.Location = new System.Drawing.Point(83, 22);
            this.txtUpdateStopLevel.Name = "txtUpdateStopLevel";
            this.txtUpdateStopLevel.Size = new System.Drawing.Size(100, 20);
            this.txtUpdateStopLevel.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Stop Level";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Limit Level";
            // 
            // cbTradePlans
            // 
            this.cbTradePlans.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTradePlans.FormattingEnabled = true;
            this.cbTradePlans.Location = new System.Drawing.Point(40, 470);
            this.cbTradePlans.Name = "cbTradePlans";
            this.cbTradePlans.Size = new System.Drawing.Size(213, 21);
            this.cbTradePlans.TabIndex = 7;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(37, 454);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "Trade Plans";
            // 
            // btnStartTradePlan
            // 
            this.btnStartTradePlan.Location = new System.Drawing.Point(40, 497);
            this.btnStartTradePlan.Name = "btnStartTradePlan";
            this.btnStartTradePlan.Size = new System.Drawing.Size(117, 23);
            this.btnStartTradePlan.TabIndex = 9;
            this.btnStartTradePlan.Text = "Start Trade plan";
            this.btnStartTradePlan.UseVisualStyleBackColor = true;
            this.btnStartTradePlan.Click += new System.EventHandler(this.btnStartTradePlan_Click);
            // 
            // btnAddTradePlan
            // 
            this.btnAddTradePlan.Location = new System.Drawing.Point(270, 470);
            this.btnAddTradePlan.Name = "btnAddTradePlan";
            this.btnAddTradePlan.Size = new System.Drawing.Size(105, 23);
            this.btnAddTradePlan.TabIndex = 10;
            this.btnAddTradePlan.Text = "Add Trade Plan";
            this.btnAddTradePlan.UseVisualStyleBackColor = true;
            this.btnAddTradePlan.Click += new System.EventHandler(this.btnAddTradePlan_Click);
            // 
            // btnEditTradePlan
            // 
            this.btnEditTradePlan.Location = new System.Drawing.Point(391, 470);
            this.btnEditTradePlan.Name = "btnEditTradePlan";
            this.btnEditTradePlan.Size = new System.Drawing.Size(105, 23);
            this.btnEditTradePlan.TabIndex = 11;
            this.btnEditTradePlan.Text = "Edit Trade Plan";
            this.btnEditTradePlan.UseVisualStyleBackColor = true;
            this.btnEditTradePlan.Click += new System.EventHandler(this.btnEditTradePlan_Click);
            // 
            // btnDeleteTradePlan
            // 
            this.btnDeleteTradePlan.Location = new System.Drawing.Point(512, 470);
            this.btnDeleteTradePlan.Name = "btnDeleteTradePlan";
            this.btnDeleteTradePlan.Size = new System.Drawing.Size(110, 23);
            this.btnDeleteTradePlan.TabIndex = 12;
            this.btnDeleteTradePlan.Text = "Delete Trade Plan";
            this.btnDeleteTradePlan.UseVisualStyleBackColor = true;
            this.btnDeleteTradePlan.Click += new System.EventHandler(this.btnDeleteTradePlan_Click);
            // 
            // txtTradelimit
            // 
            this.txtTradelimit.Location = new System.Drawing.Point(84, 181);
            this.txtTradelimit.Name = "txtTradelimit";
            this.txtTradelimit.Size = new System.Drawing.Size(100, 20);
            this.txtTradelimit.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 184);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Trade Limit";
            // 
            // TradeFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 573);
            this.Controls.Add(this.btnDeleteTradePlan);
            this.Controls.Add(this.btnEditTradePlan);
            this.Controls.Add(this.btnAddTradePlan);
            this.Controls.Add(this.btnStartTradePlan);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cbTradePlans);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lvPositions);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnGetOpenPositions);
            this.Name = "TradeFrm";
            this.Text = "TradeFrm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TradeFrm_FormClosing);
            this.Load += new System.EventHandler(this.TradeFrm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetOpenPositions;
        private System.Windows.Forms.Button btnOpenPosition;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStopLevel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lvPositions;
        private System.Windows.Forms.ColumnHeader colPosName;
        private System.Windows.Forms.ColumnHeader colPosDirection;
        private System.Windows.Forms.ColumnHeader colPosOpen;
        private System.Windows.Forms.ColumnHeader colPosCurrent;
        private System.Windows.Forms.ColumnHeader colPosDate;
        private System.Windows.Forms.ColumnHeader colPosPL;
        private System.Windows.Forms.ColumnHeader colPosSize;
        private System.Windows.Forms.ColumnHeader colPosGain;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbDirection;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtLimitDistance;
        private System.Windows.Forms.TextBox txtLimitLevel;
        private System.Windows.Forms.TextBox txtStopDistance;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClosePosition;
        private System.Windows.Forms.TextBox txtCloseQty;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnChangePosition;
        private System.Windows.Forms.TextBox txtUpdateLimitLevel;
        private System.Windows.Forms.TextBox txtUpdateStopLevel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbTradePlans;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnStartTradePlan;
        private System.Windows.Forms.Button btnAddTradePlan;
        private System.Windows.Forms.Button btnEditTradePlan;
        private System.Windows.Forms.Button btnDeleteTradePlan;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtTradelimit;
    }
}