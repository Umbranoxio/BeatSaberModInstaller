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
            this.tabSettings = new System.Windows.Forms.TabControl();
            this.tabPageCore = new System.Windows.Forms.TabPage();
            this.listViewMods = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderAuthor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.directDownloadToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageCredits = new System.Windows.Forms.TabPage();
            this.linkLabelContributors = new System.Windows.Forms.LinkLabel();
            this.linkLabellolPants = new System.Windows.Forms.LinkLabel();
            this.linkLabelUmbranox = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.labelModSaber2 = new System.Windows.Forms.Label();
            this.linkLabelModSaberLink = new System.Windows.Forms.LinkLabel();
            this.labelModSaber1 = new System.Windows.Forms.Label();
            this.tabPageHelp = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabelDiscord = new System.Windows.Forms.LinkLabel();
            this.labelDiscordInfo = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.extrasGroupBox = new System.Windows.Forms.GroupBox();
            this.platformLabel = new System.Windows.Forms.Label();
            this.resetSettingsButton = new System.Windows.Forms.Button();
            this.openSettingsFolder = new System.Windows.Forms.Button();
            this.toggleRegisterOneClick = new System.Windows.Forms.CheckBox();
            this.buttonViewInfo = new System.Windows.Forms.Button();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.tableLayoutPanelInfo = new System.Windows.Forms.TableLayoutPanel();
            this.helpInfoLabel1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPluginsPath = new System.Windows.Forms.TextBox();
            this.helpInfoLabel3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabSettings.SuspendLayout();
            this.tabPageCore.SuspendLayout();
            this.contextMenuStripMain.SuspendLayout();
            this.tabPageCredits.SuspendLayout();
            this.tabPageHelp.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.extrasGroupBox.SuspendLayout();
            this.panelInfo.SuspendLayout();
            this.tableLayoutPanelInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxDirectory
            // 
            this.textBoxDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDirectory.Enabled = false;
            this.textBoxDirectory.Location = new System.Drawing.Point(10, 25);
            this.textBoxDirectory.Name = "textBoxDirectory";
            this.textBoxDirectory.Size = new System.Drawing.Size(454, 22);
            this.textBoxDirectory.TabIndex = 0;
            this.textBoxDirectory.TextChanged += new System.EventHandler(this.textBoxDirectory_TextChanged);
            // 
            // buttonFolderBrowser
            // 
            this.buttonFolderBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFolderBrowser.Location = new System.Drawing.Point(470, 25);
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
            this.buttonInstall.Location = new System.Drawing.Point(462, 401);
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
            this.labelStatus.Location = new System.Drawing.Point(7, 406);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(285, 145);
            this.labelStatus.TabIndex = 5;
            this.labelStatus.Text = "Status: Null";
            // 
            // tabSettings
            // 
            this.tabSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabSettings.Controls.Add(this.tabPageCore);
            this.tabSettings.Controls.Add(this.tabPageCredits);
            this.tabSettings.Controls.Add(this.tabPageHelp);
            this.tabSettings.Controls.Add(this.tabPage1);
            this.tabSettings.Enabled = false;
            this.tabSettings.Location = new System.Drawing.Point(10, 140);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            this.tabSettings.Size = new System.Drawing.Size(566, 255);
            this.tabSettings.TabIndex = 8;
            // 
            // tabPageCore
            // 
            this.tabPageCore.Controls.Add(this.listViewMods);
            this.tabPageCore.Location = new System.Drawing.Point(4, 22);
            this.tabPageCore.Name = "tabPageCore";
            this.tabPageCore.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCore.Size = new System.Drawing.Size(558, 229);
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
            this.listViewMods.Size = new System.Drawing.Size(546, 217);
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
            this.viewInfoToolStripMenuItem,
            this.directDownloadToolStripMenuItem1});
            this.contextMenuStripMain.Name = "contextMenuStripMain";
            this.contextMenuStripMain.Size = new System.Drawing.Size(163, 48);
            this.contextMenuStripMain.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripMain_Opening);
            // 
            // viewInfoToolStripMenuItem
            // 
            this.viewInfoToolStripMenuItem.Name = "viewInfoToolStripMenuItem";
            this.viewInfoToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.viewInfoToolStripMenuItem.Text = "View Info";
            this.viewInfoToolStripMenuItem.Click += new System.EventHandler(this.viewInfoToolStripMenuItem_Click);
            // 
            // directDownloadToolStripMenuItem1
            // 
            this.directDownloadToolStripMenuItem1.Name = "directDownloadToolStripMenuItem1";
            this.directDownloadToolStripMenuItem1.Size = new System.Drawing.Size(162, 22);
            this.directDownloadToolStripMenuItem1.Text = "Direct Download";
            this.directDownloadToolStripMenuItem1.Click += new System.EventHandler(this.directDownloadToolStripMenuItem1_Click);
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
            this.tabPageCredits.Size = new System.Drawing.Size(558, 229);
            this.tabPageCredits.TabIndex = 1;
            this.tabPageCredits.Text = "Mod Manager Credits";
            this.tabPageCredits.UseVisualStyleBackColor = true;
            // 
            // linkLabelContributors
            // 
            this.linkLabelContributors.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.linkLabelContributors.AutoSize = true;
            this.linkLabelContributors.Location = new System.Drawing.Point(232, 137);
            this.linkLabelContributors.Name = "linkLabelContributors";
            this.linkLabelContributors.Size = new System.Drawing.Size(73, 13);
            this.linkLabelContributors.TabIndex = 7;
            this.linkLabelContributors.TabStop = true;
            this.linkLabelContributors.Text = "Contributors";
            this.linkLabelContributors.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelContributors_LinkClicked);
            // 
            // linkLabellolPants
            // 
            this.linkLabellolPants.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.linkLabellolPants.AutoSize = true;
            this.linkLabellolPants.Location = new System.Drawing.Point(386, 92);
            this.linkLabellolPants.Name = "linkLabellolPants";
            this.linkLabellolPants.Size = new System.Drawing.Size(57, 13);
            this.linkLabellolPants.TabIndex = 5;
            this.linkLabellolPants.TabStop = true;
            this.linkLabellolPants.Text = "vanZeben";
            this.linkLabellolPants.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabellolPants_LinkClicked);
            // 
            // linkLabelUmbranox
            // 
            this.linkLabelUmbranox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.linkLabelUmbranox.AutoSize = true;
            this.linkLabelUmbranox.Location = new System.Drawing.Point(302, 114);
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
            this.label3.Location = new System.Drawing.Point(174, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Mod Installer created by ";
            // 
            // labelModSaber2
            // 
            this.labelModSaber2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelModSaber2.AutoSize = true;
            this.labelModSaber2.Location = new System.Drawing.Point(329, 92);
            this.labelModSaber2.Name = "labelModSaber2";
            this.labelModSaber2.Size = new System.Drawing.Size(60, 13);
            this.labelModSaber2.TabIndex = 2;
            this.labelModSaber2.Text = "created by";
            // 
            // linkLabelModSaberLink
            // 
            this.linkLabelModSaberLink.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.linkLabelModSaberLink.AutoSize = true;
            this.linkLabelModSaberLink.Location = new System.Drawing.Point(273, 92);
            this.linkLabelModSaberLink.Name = "linkLabelModSaberLink";
            this.linkLabelModSaberLink.Size = new System.Drawing.Size(59, 13);
            this.linkLabelModSaberLink.TabIndex = 1;
            this.linkLabelModSaberLink.TabStop = true;
            this.linkLabelModSaberLink.Text = "BeatMods";
            this.linkLabelModSaberLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelModSaberLink_LinkClicked);
            // 
            // labelModSaber1
            // 
            this.labelModSaber1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelModSaber1.AutoSize = true;
            this.labelModSaber1.Location = new System.Drawing.Point(92, 92);
            this.labelModSaber1.Name = "labelModSaber1";
            this.labelModSaber1.Size = new System.Drawing.Size(187, 13);
            this.labelModSaber1.TabIndex = 0;
            this.labelModSaber1.Text = "Mod Hosting Platform Provided by ";
            // 
            // tabPageHelp
            // 
            this.tabPageHelp.Controls.Add(this.label4);
            this.tabPageHelp.Controls.Add(this.linkLabelDiscord);
            this.tabPageHelp.Controls.Add(this.labelDiscordInfo);
            this.tabPageHelp.Location = new System.Drawing.Point(4, 22);
            this.tabPageHelp.Name = "tabPageHelp";
            this.tabPageHelp.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHelp.Size = new System.Drawing.Size(558, 229);
            this.tabPageHelp.TabIndex = 2;
            this.tabPageHelp.Text = "Help";
            this.tabPageHelp.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(212, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Need more help?";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linkLabelDiscord
            // 
            this.linkLabelDiscord.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.linkLabelDiscord.AutoSize = true;
            this.linkLabelDiscord.Location = new System.Drawing.Point(189, 137);
            this.linkLabelDiscord.Name = "linkLabelDiscord";
            this.linkLabelDiscord.Size = new System.Drawing.Size(145, 13);
            this.linkLabelDiscord.TabIndex = 1;
            this.linkLabelDiscord.TabStop = true;
            this.linkLabelDiscord.Text = "discord.gg/beatsabermods";
            this.linkLabelDiscord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabelDiscord.Click += new System.EventHandler(this.linkLabelDiscord_Click);
            // 
            // labelDiscordInfo
            // 
            this.labelDiscordInfo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelDiscordInfo.AutoSize = true;
            this.labelDiscordInfo.Location = new System.Drawing.Point(117, 114);
            this.labelDiscordInfo.Name = "labelDiscordInfo";
            this.labelDiscordInfo.Size = new System.Drawing.Size(303, 13);
            this.labelDiscordInfo.TabIndex = 0;
            this.labelDiscordInfo.Text = "Join us on the Beat Saber Modding Group Discord server!";
            this.labelDiscordInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.extrasGroupBox);
            this.tabPage1.Controls.Add(this.toggleRegisterOneClick);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(558, 229);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Settings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // extrasGroupBox
            // 
            this.extrasGroupBox.Controls.Add(this.platformLabel);
            this.extrasGroupBox.Controls.Add(this.resetSettingsButton);
            this.extrasGroupBox.Controls.Add(this.openSettingsFolder);
            this.extrasGroupBox.Location = new System.Drawing.Point(7, 31);
            this.extrasGroupBox.Name = "extrasGroupBox";
            this.extrasGroupBox.Size = new System.Drawing.Size(545, 70);
            this.extrasGroupBox.TabIndex = 1;
            this.extrasGroupBox.TabStop = false;
            this.extrasGroupBox.Text = "Debugging";
            // 
            // platformLabel
            // 
            this.platformLabel.AutoSize = true;
            this.platformLabel.Location = new System.Drawing.Point(7, 22);
            this.platformLabel.Name = "platformLabel";
            this.platformLabel.Size = new System.Drawing.Size(94, 13);
            this.platformLabel.TabIndex = 2;
            this.platformLabel.Text = "Platform: Default";
            // 
            // resetSettingsButton
            // 
            this.resetSettingsButton.Location = new System.Drawing.Point(141, 41);
            this.resetSettingsButton.Name = "resetSettingsButton";
            this.resetSettingsButton.Size = new System.Drawing.Size(144, 23);
            this.resetSettingsButton.TabIndex = 1;
            this.resetSettingsButton.Text = "Reset Settings to Default";
            this.resetSettingsButton.UseVisualStyleBackColor = true;
            this.resetSettingsButton.Click += new System.EventHandler(this.resetSettingsButton_Click);
            // 
            // openSettingsFolder
            // 
            this.openSettingsFolder.Location = new System.Drawing.Point(6, 41);
            this.openSettingsFolder.Name = "openSettingsFolder";
            this.openSettingsFolder.Size = new System.Drawing.Size(129, 23);
            this.openSettingsFolder.TabIndex = 0;
            this.openSettingsFolder.Text = "Open Settings Folder";
            this.openSettingsFolder.UseVisualStyleBackColor = true;
            this.openSettingsFolder.Click += new System.EventHandler(this.openSettingsFolderButton_Click);
            // 
            // toggleRegisterOneClick
            // 
            this.toggleRegisterOneClick.AutoSize = true;
            this.toggleRegisterOneClick.Location = new System.Drawing.Point(7, 7);
            this.toggleRegisterOneClick.Name = "toggleRegisterOneClick";
            this.toggleRegisterOneClick.Size = new System.Drawing.Size(175, 17);
            this.toggleRegisterOneClick.TabIndex = 0;
            this.toggleRegisterOneClick.Text = "Register as OneClick Installer";
            this.toggleRegisterOneClick.UseVisualStyleBackColor = true;
            this.toggleRegisterOneClick.CheckedChanged += new System.EventHandler(this.ToggleRegisterOneClick_CheckedChanged);
            // 
            // buttonViewInfo
            // 
            this.buttonViewInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonViewInfo.Enabled = false;
            this.buttonViewInfo.Location = new System.Drawing.Point(298, 401);
            this.buttonViewInfo.Name = "buttonViewInfo";
            this.buttonViewInfo.Size = new System.Drawing.Size(158, 23);
            this.buttonViewInfo.TabIndex = 9;
            this.buttonViewInfo.Text = "View Selected Mod Info";
            this.buttonViewInfo.UseVisualStyleBackColor = true;
            this.buttonViewInfo.Click += new System.EventHandler(this.buttonViewInfo_Click);
            // 
            // panelInfo
            // 
            this.panelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelInfo.BackColor = System.Drawing.SystemColors.Info;
            this.panelInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInfo.Controls.Add(this.tableLayoutPanelInfo);
            this.panelInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelInfo.Location = new System.Drawing.Point(10, 54);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(564, 80);
            this.panelInfo.TabIndex = 10;
            // 
            // tableLayoutPanelInfo
            // 
            this.tableLayoutPanelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelInfo.BackColor = System.Drawing.SystemColors.Info;
            this.tableLayoutPanelInfo.ColumnCount = 1;
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelInfo.Controls.Add(this.helpInfoLabel1, 0, 0);
            this.tableLayoutPanelInfo.Controls.Add(this.label2, 0, 4);
            this.tableLayoutPanelInfo.Controls.Add(this.textBoxPluginsPath, 0, 1);
            this.tableLayoutPanelInfo.Controls.Add(this.helpInfoLabel3, 0, 3);
            this.tableLayoutPanelInfo.Location = new System.Drawing.Point(-1, 3);
            this.tableLayoutPanelInfo.Name = "tableLayoutPanelInfo";
            this.tableLayoutPanelInfo.RowCount = 5;
            this.tableLayoutPanelInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelInfo.Size = new System.Drawing.Size(564, 79);
            this.tableLayoutPanelInfo.TabIndex = 13;
            // 
            // helpInfoLabel1
            // 
            this.helpInfoLabel1.AutoSize = true;
            this.helpInfoLabel1.Location = new System.Drawing.Point(3, 0);
            this.helpInfoLabel1.Name = "helpInfoLabel1";
            this.helpInfoLabel1.Size = new System.Drawing.Size(269, 13);
            this.helpInfoLabel1.TabIndex = 0;
            this.helpInfoLabel1.Text = "Most mods will install a .dll into the Plugins folder:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(378, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Right click on a mod in the list below to view more info about that mod.";
            // 
            // textBoxPluginsPath
            // 
            this.textBoxPluginsPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPluginsPath.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxPluginsPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPluginsPath.Location = new System.Drawing.Point(6, 17);
            this.textBoxPluginsPath.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.textBoxPluginsPath.Name = "textBoxPluginsPath";
            this.textBoxPluginsPath.ReadOnly = true;
            this.textBoxPluginsPath.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxPluginsPath.Size = new System.Drawing.Size(555, 15);
            this.textBoxPluginsPath.TabIndex = 5;
            this.textBoxPluginsPath.Click += new System.EventHandler(this.textBoxPluginsPath_Click);
            this.textBoxPluginsPath.Leave += new System.EventHandler(this.textBoxPluginsPath_Leave);
            // 
            // helpInfoLabel3
            // 
            this.helpInfoLabel3.AutoSize = true;
            this.helpInfoLabel3.Location = new System.Drawing.Point(3, 42);
            this.helpInfoLabel3.Name = "helpInfoLabel3";
            this.helpInfoLabel3.Size = new System.Drawing.Size(318, 13);
            this.helpInfoLabel3.TabIndex = 3;
            this.helpInfoLabel3.Text = "You can uninstall mods by removing the .dll from that folder.";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(507, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "v0.13.2 only";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 436);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panelInfo);
            this.Controls.Add(this.buttonViewInfo);
            this.Controls.Add(this.tabSettings);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonInstall);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonFolderBrowser);
            this.Controls.Add(this.textBoxDirectory);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Beat Saber Mod Manager (Classic Edition) ";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tabSettings.ResumeLayout(false);
            this.tabPageCore.ResumeLayout(false);
            this.contextMenuStripMain.ResumeLayout(false);
            this.tabPageCredits.ResumeLayout(false);
            this.tabPageCredits.PerformLayout();
            this.tabPageHelp.ResumeLayout(false);
            this.tabPageHelp.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.extrasGroupBox.ResumeLayout(false);
            this.extrasGroupBox.PerformLayout();
            this.panelInfo.ResumeLayout(false);
            this.tableLayoutPanelInfo.ResumeLayout(false);
            this.tableLayoutPanelInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxDirectory;
        private System.Windows.Forms.Button buttonFolderBrowser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonInstall;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.TabControl tabSettings;
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
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelInfo;
        private System.Windows.Forms.Label helpInfoLabel1;
        private System.Windows.Forms.Label helpInfoLabel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPluginsPath;
        private System.Windows.Forms.TabPage tabPageHelp;
        private System.Windows.Forms.Label labelDiscordInfo;
        private System.Windows.Forms.LinkLabel linkLabelDiscord;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem directDownloadToolStripMenuItem1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox toggleRegisterOneClick;
        private System.Windows.Forms.GroupBox extrasGroupBox;
        private System.Windows.Forms.Button openSettingsFolder;
        private System.Windows.Forms.Button resetSettingsButton;
        private System.Windows.Forms.Label platformLabel;
    }
}

