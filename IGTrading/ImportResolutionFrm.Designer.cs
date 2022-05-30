namespace IGTrading
{
    partial class ImportResolutionFrm
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
            this.cbResolution = new System.Windows.Forms.ComboBox();
            this.lblEpic = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.dtDownloadTo = new System.Windows.Forms.DateTimePicker();
            this.dtDownloadFrom = new System.Windows.Forms.DateTimePicker();
            this.btnImport = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.chkDataStream = new System.Windows.Forms.CheckBox();
            this.cbSourceResolution = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbResolution
            // 
            this.cbResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbResolution.FormattingEnabled = true;
            this.cbResolution.Location = new System.Drawing.Point(136, 137);
            this.cbResolution.Name = "cbResolution";
            this.cbResolution.Size = new System.Drawing.Size(161, 21);
            this.cbResolution.TabIndex = 0;
            // 
            // lblEpic
            // 
            this.lblEpic.AutoSize = true;
            this.lblEpic.Location = new System.Drawing.Point(136, 22);
            this.lblEpic.Name = "lblEpic";
            this.lblEpic.Size = new System.Drawing.Size(27, 13);
            this.lblEpic.TabIndex = 23;
            this.lblEpic.Text = "epic";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Selected Epic";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(74, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Date To";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Date From";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(238, 212);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 23);
            this.btnClose.TabIndex = 19;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // dtDownloadTo
            // 
            this.dtDownloadTo.CustomFormat = "dd MMM yyyy HH:mm:ss";
            this.dtDownloadTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDownloadTo.Location = new System.Drawing.Point(136, 80);
            this.dtDownloadTo.Name = "dtDownloadTo";
            this.dtDownloadTo.Size = new System.Drawing.Size(161, 20);
            this.dtDownloadTo.TabIndex = 18;
            // 
            // dtDownloadFrom
            // 
            this.dtDownloadFrom.CustomFormat = "dd MMM yyyy HH:mm:ss";
            this.dtDownloadFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDownloadFrom.Location = new System.Drawing.Point(136, 50);
            this.dtDownloadFrom.Name = "dtDownloadFrom";
            this.dtDownloadFrom.Size = new System.Drawing.Size(161, 20);
            this.dtDownloadFrom.TabIndex = 17;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(109, 212);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(107, 23);
            this.btnImport.TabIndex = 16;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Target Resolution";
            // 
            // chkDataStream
            // 
            this.chkDataStream.AutoSize = true;
            this.chkDataStream.Location = new System.Drawing.Point(136, 175);
            this.chkDataStream.Name = "chkDataStream";
            this.chkDataStream.Size = new System.Drawing.Size(137, 17);
            this.chkDataStream.TabIndex = 24;
            this.chkDataStream.Text = "Import from DataStream";
            this.chkDataStream.UseVisualStyleBackColor = true;
            // 
            // cbSourceResolution
            // 
            this.cbSourceResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSourceResolution.FormattingEnabled = true;
            this.cbSourceResolution.Location = new System.Drawing.Point(136, 108);
            this.cbSourceResolution.Name = "cbSourceResolution";
            this.cbSourceResolution.Size = new System.Drawing.Size(161, 21);
            this.cbSourceResolution.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Source Resolution";
            // 
            // ImportResolutionFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 261);
            this.ControlBox = false;
            this.Controls.Add(this.chkDataStream);
            this.Controls.Add(this.lblEpic);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dtDownloadTo);
            this.Controls.Add(this.dtDownloadFrom);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.cbSourceResolution);
            this.Controls.Add(this.cbResolution);
            this.Name = "ImportResolutionFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import Resolution";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbResolution;
        private System.Windows.Forms.Label lblEpic;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DateTimePicker dtDownloadTo;
        private System.Windows.Forms.DateTimePicker dtDownloadFrom;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkDataStream;
        private System.Windows.Forms.ComboBox cbSourceResolution;
        private System.Windows.Forms.Label label5;
    }
}