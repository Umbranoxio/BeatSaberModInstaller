using System;
using System.Windows.Forms;
using BeatSaberModManager.Core;
namespace BeatSaberModManager
{
    public partial class FormPlatformSelect : Form
    {
        PathLogic logic;
        public FormPlatformSelect(PathLogic _logic)
        {
            InitializeComponent();
            logic = _logic;
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
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
