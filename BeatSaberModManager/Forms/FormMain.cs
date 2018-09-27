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
        #endregion

        #region Constructor
        public FormMain()
        {
            InitializeComponent();
            path = new PathLogic();
            updater = new UpdateLogic();
            remote = new RemoteLogic();
        }
        #endregion

        #region Loading
        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                textBoxDirectory.Text = path.GetInstallationPath();
                
                // TODO: Re-implement update core

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
            UpdateStatus("Loading latest releases...");
            remote.GetCurrentGameVersion();
            remote.PopulateReleases();
            installer = new InstallerLogic(remote.releases, path.installPath);
            installer.StatusUpdate += Installer_StatusUpdate;
            this.Invoke((MethodInvoker)(() => { ShowReleases(); }));
        }

        private void ShowReleases()
        {
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
                    CheckDefaultMod(release, item);
                }
            }

            ListViewGroup[] sortedGroups = new ListViewGroup[this.listViewMods.Groups.Count];

            this.listViewMods.Groups.CopyTo(sortedGroups, 0);
            Array.Sort(sortedGroups, new GroupComparer());

            this.listViewMods.BeginUpdate();
            this.listViewMods.Groups.Clear();
            this.listViewMods.Groups.AddRange(sortedGroups);
            this.listViewMods.EndUpdate();

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
            if (link.Contains("song-loader"))
            {
                item.Text = $"[REQUIRED] {release.title}";
                item.BackColor = Color.LightGray;
                release.disabled = true;
            }
            if (link.Contains("song-loader") || link.Contains("scoresaber") || link.Contains("beatsaver"))
            {
                item.Checked = true;
                release.install = true;
            }
            else
            {
                release.install = false;
            }

            ReRenderListView();
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

            IEnumerable<ListViewItem> lv = listViewMods.Items.Cast<ListViewItem>();

            List<ListViewItem> changedConflicts = new List<ListViewItem>();
            if (release.conflictsWith.Count > 0)
            {
                List<ListViewItem> filtered = lv.Where(lvi =>
                {
                    ReleaseInfo info = (ReleaseInfo)lvi.Tag;

                    bool exists = release.conflictsWith.Exists(l => l.name == info.name);
                    if (!exists)
                        return false;

                    ModLink link = release.conflictsWith.Find(l => l.name == info.name);

                    Version version = new Version(info.version);
                    Range range = new Range(link.semver);

                    return range.IsSatisfied(version);
                }).ToList();

                foreach (var x in filtered)
                {
                    changedConflicts.Add(x);
                    ReleaseInfo info = (ReleaseInfo)x.Tag;
                    info.disabled = e.Item.Checked;
                    info.install = !e.Item.Checked;

                    if (e.Item.Checked)
                        x.Checked = false;
                }
            }

            if (release.dependsOn.Count > 0)
            {
                List<ListViewItem> filtered = lv.Where(lvi =>
                {
                    ReleaseInfo info = (ReleaseInfo)lvi.Tag;
                    if (info.disabled && !info.install)
                        return false;

                    bool exists = release.dependsOn.Exists(l => l.name == info.name);
                    if (!exists)
                        return false;

                    ModLink link = release.dependsOn.Find(l => l.name == info.name);

                    Version version = new Version(info.version);
                    Range range = new Range(link.semver);

                    return range.IsSatisfied(version);
                }).ToList();

                if (filtered.Count != release.dependsOn.Count)
                {
                    release.install = false;
                    release.disabled = true;

                    foreach (var x in changedConflicts)
                    {
                        ReleaseInfo info = (ReleaseInfo)x.Tag;
                        info.disabled = !e.Item.Checked;
                        info.install = e.Item.Checked;
                    }
                }
                else
                {
                    foreach (var x in filtered)
                    {
                        ReleaseInfo info = (ReleaseInfo)x.Tag;
                        info.disabled = e.Item.Checked;
                        info.install = e.Item.Checked;
                        x.Checked = e.Item.Checked;
                    }
                }
            }

            ReRenderListView();
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
            new FormDetailViewer((ReleaseInfo)listViewMods.SelectedItems[0].Tag).ShowDialog();
        }

        private void viewInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewMods.SelectedItems.Count >= 1)
            {
                new FormDetailViewer((ReleaseInfo)listViewMods.SelectedItems[0].Tag).ShowDialog();
            }
        }

        private void linkLabellolPants_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/lolPants");
        }

        private void linkLabelModSaberLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.modsaber.org/faq");
        }

        private void linkLabelUmbranox_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://twitter.com/Umbranoxus");
        }

        private void linkLabelContributors_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/Umbranoxio/BeatSaberModInstaller/graphs/contributors");
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
    }
}
