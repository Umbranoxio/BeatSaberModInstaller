namespace BeatSaberModInstaller
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.textBoxDirectory = new System.Windows.Forms.TextBox();
            this.buttonFolderBrowser = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxSongLoader = new System.Windows.Forms.CheckBox();
            this.radioButtonScoreSaberOculus = new System.Windows.Forms.RadioButton();
            this.checkBoxBeatSaver = new System.Windows.Forms.CheckBox();
            this.radioButtonScoreSaberSteam = new System.Windows.Forms.RadioButton();
            this.buttonInstall = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageCore = new System.Windows.Forms.TabPage();
            this.tabPageMultiplayer = new System.Windows.Forms.TabPage();
            this.radioButtonBetaOculus = new System.Windows.Forms.RadioButton();
            this.radioButtonBetaSteam = new System.Windows.Forms.RadioButton();
            this.linkLabelReadMore = new System.Windows.Forms.LinkLabel();
            this.checkBoxRead = new System.Windows.Forms.CheckBox();
            this.textBoxBeta = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelPS = new System.Windows.Forms.Label();
            this.tabControlMain.SuspendLayout();
            this.tabPageCore.SuspendLayout();
            this.tabPageMultiplayer.SuspendLayout();
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
            // checkBoxSongLoader
            // 
            this.checkBoxSongLoader.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxSongLoader.AutoSize = true;
            this.checkBoxSongLoader.Checked = true;
            this.checkBoxSongLoader.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSongLoader.Enabled = false;
            this.checkBoxSongLoader.Location = new System.Drawing.Point(163, 85);
            this.checkBoxSongLoader.Name = "checkBoxSongLoader";
            this.checkBoxSongLoader.Size = new System.Drawing.Size(211, 17);
            this.checkBoxSongLoader.TabIndex = 4;
            this.checkBoxSongLoader.Text = "Song Loader (Required) {0} - xyonico";
            this.checkBoxSongLoader.UseVisualStyleBackColor = true;
            // 
            // radioButtonScoreSaberOculus
            // 
            this.radioButtonScoreSaberOculus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioButtonScoreSaberOculus.AutoSize = true;
            this.radioButtonScoreSaberOculus.Enabled = false;
            this.radioButtonScoreSaberOculus.Location = new System.Drawing.Point(92, 154);
            this.radioButtonScoreSaberOculus.Name = "radioButtonScoreSaberOculus";
            this.radioButtonScoreSaberOculus.Size = new System.Drawing.Size(353, 17);
            this.radioButtonScoreSaberOculus.TabIndex = 7;
            this.radioButtonScoreSaberOculus.TabStop = true;
            this.radioButtonScoreSaberOculus.Text = "ScoreSaber (Custom ScoreBoards) (Oculus Home) {0} - Umbranox";
            this.radioButtonScoreSaberOculus.UseVisualStyleBackColor = true;
            // 
            // checkBoxBeatSaver
            // 
            this.checkBoxBeatSaver.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxBeatSaver.AutoSize = true;
            this.checkBoxBeatSaver.Checked = true;
            this.checkBoxBeatSaver.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBeatSaver.Enabled = false;
            this.checkBoxBeatSaver.Location = new System.Drawing.Point(138, 108);
            this.checkBoxBeatSaver.Name = "checkBoxBeatSaver";
            this.checkBoxBeatSaver.Size = new System.Drawing.Size(261, 17);
            this.checkBoxBeatSaver.TabIndex = 5;
            this.checkBoxBeatSaver.Text = "Beat Saver In-Game Browser {0} - Andruzzzhka";
            this.checkBoxBeatSaver.UseVisualStyleBackColor = true;
            // 
            // radioButtonScoreSaberSteam
            // 
            this.radioButtonScoreSaberSteam.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioButtonScoreSaberSteam.AutoSize = true;
            this.radioButtonScoreSaberSteam.Enabled = false;
            this.radioButtonScoreSaberSteam.Location = new System.Drawing.Point(111, 131);
            this.radioButtonScoreSaberSteam.Name = "radioButtonScoreSaberSteam";
            this.radioButtonScoreSaberSteam.Size = new System.Drawing.Size(315, 17);
            this.radioButtonScoreSaberSteam.TabIndex = 6;
            this.radioButtonScoreSaberSteam.TabStop = true;
            this.radioButtonScoreSaberSteam.Text = "ScoreSaber (Custom ScoreBoards) (Steam) {0} - Umbranox";
            this.radioButtonScoreSaberSteam.UseVisualStyleBackColor = true;
            // 
            // buttonInstall
            // 
            this.buttonInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonInstall.Enabled = false;
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
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(7, 346);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(66, 13);
            this.labelStatus.TabIndex = 5;
            this.labelStatus.Text = "Status: Null";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPageCore);
            this.tabControlMain.Controls.Add(this.tabPageMultiplayer);
            this.tabControlMain.Enabled = false;
            this.tabControlMain.Location = new System.Drawing.Point(10, 53);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(544, 282);
            this.tabControlMain.TabIndex = 8;
            this.tabControlMain.SelectedIndexChanged += new System.EventHandler(this.tabControlMain_SelectedIndexChanged);
            // 
            // tabPageCore
            // 
            this.tabPageCore.Controls.Add(this.checkBoxSongLoader);
            this.tabPageCore.Controls.Add(this.radioButtonScoreSaberSteam);
            this.tabPageCore.Controls.Add(this.radioButtonScoreSaberOculus);
            this.tabPageCore.Controls.Add(this.checkBoxBeatSaver);
            this.tabPageCore.Location = new System.Drawing.Point(4, 22);
            this.tabPageCore.Name = "tabPageCore";
            this.tabPageCore.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCore.Size = new System.Drawing.Size(536, 256);
            this.tabPageCore.TabIndex = 0;
            this.tabPageCore.Text = "Core Plugins";
            this.tabPageCore.UseVisualStyleBackColor = true;
            // 
            // tabPageMultiplayer
            // 
            this.tabPageMultiplayer.Controls.Add(this.radioButtonBetaOculus);
            this.tabPageMultiplayer.Controls.Add(this.radioButtonBetaSteam);
            this.tabPageMultiplayer.Controls.Add(this.linkLabelReadMore);
            this.tabPageMultiplayer.Controls.Add(this.checkBoxRead);
            this.tabPageMultiplayer.Controls.Add(this.textBoxBeta);
            this.tabPageMultiplayer.Controls.Add(this.label2);
            this.tabPageMultiplayer.Location = new System.Drawing.Point(4, 22);
            this.tabPageMultiplayer.Name = "tabPageMultiplayer";
            this.tabPageMultiplayer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMultiplayer.Size = new System.Drawing.Size(536, 256);
            this.tabPageMultiplayer.TabIndex = 1;
            this.tabPageMultiplayer.Text = "Multiplayer Beta";
            this.tabPageMultiplayer.UseVisualStyleBackColor = true;
            // 
            // radioButtonBetaOculus
            // 
            this.radioButtonBetaOculus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonBetaOculus.AutoSize = true;
            this.radioButtonBetaOculus.Enabled = false;
            this.radioButtonBetaOculus.Location = new System.Drawing.Point(231, 231);
            this.radioButtonBetaOculus.Name = "radioButtonBetaOculus";
            this.radioButtonBetaOculus.Size = new System.Drawing.Size(299, 17);
            this.radioButtonBetaOculus.TabIndex = 11;
            this.radioButtonBetaOculus.Text = "Install Multiplayer Beta (Oculus Home) {0} - Umbranox";
            this.radioButtonBetaOculus.UseVisualStyleBackColor = true;
            this.radioButtonBetaOculus.CheckedChanged += new System.EventHandler(this.radioButtonBetaOculus_CheckedChanged);
            // 
            // radioButtonBetaSteam
            // 
            this.radioButtonBetaSteam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonBetaSteam.AutoSize = true;
            this.radioButtonBetaSteam.Enabled = false;
            this.radioButtonBetaSteam.Location = new System.Drawing.Point(269, 211);
            this.radioButtonBetaSteam.Name = "radioButtonBetaSteam";
            this.radioButtonBetaSteam.Size = new System.Drawing.Size(261, 17);
            this.radioButtonBetaSteam.TabIndex = 10;
            this.radioButtonBetaSteam.Text = "Install Multiplayer Beta (Steam) {0} - Umbranox";
            this.radioButtonBetaSteam.UseVisualStyleBackColor = true;
            this.radioButtonBetaSteam.CheckedChanged += new System.EventHandler(this.radioButtonBetaSteam_CheckedChanged);
            // 
            // linkLabelReadMore
            // 
            this.linkLabelReadMore.AutoSize = true;
            this.linkLabelReadMore.Location = new System.Drawing.Point(464, 8);
            this.linkLabelReadMore.Name = "linkLabelReadMore";
            this.linkLabelReadMore.Size = new System.Drawing.Size(67, 13);
            this.linkLabelReadMore.TabIndex = 9;
            this.linkLabelReadMore.TabStop = true;
            this.linkLabelReadMore.Text = "Learn more ";
            this.linkLabelReadMore.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelReadMore_LinkClicked);
            // 
            // checkBoxRead
            // 
            this.checkBoxRead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxRead.AutoSize = true;
            this.checkBoxRead.Enabled = false;
            this.checkBoxRead.Location = new System.Drawing.Point(9, 220);
            this.checkBoxRead.Name = "checkBoxRead";
            this.checkBoxRead.Size = new System.Drawing.Size(156, 17);
            this.checkBoxRead.TabIndex = 3;
            this.checkBoxRead.Text = "I have read the disclaimer";
            this.checkBoxRead.UseVisualStyleBackColor = true;
            this.checkBoxRead.CheckedChanged += new System.EventHandler(this.checkBoxRead_CheckedChanged);
            // 
            // textBoxBeta
            // 
            this.textBoxBeta.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBeta.BackColor = System.Drawing.Color.White;
            this.textBoxBeta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxBeta.Location = new System.Drawing.Point(9, 25);
            this.textBoxBeta.Multiline = true;
            this.textBoxBeta.Name = "textBoxBeta";
            this.textBoxBeta.ReadOnly = true;
            this.textBoxBeta.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxBeta.Size = new System.Drawing.Size(521, 178);
            this.textBoxBeta.TabIndex = 2;
            this.textBoxBeta.Text = resources.GetString("textBoxBeta.Text");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(414, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Important, please read this in full before participating in the beta";
            // 
            // labelPS
            // 
            this.labelPS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPS.AutoSize = true;
            this.labelPS.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelPS.Location = new System.Drawing.Point(191, 58);
            this.labelPS.Name = "labelPS";
            this.labelPS.Size = new System.Drawing.Size(355, 13);
            this.labelPS.TabIndex = 5;
            this.labelPS.Text = "P.S If you don\'t read the disclaimer; you\'re going to have a bad time";
            this.labelPS.Visible = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 376);
            this.Controls.Add(this.labelPS);
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
            this.tabPageCore.PerformLayout();
            this.tabPageMultiplayer.ResumeLayout(false);
            this.tabPageMultiplayer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxDirectory;
        private System.Windows.Forms.Button buttonFolderBrowser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonInstall;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.CheckBox checkBoxSongLoader;
        private System.Windows.Forms.RadioButton radioButtonScoreSaberOculus;
        private System.Windows.Forms.CheckBox checkBoxBeatSaver;
        private System.Windows.Forms.RadioButton radioButtonScoreSaberSteam;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageCore;
        private System.Windows.Forms.TabPage tabPageMultiplayer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxBeta;
        private System.Windows.Forms.CheckBox checkBoxRead;
        private System.Windows.Forms.Label labelPS;
        private System.Windows.Forms.LinkLabel linkLabelReadMore;
        private System.Windows.Forms.RadioButton radioButtonBetaOculus;
        private System.Windows.Forms.RadioButton radioButtonBetaSteam;
    }
}

