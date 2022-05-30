namespace IGTrading
{
    partial class MainFrm
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginDemoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginLiveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resolutionImportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tradeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.liveTradeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tradeSimulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataStreamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbEpicList = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnDoMarketSearch = new System.Windows.Forms.Button();
            this.lblLogonStatus = new System.Windows.Forms.Label();
            this.cbDisplayType = new System.Windows.Forms.ComboBox();
            this.lbErrorLog = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lvDataStream = new System.Windows.Forms.ListView();
            this.colDataName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAskPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colBidPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLastUpdated = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.lvAccounts = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFunds = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAccPL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAccMargin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFundsAvailable = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lvPositions = new System.Windows.Forms.ListView();
            this.instrumentDetail1 = new IGTrading.InstrumentDetail();
            this.colAccName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEpic = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.dataToolStripMenuItem,
            this.tradeToolStripMenuItem,
            this.dataStreamToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1167, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginDemoToolStripMenuItem,
            this.loginLiveToolStripMenuItem1,
            this.logoutToolStripMenuItem,
            this.changeAccountToolStripMenuItem});
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.loginToolStripMenuItem.Text = "Account";
            // 
            // loginDemoToolStripMenuItem
            // 
            this.loginDemoToolStripMenuItem.Name = "loginDemoToolStripMenuItem";
            this.loginDemoToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.loginDemoToolStripMenuItem.Text = "LoginDemo";
            this.loginDemoToolStripMenuItem.Click += new System.EventHandler(this.loginDemoToolStripMenuItem_Click);
            // 
            // loginLiveToolStripMenuItem1
            // 
            this.loginLiveToolStripMenuItem1.Name = "loginLiveToolStripMenuItem1";
            this.loginLiveToolStripMenuItem1.Size = new System.Drawing.Size(163, 22);
            this.loginLiveToolStripMenuItem1.Text = "LoginLive";
            this.loginLiveToolStripMenuItem1.Click += new System.EventHandler(this.loginLiveToolStripMenuItem1_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // changeAccountToolStripMenuItem
            // 
            this.changeAccountToolStripMenuItem.Name = "changeAccountToolStripMenuItem";
            this.changeAccountToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.changeAccountToolStripMenuItem.Text = "Change Account";
            this.changeAccountToolStripMenuItem.Click += new System.EventHandler(this.changeAccountToolStripMenuItem_Click);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadToolStripMenuItem,
            this.resolutionImportToolStripMenuItem,
            this.chartToolStripMenuItem});
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.dataToolStripMenuItem.Text = "Data";
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.downloadToolStripMenuItem.Text = "Download";
            this.downloadToolStripMenuItem.Click += new System.EventHandler(this.downloadToolStripMenuItem_Click);
            // 
            // resolutionImportToolStripMenuItem
            // 
            this.resolutionImportToolStripMenuItem.Name = "resolutionImportToolStripMenuItem";
            this.resolutionImportToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.resolutionImportToolStripMenuItem.Text = "Resolution Import";
            this.resolutionImportToolStripMenuItem.Click += new System.EventHandler(this.resolutionImportToolStripMenuItem_Click);
            // 
            // chartToolStripMenuItem
            // 
            this.chartToolStripMenuItem.Name = "chartToolStripMenuItem";
            this.chartToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.chartToolStripMenuItem.Text = "Chart";
            this.chartToolStripMenuItem.Click += new System.EventHandler(this.chartToolStripMenuItem_Click);
            // 
            // tradeToolStripMenuItem
            // 
            this.tradeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.liveTradeToolStripMenuItem,
            this.tradeSimulationToolStripMenuItem});
            this.tradeToolStripMenuItem.Name = "tradeToolStripMenuItem";
            this.tradeToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.tradeToolStripMenuItem.Text = "Trade";
            // 
            // liveTradeToolStripMenuItem
            // 
            this.liveTradeToolStripMenuItem.Name = "liveTradeToolStripMenuItem";
            this.liveTradeToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.liveTradeToolStripMenuItem.Text = "Live Trade";
            this.liveTradeToolStripMenuItem.Click += new System.EventHandler(this.liveTradeToolStripMenuItem_Click);
            // 
            // tradeSimulationToolStripMenuItem
            // 
            this.tradeSimulationToolStripMenuItem.Name = "tradeSimulationToolStripMenuItem";
            this.tradeSimulationToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.tradeSimulationToolStripMenuItem.Text = "Trade Simulation";
            this.tradeSimulationToolStripMenuItem.Click += new System.EventHandler(this.tradeSimulationToolStripMenuItem_Click);
            // 
            // dataStreamToolStripMenuItem
            // 
            this.dataStreamToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.dataStreamToolStripMenuItem.Name = "dataStreamToolStripMenuItem";
            this.dataStreamToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.dataStreamToolStripMenuItem.Text = "Data Stream";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // lbEpicList
            // 
            this.lbEpicList.FormattingEnabled = true;
            this.lbEpicList.Location = new System.Drawing.Point(12, 120);
            this.lbEpicList.Name = "lbEpicList";
            this.lbEpicList.Size = new System.Drawing.Size(290, 446);
            this.lbEpicList.TabIndex = 1;
            this.lbEpicList.SelectedIndexChanged += new System.EventHandler(this.lbEpicList_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Search";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(60, 91);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(182, 20);
            this.txtSearch.TabIndex = 4;
            // 
            // btnDoMarketSearch
            // 
            this.btnDoMarketSearch.Location = new System.Drawing.Point(248, 91);
            this.btnDoMarketSearch.Name = "btnDoMarketSearch";
            this.btnDoMarketSearch.Size = new System.Drawing.Size(54, 23);
            this.btnDoMarketSearch.TabIndex = 5;
            this.btnDoMarketSearch.Text = "GO";
            this.btnDoMarketSearch.UseVisualStyleBackColor = true;
            this.btnDoMarketSearch.Click += new System.EventHandler(this.btnDoMarketSearch_Click);
            // 
            // lblLogonStatus
            // 
            this.lblLogonStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLogonStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogonStatus.Location = new System.Drawing.Point(15, 30);
            this.lblLogonStatus.Name = "lblLogonStatus";
            this.lblLogonStatus.Size = new System.Drawing.Size(1140, 23);
            this.lblLogonStatus.TabIndex = 6;
            this.lblLogonStatus.Text = "Not Logged On";
            this.lblLogonStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbDisplayType
            // 
            this.cbDisplayType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDisplayType.FormattingEnabled = true;
            this.cbDisplayType.Items.AddRange(new object[] {
            "Favourities",
            "Search"});
            this.cbDisplayType.Location = new System.Drawing.Point(12, 58);
            this.cbDisplayType.Name = "cbDisplayType";
            this.cbDisplayType.Size = new System.Drawing.Size(290, 21);
            this.cbDisplayType.TabIndex = 10;
            this.cbDisplayType.SelectedIndexChanged += new System.EventHandler(this.cbDisplayType_SelectedIndexChanged);
            // 
            // lbErrorLog
            // 
            this.lbErrorLog.FormattingEnabled = true;
            this.lbErrorLog.Location = new System.Drawing.Point(321, 602);
            this.lbErrorLog.Name = "lbErrorLog";
            this.lbErrorLog.Size = new System.Drawing.Size(810, 95);
            this.lbErrorLog.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(318, 586);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Log";
            // 
            // timer1
            // 
            this.timer1.Interval = 15000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lvDataStream
            // 
            this.lvDataStream.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDataName,
            this.colAskPrice,
            this.colBidPrice,
            this.colLastUpdated});
            this.lvDataStream.Location = new System.Drawing.Point(321, 310);
            this.lvDataStream.Name = "lvDataStream";
            this.lvDataStream.Size = new System.Drawing.Size(810, 273);
            this.lvDataStream.TabIndex = 13;
            this.lvDataStream.UseCompatibleStateImageBehavior = false;
            this.lvDataStream.View = System.Windows.Forms.View.Details;
            // 
            // colDataName
            // 
            this.colDataName.Text = "Name";
            this.colDataName.Width = 200;
            // 
            // colAskPrice
            // 
            this.colAskPrice.Text = "Ask Price";
            // 
            // colBidPrice
            // 
            this.colBidPrice.Text = "Bid Price";
            // 
            // colLastUpdated
            // 
            this.colLastUpdated.Text = "Last Updated";
            this.colLastUpdated.Width = 150;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(318, 294);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Live Data Stream";
            // 
            // lvAccounts
            // 
            this.lvAccounts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colFunds,
            this.colAccPL,
            this.colAccMargin,
            this.colFundsAvailable});
            this.lvAccounts.Location = new System.Drawing.Point(622, 94);
            this.lvAccounts.Name = "lvAccounts";
            this.lvAccounts.Size = new System.Drawing.Size(509, 70);
            this.lvAccounts.TabIndex = 15;
            this.lvAccounts.UseCompatibleStateImageBehavior = false;
            this.lvAccounts.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Text = "Acc Name";
            this.colName.Width = 100;
            // 
            // colFunds
            // 
            this.colFunds.Text = "TotalFunds";
            this.colFunds.Width = 100;
            // 
            // colAccPL
            // 
            this.colAccPL.Text = "Profit & Loss";
            this.colAccPL.Width = 100;
            // 
            // colAccMargin
            // 
            this.colAccMargin.Text = "Margin";
            this.colAccMargin.Width = 100;
            // 
            // colFundsAvailable
            // 
            this.colFundsAvailable.Text = "Funds Available";
            this.colFundsAvailable.Width = 100;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(622, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Account Details";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(318, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Position Details";
            // 
            // lvPositions
            // 
            this.lvPositions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colAccName,
            this.colEpic,
            this.colSize});
            this.lvPositions.Location = new System.Drawing.Point(321, 207);
            this.lvPositions.Name = "lvPositions";
            this.lvPositions.Size = new System.Drawing.Size(810, 84);
            this.lvPositions.TabIndex = 15;
            this.lvPositions.UseCompatibleStateImageBehavior = false;
            this.lvPositions.View = System.Windows.Forms.View.Details;
            // 
            // instrumentDetail1
            // 
            this.instrumentDetail1.Location = new System.Drawing.Point(321, 78);
            this.instrumentDetail1.Name = "instrumentDetail1";
            this.instrumentDetail1.Size = new System.Drawing.Size(295, 110);
            this.instrumentDetail1.TabIndex = 3;
            // 
            // colAccName
            // 
            this.colAccName.Text = "Acc Name";
            this.colAccName.Width = 100;
            // 
            // colEpic
            // 
            this.colEpic.Text = "Epic";
            // 
            // colSize
            // 
            this.colSize.Text = "Size";
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 719);
            this.Controls.Add(this.lvPositions);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lvAccounts);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lvDataStream);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbErrorLog);
            this.Controls.Add(this.cbDisplayType);
            this.Controls.Add(this.lblLogonStatus);
            this.Controls.Add(this.btnDoMarketSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.instrumentDetail1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbEpicList);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IG Trading";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFrm_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginLiveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginDemoToolStripMenuItem;
        private System.Windows.Forms.ListBox lbEpicList;
        private System.Windows.Forms.Label label2;
        private InstrumentDetail instrumentDetail1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnDoMarketSearch;
        private System.Windows.Forms.Label lblLogonStatus;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resolutionImportToolStripMenuItem;
        private System.Windows.Forms.ComboBox cbDisplayType;
        private System.Windows.Forms.ToolStripMenuItem chartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tradeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem liveTradeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tradeSimulationToolStripMenuItem;
        private System.Windows.Forms.ListBox lbErrorLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem dataStreamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ListView lvDataStream;
        private System.Windows.Forms.ColumnHeader colDataName;
        private System.Windows.Forms.ColumnHeader colAskPrice;
        private System.Windows.Forms.ColumnHeader colBidPrice;
        private System.Windows.Forms.ColumnHeader colLastUpdated;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lvAccounts;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListView lvPositions;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colFunds;
        private System.Windows.Forms.ColumnHeader colAccPL;
        private System.Windows.Forms.ColumnHeader colAccMargin;
        private System.Windows.Forms.ColumnHeader colFundsAvailable;
        private System.Windows.Forms.ColumnHeader colAccName;
        private System.Windows.Forms.ColumnHeader colEpic;
        private System.Windows.Forms.ColumnHeader colSize;
    }
}

