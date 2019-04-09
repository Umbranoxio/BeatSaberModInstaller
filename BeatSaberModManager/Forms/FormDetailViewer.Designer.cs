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
            MaterialSkin.Controls.MaterialRaisedButton buttonExternalLink;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDetailViewer));
            this.webBrowserDescription = new System.Windows.Forms.WebBrowser();
            this.labelDescription = new MaterialSkin.Controls.MaterialLabel();
            this.buttonClose = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialRaisedButton1 = new MaterialSkin.Controls.MaterialRaisedButton();
            buttonExternalLink = new MaterialSkin.Controls.MaterialRaisedButton();
            this.SuspendLayout();
            // 
            // webBrowserDescription
            // 
            this.webBrowserDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowserDescription.IsWebBrowserContextMenuEnabled = false;
            this.webBrowserDescription.Location = new System.Drawing.Point(12, 97);
            this.webBrowserDescription.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserDescription.Name = "webBrowserDescription";
            this.webBrowserDescription.Size = new System.Drawing.Size(743, 398);
            this.webBrowserDescription.TabIndex = 6;
            this.webBrowserDescription.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowserDescription_DocumentCompleted);
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.BackColor = System.Drawing.Color.Transparent;
            this.labelDescription.Depth = 0;
            this.labelDescription.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelDescription.Location = new System.Drawing.Point(12, 74);
            this.labelDescription.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(90, 19);
            this.labelDescription.TabIndex = 6;
            this.labelDescription.Text = "Description:";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Depth = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Location = new System.Drawing.Point(637, 501);
            this.buttonClose.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Primary = true;
            this.buttonClose.Size = new System.Drawing.Size(118, 35);
            this.buttonClose.TabIndex = 7;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.MaterialRaisedButton1_Click);
            // 
            // materialRaisedButton1
            // 
            this.materialRaisedButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.materialRaisedButton1.Depth = 0;
            this.materialRaisedButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.materialRaisedButton1.Location = new System.Drawing.Point(12, 501);
            this.materialRaisedButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton1.Name = "materialRaisedButton1";
            this.materialRaisedButton1.Primary = true;
            this.materialRaisedButton1.Size = new System.Drawing.Size(156, 35);
            this.materialRaisedButton1.TabIndex = 9;
            this.materialRaisedButton1.Text = "Direct Download";
            this.materialRaisedButton1.UseVisualStyleBackColor = true;
            this.materialRaisedButton1.Click += new System.EventHandler(this.MaterialRaisedButton1_Click_1);
            // 
            // buttonExternalLink
            // 
            buttonExternalLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            buttonExternalLink.Depth = 0;
            buttonExternalLink.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonExternalLink.Location = new System.Drawing.Point(174, 501);
            buttonExternalLink.MouseState = MaterialSkin.MouseState.HOVER;
            buttonExternalLink.Name = "buttonExternalLink";
            buttonExternalLink.Primary = true;
            buttonExternalLink.Size = new System.Drawing.Size(118, 35);
            buttonExternalLink.TabIndex = 10;
            buttonExternalLink.Text = "More Info";
            buttonExternalLink.UseVisualStyleBackColor = true;
            buttonExternalLink.Click += new System.EventHandler(this.buttonExternalLink_Click);
            // 
            // FormDetailViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 548);
            this.Controls.Add(buttonExternalLink);
            this.Controls.Add(this.materialRaisedButton1);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.webBrowserDescription);
            this.Controls.Add(this.labelDescription);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDetailViewer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mod Name by Author Name Version";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.WebBrowser webBrowserDescription;
        private MaterialSkin.Controls.MaterialLabel labelDescription;
        private MaterialSkin.Controls.MaterialRaisedButton buttonClose;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton1;
    }
}