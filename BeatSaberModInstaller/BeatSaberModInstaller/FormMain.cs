using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;
using BeatSaberModInstaller.Internals;
using System.Diagnostics;
using System.Runtime.InteropServices;
using BeatSaberModInstaller.Internals.SimpleJSON;
namespace BeatSaberModInstaller
{
    public partial class FormMain : Form
    {
        public const string BaseEndpoint = "https://api.github.com/repos/";
        public const Int16 CurrentVersion = 8;
        public List<ReleaseInfo> releases;
        public string InstallDirectory = @"";
        public bool isSteam = true;
        public bool platformDetected = false;
        public FormMain()
        {
            InitializeComponent();
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            LocationHandler();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            releases = new List<ReleaseInfo>();
            new Thread(() =>
            {
                LoadRequiredPlugins();
            }).Start();
        }

        #region ReleaseHandling
        private void LoadReleases()
        {
            var decoded = JSON.Parse(DownloadSite("https://raw.githubusercontent.com/Umbranoxio/BeatSaberModInstaller/master/mods.json"));
            int totalMods = decoded["totalMods"];
            for (int i = 0; i < totalMods; i++)
            {
                JSONNode current = decoded[i.ToString()];
                ReleaseInfo release = new ReleaseInfo(null, null, null, true, null, current["gitPath"],
                    current["releaseId"], current["tag"]);
                release = UpdateReleaseInfo(release.ReleaseId, release.GitPath, release.Tag);
                releases.Add(release);
            }
            //WriteReleasesToDisk();
        }
        private void LoadRequiredPlugins()
        {
            CheckVersion();
            UpdateStatus("Getting latest version info...");
            LoadReleases();
            this.Invoke((MethodInvoker)(() =>
            {//Invoke so we can call from current thread
             //Update checkbox's text
                foreach (ReleaseInfo release in releases)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = release.Name;
                    if (release.Tag != string.Empty) { item.Text = string.Format("{0} - ({1})",release.Name, release.Tag); };
                    item.SubItems.Add(release.Author);
                    item.Tag = release;
                    if (release.Install)
                    {
                        listViewMods.Items.Add(item);
                    }
                    CheckDefaultMod(release, item);
                }
                tabControlMain.Enabled = true;
                buttonInstall.Enabled = true;

            }));
           
            UpdateStatus("Release info updated!");

        }

        private ReleaseInfo UpdateReleaseInfo(int downloadId, string release, string tag)
        {
            string releaseFormatted = BaseEndpoint + release + "/releases";
            var ScoreSaberData = JSON.Parse(DownloadSite(releaseFormatted));
            var rootNode = ScoreSaberData[0];
            var tagName = rootNode["tag_name"];
            var name = rootNode["name"];
            var assetsNode = rootNode["assets"];
            var downloadReleaseNode = assetsNode[downloadId];
            var downloadLink = downloadReleaseNode["browser_download_url"];
            var uploaderNode = downloadReleaseNode["uploader"];
            var author = uploaderNode["login"];
            ReleaseInfo newRelease = new ReleaseInfo(tagName, downloadLink, name, true, author, release, downloadId, tag);

            if (isSteam == false)
            {
                if (newRelease.Tag.ToLower().Contains("steam"))
                {
                    newRelease.Install = false;
                }
            } else {
                if (newRelease.Tag.ToLower().Contains("oculus"))
                {
                    newRelease.Install = false;
                }
            }

            Thread.Sleep(500); //So we don't get rate limited by github
            return newRelease;
        }
     
        #endregion

        #region Installation
        private void Install()
        {
            ChangeInstallButtonState(false);
            UpdateStatus("Starting install sequence...");
            foreach (ReleaseInfo release in releases)
            {
                if (release.Install)
                {
                    UpdateStatus(string.Format("Downloading...{0}", release.Name));
                    byte[] file = DownloadFile(release.Link);
                    UpdateStatus(string.Format("Moving...{0}", release.Name));
                    string fileName = Path.GetFileName(release.Link);
                    if (release.Link.Contains(".dll"))
                    {
                        File.WriteAllBytes(InstallDirectory + @"\Plugins\" + fileName, file);
                    }
                    else
                    {
                        UnzipFile(file, InstallDirectory);
                    }
                    UpdateStatus(string.Format("Moved!{0}", release.Name));
                }
            }
            Process.Start(InstallDirectory + @"\IPA.exe", Quoted(InstallDirectory + @"\Beat Saber.exe"));
            UpdateStatus("Install complete!");
            ChangeInstallButtonState(true);
        }
       
        #endregion

        #region UIEvents
        private void buttonInstall_Click(object sender, EventArgs e)
        {
            if (checkBoxUninstallPrevMods.Checked == true)
            { UninstallPrevMods(); }
            new Thread(() =>
            {
                Install();
            }).Start();
        }
        private void buttonFolderBrowser_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = folderDialog.SelectedPath;
                    if (File.Exists(path + @"\Beat Saber.exe"))
                    {
                        InstallDirectory = folderDialog.SelectedPath;
                        textBoxDirectory.Text = folderDialog.SelectedPath;
                    }
                    else
                    {
                        MessageBox.Show("The directory you selected doesn't contain Beat Saber.exe! please try again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }

            }
        }
        private void listViewMods_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ReleaseInfo release = (ReleaseInfo)e.Item.Tag;
            if (release.Link.Contains("BeatSaberSongLoader")) { e.Item.Checked = true; };
            release.Install = e.Item.Checked;
        }
         private void listViewMods_DoubleClick(object sender, EventArgs e)
        {
            OpenLinkFromRelease();
        }
         private void viewInfoToolStripMenuItem_Click(object sender, EventArgs e)
         {
             OpenLinkFromRelease();
         }

        private void UninstallPrevMods()
        {
            string[] modsToUninstall = Directory.GetFiles(InstallDirectory + @"\Plugins");
            int modToUninstall = 0;
            foreach (string mod in modsToUninstall)
            {
                if (File.Exists(modsToUninstall[modToUninstall]))
                {
                    File.Delete(modsToUninstall[modToUninstall]);
                }
                modToUninstall++;
            }
        }
        #endregion

        #region Helpers
        
        private CookieContainer PermCookie;
        private string DownloadSite(string URL)
        {
            try
            {
                if (PermCookie == null) { PermCookie = new CookieContainer(); }
                HttpWebRequest RQuest = (HttpWebRequest)HttpWebRequest.Create(URL);
                RQuest.Method = "GET";
                RQuest.KeepAlive = true;
                RQuest.CookieContainer = PermCookie;
                RQuest.ContentType = "application/x-www-form-urlencoded";
                RQuest.Referer = "";
                RQuest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
                RQuest.Proxy = null;
                HttpWebResponse Response = (HttpWebResponse)RQuest.GetResponse();
                StreamReader Sr = new StreamReader(Response.GetResponseStream());
                string Code = Sr.ReadToEnd();
                Sr.Close();
                return Code;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("403"))
                {
                    MessageBox.Show("Failed to update version info, GitHub has rate limited you, please check back in 15 - 30 minutes", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Failed to update version info, please check your internet connection", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Process.GetCurrentProcess().Kill();
                return null;
            }
        }
        private void UnzipFile(byte[] data, string directory)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                using (var unzip = new Unzip(ms))
                {
                    unzip.ExtractToDirectory(directory);
                }
            }
        }
        private byte[] DownloadFile(string url)
        {
            WebClient client = new WebClient();
            client.Proxy = null;
            return client.DownloadData(url);
        }
        private void UpdateStatus(string status)
        {
            string formattedText = string.Format("Status: {0}", status);
            this.Invoke((MethodInvoker)(() =>
            { //Invoke so we can call from any thread
                labelStatus.Text = formattedText;
            }));
        }
  
        public string Quoted(string str)
        {
            return "\"" + str + "\"";
        }
        private void NotFoundHandler()
        {
            bool found = false;
            while (found == false)
            {
                using (var folderDialog = new FolderBrowserDialog())
                {
                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        string path = folderDialog.SelectedPath;
                        if (File.Exists(path + @"\Beat Saber.exe"))
                        {
                            textBoxDirectory.Text = folderDialog.SelectedPath;
                            InstallDirectory = folderDialog.SelectedPath;
                            FormSelectPlatform selector = new FormSelectPlatform(this);
                            selector.ShowDialog();
                            found = true;
                        }
                        else
                        {
                            MessageBox.Show("The directory you selected doesn't contain Beat Saber.exe! please try again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }
            }
        }
        private void CheckVersion()
        {
            UpdateStatus("Checking for updates...");
            Int16 version = Convert.ToInt16(DownloadSite("https://raw.githubusercontent.com/Umbranoxio/BeatSaberModInstaller/master/update.txt"));
            if (version > CurrentVersion)
            {
                this.Invoke((MethodInvoker)(() =>
                {
                    MessageBox.Show("Your version of the mod installer is outdated! Please download the new one!", "Update available!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Process.Start("https://github.com/Umbranoxio/BeatSaberModInstaller/releases");
                    Process.GetCurrentProcess().Kill();
                    Environment.Exit(0);
                }));
            }
        }
        private void ChangeInstallButtonState(bool enabled)
        {
            this.Invoke((MethodInvoker)(() =>
                {
                    buttonInstall.Enabled = enabled;
                }));
        }
        private void WriteReleasesToDisk()
        {
            JSONNode root = new JSONObject();
            root["totalMods"] = releases.Count;
            foreach (ReleaseInfo release in releases)
            {
                JSONNode item = new JSONObject();
                item["name"] = release.Name;
                item["author"] = release.Author;
                item["version"] = release.Version;
                item["tag"] = release.Tag;
                item["gitPath"] = release.GitPath;
                item["releaseId"] = release.ReleaseId;
                root.Add(releases.IndexOf(release).ToString(), item);
            }
            StringBuilder builder = new StringBuilder();
            root.WriteToStringBuilder(builder, 1,1, JSONTextMode.Indent);
            System.IO.File.WriteAllText("mods.json", builder.ToString());
        }
        private void OpenLinkFromRelease()
        {
            if (listViewMods.SelectedItems.Count > 0)
            {
                ReleaseInfo release = (ReleaseInfo)listViewMods.SelectedItems[0].Tag;
                Process.Start(string.Format("https://github.com/{0}", release.GitPath));
            }
            
        }
        #endregion

        #region Registry
        private void LocationHandler()
        {
            string steam = GetSteamLocation();
            string oculus = GetOculusHomeLocation();
            if (steam != null)
            {
                if (Directory.Exists(steam))
                {
                    if (File.Exists(steam + @"\Beat Saber.exe"))
                    {
                        textBoxDirectory.Text = steam;
                        InstallDirectory = steam;
                        platformDetected = true;
                        return;
                    }
                }
            }
            if (oculus != null)
            {
                if (Directory.Exists(oculus))
                {
                    if (File.Exists(oculus + @"\Beat Saber.exe"))
                    {
                        textBoxDirectory.Text = oculus;
                        InstallDirectory = oculus;
                        isSteam = false;
                        platformDetected = true;
                        return;
                    }
                }
            }
            ShowErrorFindingDirectoryMessage();
        }
        private void ShowErrorFindingDirectoryMessage()
        {
            MessageBox.Show("We couldn't seem to find your beatsaber installation, please press \"OK\" and point us to it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            NotFoundHandler();
            this.TopMost = true;
        }
        private string GetSteamLocation()
        {
            string path = RegistryWOW6432.GetRegKey64(RegHive.HKEY_LOCAL_MACHINE, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 620980", @"InstallLocation");
            if (path != null)
            {
                path = path + @"\";
            }
            return path;
        }
        private string GetOculusHomeLocation()
        {
            string path = RegistryWOW6432.GetRegKey32(RegHive.HKEY_LOCAL_MACHINE, @"SOFTWARE\Oculus VR, LLC\Oculus\Config", @"InitialAppLibrary");
            if (path != null)
            {
                path = path + @"\Software\hyperbolic-magnetism-beat-saber";
            }
            return path;
        }
        private void CheckDefaultMod(ReleaseInfo release, ListViewItem item)
        {
            if (release.Link.Contains("BeatSaberSongLoader") || release.GitPath.Contains("ScoreSaber") || release.GitPath.Contains("BeatSaverDownloader"))
            {
                item.Checked = true;
            }
            else
            {
                release.Install = false;
            }
        }
        #endregion

        #region RegHelper
        enum RegSAM
        {
            QueryValue = 0x0001,
            SetValue = 0x0002,
            CreateSubKey = 0x0004,
            EnumerateSubKeys = 0x0008,
            Notify = 0x0010,
            CreateLink = 0x0020,
            WOW64_32Key = 0x0200,
            WOW64_64Key = 0x0100,
            WOW64_Res = 0x0300,
            Read = 0x00020019,
            Write = 0x00020006,
            Execute = 0x00020019,
            AllAccess = 0x000f003f
        }

        static class RegHive
        {
            public static UIntPtr HKEY_LOCAL_MACHINE = new UIntPtr(0x80000002u);
            public static UIntPtr HKEY_CURRENT_USER = new UIntPtr(0x80000001u);
        }

        static class RegistryWOW6432
        {
            [DllImport("Advapi32.dll")]
            static extern uint RegOpenKeyEx(UIntPtr hKey, string lpSubKey, uint ulOptions, int samDesired, out int phkResult);

            [DllImport("Advapi32.dll")]
            static extern uint RegCloseKey(int hKey);

            [DllImport("advapi32.dll", EntryPoint = "RegQueryValueEx")]
            public static extern int RegQueryValueEx(int hKey, string lpValueName, int lpReserved, ref uint lpType, System.Text.StringBuilder lpData, ref uint lpcbData);

            static public string GetRegKey64(UIntPtr inHive, String inKeyName, string inPropertyName)
            {
                return GetRegKey64(inHive, inKeyName, RegSAM.WOW64_64Key, inPropertyName);
            }

            static public string GetRegKey32(UIntPtr inHive, String inKeyName, string inPropertyName)
            {
                return GetRegKey64(inHive, inKeyName, RegSAM.WOW64_32Key, inPropertyName);
            }

            static public string GetRegKey64(UIntPtr inHive, String inKeyName, RegSAM in32or64key, string inPropertyName)
            {
                //UIntPtr HKEY_LOCAL_MACHINE = (UIntPtr)0x80000002;
                int hkey = 0;

                try
                {
                    uint lResult = RegOpenKeyEx(RegHive.HKEY_LOCAL_MACHINE, inKeyName, 0, (int)RegSAM.QueryValue | (int)in32or64key, out hkey);
                    if (0 != lResult) return null;
                    uint lpType = 0;
                    uint lpcbData = 1024;
                    StringBuilder AgeBuffer = new StringBuilder(1024);
                    RegQueryValueEx(hkey, inPropertyName, 0, ref lpType, AgeBuffer, ref lpcbData);
                    string Age = AgeBuffer.ToString();
                    return Age;
                }
                finally
                {
                    if (0 != hkey) RegCloseKey(hkey);
                }
            }
        }
        #endregion
        
    }

}
