﻿using System;
using System.Windows.Forms;
using BeatSaberModManager.Core;
using System.Threading;
using System.Collections.Generic;
using BeatSaberModManager.DataModels;
using System.Diagnostics;
using System.Net;

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
                CheckInstallerVersion();
                new Thread(() => { RemoteLoad(); }).Start();
            } catch (Exception ex)
            {
                MessageBox.Show("Failed to start, error: " + ex.ToString());
                Environment.Exit(0);
            }
        }

        private void CheckInstallerVersion()
        {
            var isLastVersion = false;

            try
            {
                isLastVersion = remote.CheckIfLatestInstallerVersion();
            }
            catch (WebException)
            {
                MessageBox.Show(this,
                    "Failed to get the latest Mod Manager version info! Please check your internet connection!",
                    "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
                return;
            }

            if (!isLastVersion)
            {
                MessageBox.Show(this,
                    "Your version of the mod installer is outdated! Please download the new one!",
                    "Update available!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Process.Start("https://github.com/Umbranoxio/BeatSaberModInstaller/releases");
                Environment.Exit(1);
            }
        }

        private void RemoteLoad()
        {
            UpdateStatus("Loading latest releases...");

            try
            {
                remote.GetCurrentGameVersion();
                remote.PopulateReleases();
            }
            catch (WebException)
            {
                ShowModSaberConnectionError();
                Environment.Exit(1);
                return;
            }

            installer = new InstallerLogic(remote.releases, path.installPath);
            installer.StatusUpdate += Installer_StatusUpdate;
            this.Invoke((MethodInvoker)(() => { ShowReleases(); }));
        }

        private void ShowModSaberConnectionError()
        {
            this.Invoke((MethodInvoker)(() =>
            {
                MessageBox.Show(this,
                    "Unable to connect to ModSaber!\n"
                        + "modsaber.ml may be currently unavailable.\n\n"
                        + "Please also check your internet connection.",
                    "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }));
        }

        private void ShowReleases()
        {
            Dictionary<string, int> groups = new Dictionary<string, int>();

            listViewMods.Groups.Clear();
            int other = listViewMods.Groups.Add(new ListViewGroup("Other", HorizontalAlignment.Left));
            groups.Add("Other", other);

            foreach (ReleaseInfo release in remote.releases)
            {
                ListViewItem item = new ListViewItem();
                item.Text = release.title;
                item.SubItems.Add(release.author);
                item.SubItems.Add(release.version);
                item.Tag = release;

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
                item.Text = item.Text + " (required)";
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
            Process.Start("https://www.modsaber.ml/#/faq");
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
