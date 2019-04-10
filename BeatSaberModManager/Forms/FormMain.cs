using System;
using System.Linq;
using System.Windows.Forms;
using BeatSaberModManager.Core;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using BeatSaberModManager.DataModels;
using System.Diagnostics;
using System.Drawing;
using SemVer;
using Version = SemVer.Version;
using MaterialSkin.Controls;
using MaterialSkin;

namespace BeatSaberModManager
{
    public partial class FormMain : MaterialForm
    {

        #region Instances
        PathLogic path;
        UpdateLogic updater;
        RemoteLogic remote;
        InstallerLogic installer;
        MaterialSkinManager skinManager;
        bool finishedLoading = false;
        bool darkTheme = false;
        int activeThemeID = 0;
        List<string> defaultMods = new List<string>(new string[] { "songloader", "scoresaber", "beatsaverdownloader" });

        #endregion

        #region Constructor
        public FormMain()
        {
            InitializeComponent();
            path = new PathLogic();
            updater = new UpdateLogic();
            remote = new RemoteLogic();

            skinManager = MaterialSkinManager.Instance;
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            skinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            
            // Show tooltips
            listViewMods.ShowItemToolTips = true;

            if (File.Exists(path.modsListPath))
            {
                var modList = System.IO.File.ReadAllText(path.modsListPath).Split(',');
                foreach (var mod in modList)
                {
                    if (!defaultMods.Contains(mod))
                    {
                        defaultMods.Add(mod.ToLower());
                    }
                }
            }
        }
        #endregion

        #region Loading
        private void FormMain_Load(object sender, EventArgs e)
        {
            SetUITheme(Properties.Settings.Default.Theme, Properties.Settings.Default.DarkTheme);
            toggleTheme.Checked = Properties.Settings.Default.DarkTheme;

            switch (Properties.Settings.Default.Theme)
            {
                case 0:
                    radioThemeBlueGrey.Checked = true;
                    break;

                case 1:
                    radioThemeGreen.Checked = true;
                    break;

                case 2:
                    radioThemeOrange.Checked = true;
                    break;

                case 3:
                    radioThemeBlue.Checked = true;
                    break;

                case 4:
                    radioThemeRed.Checked = true;
                    break;

                default:
                    radioThemeBlueGrey.Checked = true;
                    break;
            }

            try
            {
                new Thread(() => { updater.CheckForUpdates(); }).Start();
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
                item.BackColor = darkTheme ? Color.FromArgb(255, 30, 30, 30) : Color.LightGray;
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

        private void SetUITheme(int themeID, bool darkMode)
        {
            darkTheme = darkMode;
            activeThemeID = themeID;

            if (darkMode)
            {
                skinManager.Theme = MaterialSkinManager.Themes.DARK;
                listViewMods.BackColor = Color.FromArgb(255, 40, 40, 40);
                listViewMods.ForeColor = Color.WhiteSmoke;
                textBoxDirectory.BackColor = Color.FromArgb(255, 40, 40, 40);
            }
            else
            {
                skinManager.Theme = MaterialSkinManager.Themes.LIGHT;
                listViewMods.BackColor = Color.White;
                listViewMods.ForeColor = Color.Black;
                textBoxDirectory.BackColor = Color.WhiteSmoke;
            }

            switch (themeID)
            {
                case 0:
                    // Blue Grey
                    skinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
                    break;

                case 1:
                    // Green
                    skinManager.ColorScheme = new ColorScheme(Primary.Green800, Primary.Green900, Primary.Green500, Accent.LightGreen200, TextShade.WHITE);
                    break;

                case 2:
                    // Orange
                    skinManager.ColorScheme = new ColorScheme(Primary.DeepOrange800, Primary.DeepOrange900, Primary.DeepOrange500, Accent.Orange200, TextShade.WHITE);
                    break;

                case 3:
                    // Blue
                    skinManager.ColorScheme = new ColorScheme(Primary.Blue800, Primary.Blue900, Primary.Blue500, Accent.Blue200, TextShade.WHITE);
                    break;

                case 4:
                    // Red
                    skinManager.ColorScheme = new ColorScheme(Primary.Red800, Primary.Red900, Primary.Red500, Accent.Red200, TextShade.WHITE);
                    break;

                default:
                    // Default - Blue Grey
                    skinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
                    break;
            }

            Properties.Settings.Default.DarkTheme = darkMode;
            Properties.Settings.Default.Theme = themeID;
            Properties.Settings.Default.Save();
            ReRenderListView();
        }
        #endregion

        #region Event Handlers
        private void Installer_StatusUpdate(string status)
        {
            UpdateStatus(status);
            if (status == "Install complete!") { this.Invoke((MethodInvoker)(() => { buttonInstall.Enabled = true; })); }
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
                    item.Checked = true;
                    item.BackColor = darkTheme ? Color.FromArgb(255, 30, 30, 30) : Color.LightGray;
                    item.Text = $"[REQUIRED] {release.title}";
                }
                else
                {
                    item.Text = release.title;
                    item.BackColor = darkTheme ? Color.FromArgb(255, 40, 40, 40) : Color.White;
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

        private void BrowseInstallationButton_Click(object sender, EventArgs e)
        {
            textBoxDirectory.Text = path.ManualFind();
            installer.installDirectory = textBoxDirectory.Text;
        }

        private void ToggleTheme_CheckedChanged(object sender, EventArgs e)
        {
            SetUITheme(activeThemeID, toggleTheme.Checked);
        }

        private void ButtonInstall2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxDirectory.Text))
            {
                MessageBox.Show("No install directory selected!", "No install directory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            buttonInstall.Enabled = false;
            try
            {
                var modList = new List<string>();
                foreach (ListViewItem item in listViewMods.Items)
                {
                    if (item.Checked)
                    {
                        var releaseInfo = (ReleaseInfo)item.Tag;
                        modList.Add(releaseInfo.name);
                    }
                }
                System.IO.File.WriteAllText(System.IO.Path.Combine(Environment.CurrentDirectory, path.modsListPath), string.Join(",", modList));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to save modlist backup, Will not load mods on next startup{Environment.NewLine}{ex}");
            }
            new Thread(() => { installer.Run(); }).Start();
        }

        private void ButtonViewInfo2_Click(object sender, EventArgs e)
        {
            if (listViewMods.SelectedItems.Count == 0) { MessageBox.Show("You have to select a mod first."); return; }
            this.Opacity = 0.8;
            //new FormDetailViewer((ReleaseInfo)listViewMods.SelectedItems[0].Tag).ShowDialog();
            Process.Start(((ReleaseInfo)listViewMods.SelectedItems[0].Tag).infoLink);
            this.Opacity = 1;
        }

        private void CreditBeatmods_Click(object sender, EventArgs e)
        {
            Process.Start("https://beatmods.com/");
        }

        private void CreditContributors_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/beat-saber-modding-group/BeatSaberModInstaller/graphs/contributors");
        }

        private void CreditUmbranox_Click(object sender, EventArgs e)
        {
            Process.Start("https://twitter.com/Umbranoxus");
        }

        private void CreditVanZeben_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/vanZeben");
        }

        private void CreditMaterialSkin_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/IgnaceMaes/MaterialSkin");
        }

        private void DiscordJoinButton_Click(object sender, EventArgs e)
        {
            Process.Start("http://discord.gg/beatsabermods");
        }

        private void ViewInfoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listViewMods.SelectedItems.Count >= 1)
            {
                // Instead of opening the form, simply open the link in browser window
                //new FormDetailViewer((ReleaseInfo)listViewMods.SelectedItems[0].Tag).ShowDialog();
                Process.Start(((ReleaseInfo)listViewMods.SelectedItems[0].Tag).infoLink);
            }
        }

        private void LabelStatus_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(labelStatus.Text);
        }

        private void RadioThemeRed_CheckedChanged(object sender, EventArgs e)
        {
            SetUITheme(4, darkTheme);
        }

        private void RadioThemeGreen_CheckedChanged(object sender, EventArgs e)
        {
            SetUITheme(1, darkTheme);
        }

        private void RadioThemeOrange_CheckedChanged(object sender, EventArgs e)
        {
            SetUITheme(2, darkTheme);
        }

        private void RadioThemeBlue_CheckedChanged(object sender, EventArgs e)
        {
            SetUITheme(3, darkTheme);
        }

        private void RadioThemeBlueGrey_CheckedChanged(object sender, EventArgs e)
        {
            SetUITheme(0, darkTheme);
        }
        #endregion

        private void contextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void directDownloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var release = (ReleaseInfo)listViewMods.SelectedItems[0].Tag;
            if (release.downloadLink.StartsWith("https://"))
            {
                Process.Start(release.downloadLink);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.patreon.com/AssistantMoe");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Process.Start("https://wiki.assistant.moe/about");
        }
    }
}
