using System;
using System.Diagnostics;
using System.Windows.Forms;
using CommonMark;
using BeatSaberModManager.DataModels;
namespace BeatSaberModManager
{
    public partial class FormDetailViewer : Form
    {
        private ReleaseInfo _release;
        public FormDetailViewer(ReleaseInfo release)
        {
            InitializeComponent();
            _release = release;
            labelTitle.Text = string.Format("{0} by {1} {2}", release.title, release.author, release.version);
            DescriptionHandler();
        }
        private void DescriptionHandler()
        {
            string description = CommonMarkConverter.Convert(_release.description);
            webBrowserDescription.DocumentText = description;
           
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabelDirectLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_release.downloadLink.StartsWith("https://"))
            {
                Process.Start(_release.downloadLink);
            }
        }
    }
}
