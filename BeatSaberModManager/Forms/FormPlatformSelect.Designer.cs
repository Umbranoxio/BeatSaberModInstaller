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
            this.radioButtonSteam = new MaterialSkin.Controls.MaterialRadioButton();
            this.radioButtonOculus = new MaterialSkin.Controls.MaterialRadioButton();
            this.buttonConfirm = new MaterialSkin.Controls.MaterialRaisedButton();
            this.SuspendLayout();
            // 
            // radioButtonSteam
            // 
            this.radioButtonSteam.AutoSize = true;
            this.radioButtonSteam.Depth = 0;
            this.radioButtonSteam.Font = new System.Drawing.Font("Roboto", 10F);
            this.radioButtonSteam.Location = new System.Drawing.Point(20, 80);
            this.radioButtonSteam.Margin = new System.Windows.Forms.Padding(0);
            this.radioButtonSteam.MouseLocation = new System.Drawing.Point(-1, -1);
            this.radioButtonSteam.MouseState = MaterialSkin.MouseState.HOVER;
            this.radioButtonSteam.Name = "radioButtonSteam";
            this.radioButtonSteam.Ripple = true;
            this.radioButtonSteam.Size = new System.Drawing.Size(224, 30);
            this.radioButtonSteam.TabIndex = 3;
            this.radioButtonSteam.TabStop = true;
            this.radioButtonSteam.Text = "I purchased the game on Steam";
            this.radioButtonSteam.UseVisualStyleBackColor = true;
            // 
            // radioButtonOculus
            // 
            this.radioButtonOculus.AutoSize = true;
            this.radioButtonOculus.Depth = 0;
            this.radioButtonOculus.Font = new System.Drawing.Font("Roboto", 10F);
            this.radioButtonOculus.Location = new System.Drawing.Point(20, 122);
            this.radioButtonOculus.Margin = new System.Windows.Forms.Padding(0);
            this.radioButtonOculus.MouseLocation = new System.Drawing.Point(-1, -1);
            this.radioButtonOculus.MouseState = MaterialSkin.MouseState.HOVER;
            this.radioButtonOculus.Name = "radioButtonOculus";
            this.radioButtonOculus.Ripple = true;
            this.radioButtonOculus.Size = new System.Drawing.Size(286, 30);
            this.radioButtonOculus.TabIndex = 4;
            this.radioButtonOculus.TabStop = true;
            this.radioButtonOculus.Text = "I purchased the game on the Oculus Store";
            this.radioButtonOculus.UseVisualStyleBackColor = true;
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonConfirm.Depth = 0;
            this.buttonConfirm.Location = new System.Drawing.Point(124, 166);
            this.buttonConfirm.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Primary = true;
            this.buttonConfirm.Size = new System.Drawing.Size(157, 39);
            this.buttonConfirm.TabIndex = 5;
            this.buttonConfirm.Text = "Confirm";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.MaterialRaisedButton1_Click);
            // 
            // FormPlatformSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 219);
            this.Controls.Add(this.buttonConfirm);
            this.Controls.Add(this.radioButtonOculus);
            this.Controls.Add(this.radioButtonSteam);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
        private MaterialSkin.Controls.MaterialRadioButton radioButtonSteam;
        private MaterialSkin.Controls.MaterialRadioButton radioButtonOculus;
        private MaterialSkin.Controls.MaterialRaisedButton buttonConfirm;
    }
}