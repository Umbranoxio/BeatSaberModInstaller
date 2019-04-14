using System;
using System.Linq;
using System.Windows.Forms;
using BeatSaberModManager.Core;
using System.Threading;
using System.Collections.Generic;
using BeatSaberModManager.DataModels;
using System.Diagnostics;
using System.Drawing;
using SemVer;
using Version = SemVer.Version;

namespace BeatSaberModManager
{
    public partial class FormMain : Form
    {

        #region Instances
        PathLogic path;
        UpdateLogic updater;
        RemoteLogic remote;
        InstallerLogic installer;
        bool finishedLoading = false;
        List<string> defaultMods = new List<string>(new string[] { "songloader", "scoresaber", "beatsaverdownloader" });

        #endregion

        #region Constructor
        public FormMain()
        {
            InitializeComponent();
            path = new PathLogic();
            updater = new UpdateLogic();
            remote = new RemoteLogic();
            listViewMods.ShowItemToolTips = true;
            // Show tooltips
        }
        #endregion

        #region Loading
        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                updater.CheckForUpdates();
                textBoxDirectory.Text = path.GetInstallationPath();
             
                new Thread(() => { RemoteLoad(); }).Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to start, error: " + ex.ToString());
                Environment.Exit(0);
            }
        }

        private void RemoteLoad()
        {
            /*
            UpdateStatus("Loading game versions...");
            remote.GetGameVersions();
            for (int i = 0; i < remote.gameVersions.Length; i++)
            {
                GameVersion gv = remote.gameVersions[i];
                this.Invoke((MethodInvoker)(() => { comboBox_gameVersions.Items.Add(gv.value); })); 
            }
            this.Invoke((MethodInvoker)(() => { comboBox_gameVersions.SelectedIndex = 0; }));
            */
            //this.Invoke((MethodInvoker)(() => { comboBox_gameVersions.Items.Add("0.13.2"); }));
            //this.Invoke((MethodInvoker)(() => { comboBox_gameVersions.SelectedIndex = 0; }));
            UpdateStatus("Loading releases...");
            remote.PopulateReleases();
            installer = new InstallerLogic(remote.releases, path.installPath);
            installer.StatusUpdate += Installer_StatusUpdate;
            this.Invoke((MethodInvoker)(() => { ShowReleases(); }));
        }

        bool first = true;
        private void comboBox_gameVersions_SelectedIndexChanged(object sender, EventArgs e)
        {
            // For some reason this breaks deps/conflicts
            // God knows why
            // listViewMods.Items.Clear();
            UpdateStatus("Loading releases...");
            //comboBox_gameVersions.Enabled = false;
            if (!first)
            {
                ComboBox comboBox = (ComboBox)sender;
                GameVersion gameVersion = remote.gameVersions[comboBox.SelectedIndex];

                remote.selectedGameVersion = gameVersion;
                new Thread(() => { LoadFromComboBox(); }).Start();
            } else
            {
                first = false;
            }
        }
               
        private void LoadFromComboBox()
        {
            remote.PopulateReleases();
            this.Invoke((MethodInvoker)(() => { ShowReleases(); }));
        }

        private void ShowReleases()
        {
            //comboBox_gameVersions.Enabled = true;
            Dictionary<string, int> groups = new Dictionary<string, int>();
            
            listViewMods.Groups.Clear();
            int other = listViewMods.Groups.Add(new ListViewGroup("Other", HorizontalAlignment.Left));
            groups.Add("Other", other);

            foreach (ReleaseInfo release in remote.releases)
            {
                ListViewItem item = new ListViewItem
                {
                    Text = release.title,
                    Tag = release
                };

                item.ToolTipText = release.description;

                item.SubItems.Add(release.author);
                item.SubItems.Add(release.version);

                if (release.platform == path.platform || release.platform == Platform.Default)
                {
                    if (release.category == "")
                    {
                        item.Group = listViewMods.Groups[other];
                    }
                    else if (groups.ContainsKey(release.category))
                    {
                        int index = groups[release.category];
                        item.Group = listViewMods.Groups[index];
                    }
                    else
                    {
                        int index = listViewMods.Groups.Add(new ListViewGroup(release.category, HorizontalAlignment.Left));
                        groups.Add(release.category, index);
                        item.Group = listViewMods.Groups[index];
                    }

                    listViewMods.Items.Add(item);
                    release.itemHandle = item;
                   
                }
            }
            
            ListViewGroup[] sortedGroups = new ListViewGroup[this.listViewMods.Groups.Count];

            listViewMods.Groups.CopyTo(sortedGroups, 0);
            Array.Sort(sortedGroups, new GroupComparer());

            listViewMods.BeginUpdate();
            listViewMods.Groups.Clear();
            listViewMods.Groups.AddRange(sortedGroups);
            listViewMods.EndUpdate();
            finishedLoading = true;
            ReRenderListView();

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
            string name = release.name.ToLower();
            string category = release.category.ToLower();
            if (name.Equals("bsipa") || category.Contains("libraries"))
            {
                item.Text = $"[REQUIRED] {release.title}";
                item.BackColor = Color.LightGray;
                release.disabled = true;

                release.install = true;
                item.Checked = true;
            }

            if (defaultMods.Contains(name))
            {
                item.Checked = true;
                defaultMods.Remove(name);
            }
        }
        #endregion

        #region Event Handlers
        private void Installer_StatusUpdate(string status)
        {
            UpdateStatus(status);
            if (status == "Install complete!") { this.Invoke((MethodInvoker)(() => { buttonInstall.Enabled = true; })); }
        }
        private void buttonInstall_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxDirectory.Text))
            {
                MessageBox.Show("No install directory selected!", "No install directory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            buttonInstall.Enabled = false;
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
            if (release.disabled)
            {
                e.Item.Checked = release.install;
                return;
            }
            else
            {
                release.install = e.Item.Checked;
            }

            if (e.Item.Checked)
            {
                if (release.dependsOn.Count > 0)
                {
                    foreach (ModLink dependency in release.dependsOn)
                    {
                        foreach (ListViewItem lvi in listViewMods.Items)
                        {
                            ReleaseInfo check = (ReleaseInfo)lvi.Tag;
                            if (check.name == dependency.name)
                            {
                                check.itemHandle.Checked = true;
                            }
                        }
                    }
                }
            }

            if (e.Item.Checked)
            {
                if (release.conflictsWith.Count > 0)
                {
                    foreach (ModLink dependency in release.conflictsWith)
                    {
                        foreach (ListViewItem lvi in listViewMods.Items)
                        {
                            ReleaseInfo check = (ReleaseInfo)lvi.Tag;
                            if (check.name == dependency.name)
                            {
                                check.itemHandle.Checked = false;
                                check.disabled = true;
                            }
                        }
                    }
                }
            }
            else
            {
                if (release.conflictsWith.Count > 0)
                {
                    foreach (ModLink dependency in release.conflictsWith)
                    {
                        foreach (ListViewItem lvi in listViewMods.Items)
                        {
                            ReleaseInfo check = (ReleaseInfo)lvi.Tag;
                            if (check.name == dependency.name)
                            {
                                check.disabled = false;
                            }
                        }
                    }
                }
            }
            if (finishedLoading ){
                ReRenderListView();
            }
        }

        private void ReRenderListView ()
        {
            foreach (ListViewItem item in listViewMods.Items)
            {
                ReleaseInfo release = (ReleaseInfo)item.Tag;
                if (release.disabled)
                {
                    item.Checked = release.install;
                    item.BackColor = Color.LightGray;
                    item.Text = $"[{(release.install ? "REQUIRED" : "CONFLICT")}] {release.title}";
                }
                else
                {
                    item.Text = release.title;
                    item.BackColor = Color.White;
                }
                CheckDefaultMod(release, item);
            }
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
            openInfoLink();
        }

        private void viewInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openInfoLink();
        }

        private void openInfoLink()
        {
            var release = (ReleaseInfo)listViewMods.SelectedItems[0].Tag;
            if (release.infoLink.StartsWith("https://"))
            {
                Process.Start(release.infoLink);
            }
        }

        private void linkLabellolPants_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/vanZeben");
        }

        private void linkLabelModSaberLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://beatmods.com/");
        }

        private void linkLabelUmbranox_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://twitter.com/Umbranoxus");
        }

        private void linkLabelContributors_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/beat-saber-modding-group/BeatSaberModInstaller/graphs/contributors");
        }

        private void textBoxDirectory_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxDirectory.Text))
            {
                textBoxPluginsPath.Text = "${Install Directory}\\Plugins";
            }
            else
            {
                textBoxPluginsPath.Text = textBoxDirectory.Text + "\\Plugins";
            }
        }

        private void textBoxPluginsPath_Click(object sender, System.EventArgs e)
        {
            textBoxPluginsPath.SelectAll();
        }

        private void textBoxPluginsPath_Leave(object sender, System.EventArgs e)
        {
            textBoxPluginsPath.DeselectAll();
        }

        private void linkLabelDiscord_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/beatsabermods");
        }
        #endregion

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStripMain_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void directDownloadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var release = (ReleaseInfo)listViewMods.SelectedItems[0].Tag;
            if (release.downloadLink.StartsWith("https://"))
            {
                Process.Start(release.downloadLink);
            }
        }
    }
}
