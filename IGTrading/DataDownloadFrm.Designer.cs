namespace IGTrading
{
    partial class DataDownloadFrm
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
            this.dtDownloadTo = new System.Windows.Forms.DateTimePicker();
            this.dtDownloadFrom = new System.Windows.Forms.DateTimePicker();
            this.btnDownloadPriceHist = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblEpic = new System.Windows.Forms.Label();
            this.btnGetLastDate = new System.Windows.Forms.Button();
            this.btnGetFirstDate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbResolution = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // dtDownloadTo
            // 
            this.dtDownloadTo.CustomFormat = "dd MMM yyyy HH:mm:ss";
            this.dtDownloadTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDownloadTo.Location = new System.Drawing.Point(151, 64);
            this.dtDownloadTo.Name = "dtDownloadTo";
            this.dtDownloadTo.Size = new System.Drawing.Size(161, 20);
            this.dtDownloadTo.TabIndex = 11;
            // 
            // dtDownloadFrom
            // 
            this.dtDownloadFrom.CustomFormat = "dd MMM yyyy HH:mm:ss";
            this.dtDownloadFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDownloadFrom.Location = new System.Drawing.Point(151, 38);
            this.dtDownloadFrom.Name = "dtDownloadFrom";
            this.dtDownloadFrom.Size = new System.Drawing.Size(161, 20);
            this.dtDownloadFrom.TabIndex = 10;
            // 
            // btnDownloadPriceHist
            // 
            this.btnDownloadPriceHist.Location = new System.Drawing.Point(146, 134);
            this.btnDownloadPriceHist.Name = "btnDownloadPriceHist";
            this.btnDownloadPriceHist.Size = new System.Drawing.Size(107, 23);
            this.btnDownloadPriceHist.TabIndex = 9;
            this.btnDownloadPriceHist.Text = "Download";
            this.btnDownloadPriceHist.UseVisualStyleBackColor = true;
            this.btnDownloadPriceHist.Click += new System.EventHandler(this.btnDownloadPriceHist_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(275, 134);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 23);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Date From";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(89, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Date To";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Selected Epic";
            // 
            // lblEpic
            // 
            this.lblEpic.AutoSize = true;
            this.lblEpic.Location = new System.Drawing.Point(151, 10);
            this.lblEpic.Name = "lblEpic";
            this.lblEpic.Size = new System.Drawing.Size(27, 13);
            this.lblEpic.TabIndex = 15;
            this.lblEpic.Text = "epic";
            // 
            // btnGetLastDate
            // 
            this.btnGetLastDate.Location = new System.Drawing.Point(333, 37);
            this.btnGetLastDate.Name = "btnGetLastDate";
            this.btnGetLastDate.Size = new System.Drawing.Size(98, 23);
            this.btnGetLastDate.TabIndex = 16;
            this.btnGetLastDate.Text = "Get Last date";
            this.btnGetLastDate.UseVisualStyleBackColor = true;
            this.btnGetLastDate.Click += new System.EventHandler(this.btnGetLastDate_Click);
            // 
            // btnGetFirstDate
            // 
            this.btnGetFirstDate.Location = new System.Drawing.Point(333, 61);
            this.btnGetFirstDate.Name = "btnGetFirstDate";
            this.btnGetFirstDate.Size = new System.Drawing.Size(98, 23);
            this.btnGetFirstDate.TabIndex = 16;
            this.btnGetFirstDate.Text = "Get First date";
            this.btnGetFirstDate.UseVisualStyleBackColor = true;
            this.btnGetFirstDate.Click += new System.EventHandler(this.btnGetFirstDate_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(89, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Resolution";
            // 
            // cbResolution
            // 
            this.cbResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbResolution.FormattingEnabled = true;
            this.cbResolution.Location = new System.Drawing.Point(151, 90);
            this.cbResolution.Name = "cbResolution";
            this.cbResolution.Size = new System.Drawing.Size(161, 21);
            this.cbResolution.TabIndex = 21;
            // 
            // DataDownloadFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 187);
            this.ControlBox = false;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbResolution);
            this.Controls.Add(this.btnGetFirstDate);
            this.Controls.Add(this.btnGetLastDate);
            this.Controls.Add(this.lblEpic);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dtDownloadTo);
            this.Controls.Add(this.dtDownloadFrom);
            this.Controls.Add(this.btnDownloadPriceHist);
            this.MinimizeBox = false;
            this.Name = "DataDownloadFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Data Download";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtDownloadTo;
        private System.Windows.Forms.DateTimePicker dtDownloadFrom;
        private System.Windows.Forms.Button btnDownloadPriceHist;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblEpic;
        private System.Windows.Forms.Button btnGetLastDate;
        private System.Windows.Forms.Button btnGetFirstDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbResolution;
    }
}