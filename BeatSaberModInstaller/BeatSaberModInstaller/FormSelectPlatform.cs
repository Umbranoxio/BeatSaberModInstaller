using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatSaberModInstaller
{
    public partial class FormSelectPlatform : Form
    {
        FormMain Parent;
        public FormSelectPlatform(FormMain parent)
        {
            InitializeComponent();
            Parent = parent;
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            Parent.platformDetected = true;
            if (radioButtonOculus.Checked)
            {
                Parent.isSteam = false;
            }
            this.Close();
        }
    }
}
