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
            this.groupBoxMods = new System.Windows.Forms.GroupBox();
            this.radioButtonScoreSaberOculus = new System.Windows.Forms.RadioButton();
            this.radioButtonScoreSaberSteam = new System.Windows.Forms.RadioButton();
            this.checkBoxBeatSaver = new System.Windows.Forms.CheckBox();
            this.checkBoxSongLoader = new System.Windows.Forms.CheckBox();
            this.buttonInstall = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.groupBoxMods.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxDirectory
            // 
            this.textBoxDirectory.Location = new System.Drawing.Point(10, 25);
            this.textBoxDirectory.Name = "textBoxDirectory";
            this.textBoxDirectory.Size = new System.Drawing.Size(330, 22);
            this.textBoxDirectory.TabIndex = 0;
            // 
            // buttonFolderBrowser
            // 
            this.buttonFolderBrowser.Location = new System.Drawing.Point(346, 24);
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
            // groupBoxMods
            // 
            this.groupBoxMods.Controls.Add(this.radioButtonScoreSaberOculus);
            this.groupBoxMods.Controls.Add(this.radioButtonScoreSaberSteam);
            this.groupBoxMods.Controls.Add(this.checkBoxBeatSaver);
            this.groupBoxMods.Controls.Add(this.checkBoxSongLoader);
            this.groupBoxMods.Location = new System.Drawing.Point(10, 53);
            this.groupBoxMods.Name = "groupBoxMods";
            this.groupBoxMods.Size = new System.Drawing.Size(362, 121);
            this.groupBoxMods.TabIndex = 3;
            this.groupBoxMods.TabStop = false;
            this.groupBoxMods.Text = "Mod to install";
            // 
            // radioButtonScoreSaberOculus
            // 
            this.radioButtonScoreSaberOculus.AutoSize = true;
            this.radioButtonScoreSaberOculus.Enabled = false;
            this.radioButtonScoreSaberOculus.Location = new System.Drawing.Point(6, 90);
            this.radioButtonScoreSaberOculus.Name = "radioButtonScoreSaberOculus";
            this.radioButtonScoreSaberOculus.Size = new System.Drawing.Size(290, 17);
            this.radioButtonScoreSaberOculus.TabIndex = 3;
            this.radioButtonScoreSaberOculus.TabStop = true;
            this.radioButtonScoreSaberOculus.Text = "ScoreSaber (Custom ScoreBoards) (Oculus Home) {0}";
            this.radioButtonScoreSaberOculus.UseVisualStyleBackColor = true;
            this.radioButtonScoreSaberOculus.CheckedChanged += new System.EventHandler(this.radioButtonScoreSaberOculus_CheckedChanged);
            // 
            // radioButtonScoreSaberSteam
            // 
            this.radioButtonScoreSaberSteam.AutoSize = true;
            this.radioButtonScoreSaberSteam.Enabled = false;
            this.radioButtonScoreSaberSteam.Location = new System.Drawing.Point(6, 67);
            this.radioButtonScoreSaberSteam.Name = "radioButtonScoreSaberSteam";
            this.radioButtonScoreSaberSteam.Size = new System.Drawing.Size(252, 17);
            this.radioButtonScoreSaberSteam.TabIndex = 2;
            this.radioButtonScoreSaberSteam.TabStop = true;
            this.radioButtonScoreSaberSteam.Text = "ScoreSaber (Custom ScoreBoards) (Steam) {0}";
            this.radioButtonScoreSaberSteam.UseVisualStyleBackColor = true;
            this.radioButtonScoreSaberSteam.CheckedChanged += new System.EventHandler(this.radioButtonScoreSaberSteam_CheckedChanged);
            // 
            // checkBoxBeatSaver
            // 
            this.checkBoxBeatSaver.AutoSize = true;
            this.checkBoxBeatSaver.Checked = true;
            this.checkBoxBeatSaver.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBeatSaver.Enabled = false;
            this.checkBoxBeatSaver.Location = new System.Drawing.Point(6, 44);
            this.checkBoxBeatSaver.Name = "checkBoxBeatSaver";
            this.checkBoxBeatSaver.Size = new System.Drawing.Size(261, 17);
            this.checkBoxBeatSaver.TabIndex = 1;
            this.checkBoxBeatSaver.Text = "Beat Saver In-Game Browser {0} - Andruzzzhka";
            this.checkBoxBeatSaver.UseVisualStyleBackColor = true;
            this.checkBoxBeatSaver.CheckedChanged += new System.EventHandler(this.checkBoxBeatSaver_CheckedChanged);
            // 
            // checkBoxSongLoader
            // 
            this.checkBoxSongLoader.AutoSize = true;
            this.checkBoxSongLoader.Checked = true;
            this.checkBoxSongLoader.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSongLoader.Enabled = false;
            this.checkBoxSongLoader.Location = new System.Drawing.Point(6, 21);
            this.checkBoxSongLoader.Name = "checkBoxSongLoader";
            this.checkBoxSongLoader.Size = new System.Drawing.Size(211, 17);
            this.checkBoxSongLoader.TabIndex = 0;
            this.checkBoxSongLoader.Text = "Song Loader (Required) {0} - xyonico";
            this.checkBoxSongLoader.UseVisualStyleBackColor = true;
            // 
            // buttonInstall
            // 
            this.buttonInstall.Enabled = false;
            this.buttonInstall.Location = new System.Drawing.Point(297, 180);
            this.buttonInstall.Name = "buttonInstall";
            this.buttonInstall.Size = new System.Drawing.Size(75, 23);
            this.buttonInstall.TabIndex = 4;
            this.buttonInstall.Text = "Install";
            this.buttonInstall.UseVisualStyleBackColor = true;
            this.buttonInstall.Click += new System.EventHandler(this.buttonInstall_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(9, 185);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(66, 13);
            this.labelStatus.TabIndex = 5;
            this.labelStatus.Text = "Status: Null";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 211);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonInstall);
            this.Controls.Add(this.groupBoxMods);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonFolderBrowser);
            this.Controls.Add(this.textBoxDirectory);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Beat Saber Mod Installer";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBoxMods.ResumeLayout(false);
            this.groupBoxMods.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxDirectory;
        private System.Windows.Forms.Button buttonFolderBrowser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxMods;
        private System.Windows.Forms.RadioButton radioButtonScoreSaberOculus;
        private System.Windows.Forms.RadioButton radioButtonScoreSaberSteam;
        private System.Windows.Forms.CheckBox checkBoxBeatSaver;
        private System.Windows.Forms.CheckBox checkBoxSongLoader;
        private System.Windows.Forms.Button buttonInstall;
        private System.Windows.Forms.Label labelStatus;
    }
}

