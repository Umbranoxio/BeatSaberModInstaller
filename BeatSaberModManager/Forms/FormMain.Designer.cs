namespace BeatSaberModManager
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.textBoxDirectory = new System.Windows.Forms.TextBox();
            this.buttonFolderBrowser = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonInstall = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageCore = new System.Windows.Forms.TabPage();
            this.listViewMods = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderAuthor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageCredits = new System.Windows.Forms.TabPage();
            this.linkLabellolPants = new System.Windows.Forms.LinkLabel();
            this.linkLabelUmbranox = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.labelModSaber2 = new System.Windows.Forms.Label();
            this.linkLabelModSaberLink = new System.Windows.Forms.LinkLabel();
            this.labelModSaber1 = new System.Windows.Forms.Label();
            this.buttonViewInfo = new System.Windows.Forms.Button();
            this.linkLabelContributors = new System.Windows.Forms.LinkLabel();
            this.tabControlMain.SuspendLayout();
            this.tabPageCore.SuspendLayout();
            this.contextMenuStripMain.SuspendLayout();
            this.tabPageCredits.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxDirectory
            // 
            this.textBoxDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDirectory.Enabled = false;
            this.textBoxDirectory.Location = new System.Drawing.Point(10, 25);
            this.textBoxDirectory.Name = "textBoxDirectory";
            this.textBoxDirectory.Size = new System.Drawing.Size(508, 22);
            this.textBoxDirectory.TabIndex = 0;
            // 
            // buttonFolderBrowser
            // 
            this.buttonFolderBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFolderBrowser.Location = new System.Drawing.Point(524, 25);
            this.buttonFolderBrowser.Name = "buttonFolderBrowser";
            this.buttonFolderBrowser.Size = new System.Drawing.Size(26, 23);
            this.buttonFolderBrowser.TabIndex = 1;
            this.buttonFolderBrowser.Text = "..";
            this.buttonFolderBrowser.UseVisualStyleBackColor = true;
            this.buttonFolderBrowser.Click += new System.EventHandler(this.buttonFolderBrowser_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Beat Saber Folder Path:";
            // 
            // buttonInstall
            // 
            this.buttonInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonInstall.Location = new System.Drawing.Point(440, 341);
            this.buttonInstall.Name = "buttonInstall";
            this.buttonInstall.Size = new System.Drawing.Size(112, 23);
            this.buttonInstall.TabIndex = 4;
            this.buttonInstall.Text = "Install / Update";
            this.buttonInstall.UseVisualStyleBackColor = true;
            this.buttonInstall.Click += new System.EventHandler(this.buttonInstall_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStatus.Location = new System.Drawing.Point(7, 346);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(263, 145);
            this.labelStatus.TabIndex = 5;
            this.labelStatus.Text = "Status: Null";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPageCore);
            this.tabControlMain.Controls.Add(this.tabPageCredits);
            this.tabControlMain.Enabled = false;
            this.tabControlMain.Location = new System.Drawing.Point(10, 53);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(544, 282);
            this.tabControlMain.TabIndex = 8;
            // 
            // tabPageCore
            // 
            this.tabPageCore.Controls.Add(this.listViewMods);
            this.tabPageCore.Location = new System.Drawing.Point(4, 22);
            this.tabPageCore.Name = "tabPageCore";
            this.tabPageCore.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCore.Size = new System.Drawing.Size(536, 256);
            this.tabPageCore.TabIndex = 0;
            this.tabPageCore.Text = "Plugins";
            this.tabPageCore.UseVisualStyleBackColor = true;
            // 
            // listViewMods
            // 
            this.listViewMods.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewMods.CheckBoxes = true;
            this.listViewMods.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderAuthor,
            this.columnHeaderVersion});
            this.listViewMods.ContextMenuStrip = this.contextMenuStripMain;
            this.listViewMods.FullRowSelect = true;
            this.listViewMods.Location = new System.Drawing.Point(6, 6);
            this.listViewMods.Name = "listViewMods";
            this.listViewMods.Size = new System.Drawing.Size(524, 244);
            this.listViewMods.TabIndex = 0;
            this.listViewMods.UseCompatibleStateImageBehavior = false;
            this.listViewMods.View = System.Windows.Forms.View.Details;
            this.listViewMods.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewMods_ItemChecked);
            this.listViewMods.SelectedIndexChanged += new System.EventHandler(this.listViewMods_SelectedIndexChanged);
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Name";
            this.columnHeaderName.Width = 261;
            // 
            // columnHeaderAuthor
            // 
            this.columnHeaderAuthor.Text = "Author";
            this.columnHeaderAuthor.Width = 150;
            // 
            // columnHeaderVersion
            // 
            this.columnHeaderVersion.Text = "Version";
            this.columnHeaderVersion.Width = 107;
            // 
            // contextMenuStripMain
            // 
            this.contextMenuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewInfoToolStripMenuItem});
            this.contextMenuStripMain.Name = "contextMenuStripMain";
            this.contextMenuStripMain.Size = new System.Drawing.Size(124, 26);
            // 
            // viewInfoToolStripMenuItem
            // 
            this.viewInfoToolStripMenuItem.Name = "viewInfoToolStripMenuItem";
            this.viewInfoToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.viewInfoToolStripMenuItem.Text = "View Info";
            this.viewInfoToolStripMenuItem.Click += new System.EventHandler(this.viewInfoToolStripMenuItem_Click);
            // 
            // tabPageCredits
            // 
            this.tabPageCredits.Controls.Add(this.linkLabelContributors);
            this.tabPageCredits.Controls.Add(this.linkLabellolPants);
            this.tabPageCredits.Controls.Add(this.linkLabelUmbranox);
            this.tabPageCredits.Controls.Add(this.label3);
            this.tabPageCredits.Controls.Add(this.labelModSaber2);
            this.tabPageCredits.Controls.Add(this.linkLabelModSaberLink);
            this.tabPageCredits.Controls.Add(this.labelModSaber1);
            this.tabPageCredits.Location = new System.Drawing.Point(4, 22);
            this.tabPageCredits.Name = "tabPageCredits";
            this.tabPageCredits.Size = new System.Drawing.Size(536, 256);
            this.tabPageCredits.TabIndex = 1;
            this.tabPageCredits.Text = "Mod Manager Credits";
            this.tabPageCredits.UseVisualStyleBackColor = true;
            // 
            // linkLabellolPants
            // 
            this.linkLabellolPants.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.linkLabellolPants.AutoSize = true;
            this.linkLabellolPants.Location = new System.Drawing.Point(383, 99);
            this.linkLabellolPants.Name = "linkLabellolPants";
            this.linkLabellolPants.Size = new System.Drawing.Size(48, 13);
            this.linkLabellolPants.TabIndex = 5;
            this.linkLabellolPants.TabStop = true;
            this.linkLabellolPants.Text = "lolPants";
            this.linkLabellolPants.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabellolPants_LinkClicked);
            // 
            // linkLabelUmbranox
            // 
            this.linkLabelUmbranox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.linkLabelUmbranox.AutoSize = true;
            this.linkLabelUmbranox.Location = new System.Drawing.Point(302, 122);
            this.linkLabelUmbranox.Name = "linkLabelUmbranox";
            this.linkLabelUmbranox.Size = new System.Drawing.Size(60, 13);
            this.linkLabelUmbranox.TabIndex = 4;
            this.linkLabelUmbranox.TabStop = true;
            this.linkLabelUmbranox.Text = "Umbranox";
            this.linkLabelUmbranox.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelUmbranox_LinkClicked);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(174, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Mod Installer created by ";
            // 
            // labelModSaber2
            // 
            this.labelModSaber2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelModSaber2.AutoSize = true;
            this.labelModSaber2.Location = new System.Drawing.Point(326, 99);
            this.labelModSaber2.Name = "labelModSaber2";
            this.labelModSaber2.Size = new System.Drawing.Size(60, 13);
            this.labelModSaber2.TabIndex = 2;
            this.labelModSaber2.Text = "created by";
            // 
            // linkLabelModSaberLink
            // 
            this.linkLabelModSaberLink.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.linkLabelModSaberLink.AutoSize = true;
            this.linkLabelModSaberLink.Location = new System.Drawing.Point(256, 99);
            this.linkLabelModSaberLink.Name = "linkLabelModSaberLink";
            this.linkLabelModSaberLink.Size = new System.Drawing.Size(73, 13);
            this.linkLabelModSaberLink.TabIndex = 1;
            this.linkLabelModSaberLink.TabStop = true;
            this.linkLabelModSaberLink.Text = "modsaber.ml";
            this.linkLabelModSaberLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelModSaberLink_LinkClicked);
            // 
            // labelModSaber1
            // 
            this.labelModSaber1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelModSaber1.AutoSize = true;
            this.labelModSaber1.Location = new System.Drawing.Point(75, 99);
            this.labelModSaber1.Name = "labelModSaber1";
            this.labelModSaber1.Size = new System.Drawing.Size(187, 13);
            this.labelModSaber1.TabIndex = 0;
            this.labelModSaber1.Text = "Mod Hosting Platform Provided by ";
            // 
            // buttonViewInfo
            // 
            this.buttonViewInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonViewInfo.Enabled = false;
            this.buttonViewInfo.Location = new System.Drawing.Point(276, 341);
            this.buttonViewInfo.Name = "buttonViewInfo";
            this.buttonViewInfo.Size = new System.Drawing.Size(158, 23);
            this.buttonViewInfo.TabIndex = 9;
            this.buttonViewInfo.Text = "View Selected Mod Info";
            this.buttonViewInfo.UseVisualStyleBackColor = true;
            this.buttonViewInfo.Click += new System.EventHandler(this.buttonViewInfo_Click);
            // 
            // linkLabelContributors
            // 
            this.linkLabelContributors.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.linkLabelContributors.AutoSize = true;
            this.linkLabelContributors.Location = new System.Drawing.Point(232, 145);
            this.linkLabelContributors.Name = "linkLabelContributors";
            this.linkLabelContributors.Size = new System.Drawing.Size(73, 13);
            this.linkLabelContributors.TabIndex = 7;
            this.linkLabelContributors.TabStop = true;
            this.linkLabelContributors.Text = "Contributors";
            this.linkLabelContributors.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelContributors_LinkClicked);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 376);
            this.Controls.Add(this.buttonViewInfo);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonInstall);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonFolderBrowser);
            this.Controls.Add(this.textBoxDirectory);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Beat Saber Mod Manager";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageCore.ResumeLayout(false);
            this.contextMenuStripMain.ResumeLayout(false);
            this.tabPageCredits.ResumeLayout(false);
            this.tabPageCredits.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxDirectory;
        private System.Windows.Forms.Button buttonFolderBrowser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonInstall;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageCore;
        private System.Windows.Forms.ListView listViewMods;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderAuthor;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMain;
        private System.Windows.Forms.ToolStripMenuItem viewInfoToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageCredits;
        private System.Windows.Forms.Button buttonViewInfo;
        private System.Windows.Forms.ColumnHeader columnHeaderVersion;
        private System.Windows.Forms.LinkLabel linkLabelUmbranox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelModSaber2;
        private System.Windows.Forms.LinkLabel linkLabelModSaberLink;
        private System.Windows.Forms.Label labelModSaber1;
        private System.Windows.Forms.LinkLabel linkLabellolPants;
        private System.Windows.Forms.LinkLabel linkLabelContributors;
    }
}

