using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;
using BeatSaberModInstaller.Internals;
using System.Linq;
using System.Diagnostics;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace BeatSaberModInstaller
{
    public partial class FormMain : Form
    {

        public const string Git = "https://api.github.com/repos/";
        public const string Injector = "xyonico/BeatSaberSongInjector/releases";
        public const string ScoreSaver = "andruzzzhka/BeatSaverDownloader/releases";
        public const string ScoreSaber = "Umbranoxio/ScoreSaber/releases";
        public const int ScoreSaberOculusDownloadID = 0;
        public const int ScoreSaberSteamDownloadID = 1;
        public const Int16 CurrentVersion = 3;
        public List<ReleaseInfo> releases;

        public string InstallDirectory = @"";
        public bool isSteam = true;
        public FormMain()
        {
            InitializeComponent();
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            LocationHandler();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            releases = new List<ReleaseInfo>();
            radioButtonScoreSaberSteam.Checked = true;
            new Thread(() =>
            {
                GetStartupInformation();
            }).Start();
        }

        #region ReleaseHandling
        private void GetStartupInformation()
        {
            CheckVersion();
            UpdateStatus("Getting latest version info...");
            ReleaseInfo injectorRelease = GetLatestRelease(1, Injector);
            ReleaseInfo scoreSaverRelease = GetLatestRelease(0, ScoreSaver);
            ReleaseInfo scoreSaberReleaseOculus = GetLatestRelease(ScoreSaberOculusDownloadID, ScoreSaber);
            ReleaseInfo scoreSaberReleaseSteam = GetLatestRelease(ScoreSaberSteamDownloadID, ScoreSaber);
            releases.Add(injectorRelease);
            releases.Add(scoreSaverRelease);
            releases.Add(scoreSaberReleaseOculus);
            releases.Add(scoreSaberReleaseSteam);

            string exampleToShowReleaseInfoContainsDownloadLinkAswell = injectorRelease.Link;

            this.Invoke((MethodInvoker)(() =>
            {//Invoke so we can call from current thread
                //Update checkbox's text
                checkBoxBeatSaver.Text = string.Format(checkBoxBeatSaver.Text, scoreSaverRelease.Version);
                checkBoxSongLoader.Text = string.Format(checkBoxSongLoader.Text, injectorRelease.Version);
                radioButtonScoreSaberOculus.Text = string.Format(radioButtonScoreSaberOculus.Text, scoreSaberReleaseOculus.Version);
                radioButtonScoreSaberSteam.Text = string.Format(radioButtonScoreSaberSteam.Text, scoreSaberReleaseSteam.Version);
                //Let the user use the controls as they where
                checkBoxBeatSaver.Enabled = true;
                radioButtonScoreSaberOculus.Enabled = true;
                radioButtonScoreSaberSteam.Enabled = true;

                if (!isSteam)
                {
                    radioButtonScoreSaberOculus.Checked = true;
                }
                
                buttonInstall.Enabled = true;
            }));
            UpdateStatus("Release info updated!");

        }

        private ReleaseInfo GetLatestRelease(int downloadId, string release)
        {
            //Terrible handling of the json but it works, feel free to fix this
            string releaseFormatted = Git + release;
            var ScoreSaberData = BeatSaberModInstaller.Internals.SimpleJSON.JSON.Parse(DownloadSite(releaseFormatted));

            var rootNode = ScoreSaberData[0];
            var tagName = rootNode[5];
            var name = rootNode[7];
            var assetsNode = rootNode[13];
            var downloadReleaseNode = assetsNode[downloadId];
            var downloadLink = downloadReleaseNode[11];
            Thread.Sleep(500); //So we don't get rate limited by github

            if (downloadId == ScoreSaberOculusDownloadID && release == ScoreSaber)
            {
                return new ReleaseInfo(tagName, downloadLink, name, false);
            }
            else
            {
                return new ReleaseInfo(tagName, downloadLink, name, true);
            }
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
            new Thread(() =>
            {
                Install();
            }).Start();
        }

        private void checkBoxBeatSaver_CheckedChanged(object sender, EventArgs e)
        {
            UpdateRelease("beatsaver", checkBoxBeatSaver.Checked);
        }

        private void radioButtonScoreSaberSteam_CheckedChanged(object sender, EventArgs e)
        {
            UpdateRelease("scoresabersteam", radioButtonScoreSaberSteam.Checked);
        }

        private void radioButtonScoreSaberOculus_CheckedChanged(object sender, EventArgs e)
        {
            UpdateRelease("scoresabero", radioButtonScoreSaberOculus.Checked);
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
        #endregion

        #region Helpers
        
        private CookieContainer PermCookie;
        private string DownloadSite(string URL)
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
        private void UpdateRelease(string releaseName, bool isEnabled)
        {
            var index = releases.FindIndex(c => c.Link.ToLower().Contains(releaseName));
            if (index != -1)
            {
                releases[index].Install = isEnabled;
            }
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
