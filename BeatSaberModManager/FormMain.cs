using System;
using System.Windows.Forms;
using BeatSaberModManager.Core;
using System.Threading;
using BeatSaberModManager.DataModels;
using System.Diagnostics;

namespace BeatSaberModManager
{
    public partial class FormMain : Form
    {

        #region Instances
        PathLogic path;
        RemoteLogic remote;
        InstallerLogic installer;
        #endregion

        #region Constructor
        public FormMain()
        {
            InitializeComponent();
            path = new PathLogic();
            remote = new RemoteLogic();
        }
        #endregion

        #region Loading
        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                textBoxDirectory.Text = path.GetInstallationPath();
                remote.CheckVersion();
                new Thread(() => { RemoteLoad(); }).Start();
            } catch (Exception ex)
            {
                MessageBox.Show("Failed to start, error: " + ex.ToString());
            }
        }
        private void RemoteLoad()
        {
            UpdateStatus("Loading latest releases...");
            remote.PopulateReleases();
            installer = new InstallerLogic(remote.releases, path.installPath);
            installer.StatusUpdate += Installer_StatusUpdate;
            this.Invoke((MethodInvoker)(() => { ShowReleases(); }));
        }

        private void ShowReleases()
        {
            foreach (ReleaseInfo release in remote.releases)
            {
                ListViewItem item = new ListViewItem();
                item.Text = release.title;
                item.SubItems.Add(release.author);
                item.SubItems.Add(release.version);
                item.Tag = release;
                if (release.platform == path.platform || release.platform == Platform.Default)
                {
                    listViewMods.Items.Add(item);
                    CheckDefaultMod(release, item);
                }
            }
            UpdateStatus("Releases loaded.");
            tabControlMain.Enabled = true;
        }
        #endregion

        #region Helpers
        private void UpdateStatus(string status)
        {
            string formattedText = string.Format("Status: {0}", status);
            this.Invoke((MethodInvoker)(() =>
            { //Invoke so we can call from any thread
                labelStatus.Text = formattedText;
            }));
        }
        private void CheckDefaultMod(ReleaseInfo release, ListViewItem item)
        {
            string link = release.downloadLink.ToLower();
            if (link.Contains("song-loader") || link.Contains("scoresaber") || link.Contains("beatsaver"))
            {
                item.Checked = true;
                release.install = true;
            }
            else
            {
                release.install = false;
            }
        }
        #endregion

        #region Events Handlers
        private void Installer_StatusUpdate(string status)
        {
            UpdateStatus(status);
        }
        private void buttonInstall_Click(object sender, EventArgs e)
        {
            new Thread(() => { installer.Run(); }).Start();
        }

        private void buttonFolderBrowser_Click(object sender, EventArgs e)
        {
            textBoxDirectory.Text = path.ManualFind();
            installer.installDirectory = textBoxDirectory.Text;
        }
        private void listViewMods_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ReleaseInfo release = (ReleaseInfo)e.Item.Tag;
            if (release.downloadLink.ToLower().Contains("song-loader")) { e.Item.Checked = true; };
            release.install = e.Item.Checked;
        }
        private void listViewMods_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewMods.SelectedItems.Count == 1)
            {
                buttonViewInfo.Enabled = true;
            }
            else
            {
                buttonViewInfo.Enabled = false;
            }
        }
        private void buttonViewInfo_Click(object sender, EventArgs e)
        {
            new FormDetailViewer((ReleaseInfo)listViewMods.SelectedItems[0].Tag).ShowDialog();
        }
        private void linkLabellolPants_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/lolPants");
        }

        private void linkLabelModSaberLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.modsaber.ml/#/faq");
        }

        private void linkLabelUmbranox_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://twitter.com/Umbranoxus");
        }
        #endregion

    }
}
