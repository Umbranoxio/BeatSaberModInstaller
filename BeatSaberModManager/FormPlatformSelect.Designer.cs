namespace BeatSaberModManager
{
    partial class FormPlatformSelect
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
            this.radioButtonSteam = new System.Windows.Forms.RadioButton();
            this.radioButtonOculus = new System.Windows.Forms.RadioButton();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // radioButtonSteam
            // 
            this.radioButtonSteam.AutoSize = true;
            this.radioButtonSteam.Location = new System.Drawing.Point(53, 12);
            this.radioButtonSteam.Name = "radioButtonSteam";
            this.radioButtonSteam.Size = new System.Drawing.Size(187, 17);
            this.radioButtonSteam.TabIndex = 0;
            this.radioButtonSteam.TabStop = true;
            this.radioButtonSteam.Text = "I purchased the game on Steam";
            this.radioButtonSteam.UseVisualStyleBackColor = true;
            // 
            // radioButtonOculus
            // 
            this.radioButtonOculus.AutoSize = true;
            this.radioButtonOculus.Location = new System.Drawing.Point(25, 35);
            this.radioButtonOculus.Name = "radioButtonOculus";
            this.radioButtonOculus.Size = new System.Drawing.Size(242, 17);
            this.radioButtonOculus.TabIndex = 1;
            this.radioButtonOculus.TabStop = true;
            this.radioButtonOculus.Text = "I purchased the game on the Oculus Store";
            this.radioButtonOculus.UseVisualStyleBackColor = true;
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Location = new System.Drawing.Point(109, 60);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(75, 23);
            this.buttonConfirm.TabIndex = 2;
            this.buttonConfirm.Text = "Confirm";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.buttonConfirm_Click);
            // 
            // FormPlatformSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 95);
            this.Controls.Add(this.buttonConfirm);
            this.Controls.Add(this.radioButtonOculus);
            this.Controls.Add(this.radioButtonSteam);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPlatformSelect";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Please select your platform";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonSteam;
        private System.Windows.Forms.RadioButton radioButtonOculus;
        private System.Windows.Forms.Button buttonConfirm;
    }
}