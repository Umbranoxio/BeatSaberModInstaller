namespace BeatSaberModManager
{
    partial class FormDetailViewer
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.linkLabelDirectLink = new System.Windows.Forms.LinkLabel();
            this.buttonClose = new System.Windows.Forms.Button();
            this.labelDescription = new System.Windows.Forms.Label();
            this.panelDescription = new System.Windows.Forms.Panel();
            this.webBrowserDescription = new System.Windows.Forms.WebBrowser();
            this.panelDescription.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(11, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(250, 20);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Mod Name by Author Name Version";
            // 
            // linkLabelDirectLink
            // 
            this.linkLabelDirectLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabelDirectLink.AutoSize = true;
            this.linkLabelDirectLink.Location = new System.Drawing.Point(12, 454);
            this.linkLabelDirectLink.Name = "linkLabelDirectLink";
            this.linkLabelDirectLink.Size = new System.Drawing.Size(94, 13);
            this.linkLabelDirectLink.TabIndex = 3;
            this.linkLabelDirectLink.TabStop = true;
            this.linkLabelDirectLink.Text = "Direct Download";
            this.linkLabelDirectLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelDirectLink_LinkClicked);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(548, 449);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 4;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(12, 32);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(69, 13);
            this.labelDescription.TabIndex = 2;
            this.labelDescription.Text = "Description:";
            // 
            // panelDescription
            // 
            this.panelDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDescription.Controls.Add(this.webBrowserDescription);
            this.panelDescription.Location = new System.Drawing.Point(15, 48);
            this.panelDescription.Name = "panelDescription";
            this.panelDescription.Size = new System.Drawing.Size(607, 395);
            this.panelDescription.TabIndex = 5;
            // 
            // webBrowserDescription
            // 
            this.webBrowserDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserDescription.IsWebBrowserContextMenuEnabled = false;
            this.webBrowserDescription.Location = new System.Drawing.Point(0, 0);
            this.webBrowserDescription.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserDescription.Name = "webBrowserDescription";
            this.webBrowserDescription.Size = new System.Drawing.Size(605, 393);
            this.webBrowserDescription.TabIndex = 6;
            this.webBrowserDescription.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowserDescription_DocumentCompleted);
            // 
            // FormDetailViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 479);
            this.Controls.Add(this.panelDescription);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.linkLabelDirectLink);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDetailViewer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panelDescription.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.LinkLabel linkLabelDirectLink;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Panel panelDescription;
        private System.Windows.Forms.WebBrowser webBrowserDescription;
    }
}