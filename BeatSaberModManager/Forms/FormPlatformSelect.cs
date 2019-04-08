using System;
using System.Windows.Forms;
using BeatSaberModManager.Core;
using MaterialSkin.Controls;
using MaterialSkin;

namespace BeatSaberModManager
{
    public partial class FormPlatformSelect : MaterialForm
    {
        PathLogic logic;
        public FormPlatformSelect(PathLogic _logic)
        {
            InitializeComponent();
            logic = _logic;
        }

        private void MaterialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (radioButtonOculus.Checked)
            {
                logic.platform = Platform.Oculus;
            }
            else
            {
                logic.platform = Platform.Steam;
            }
            this.Close();
        }
    }
}
