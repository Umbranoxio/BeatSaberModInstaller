using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Windows.Forms;
using BeatSaberModManager.Dependencies;
using Microsoft.Win32;

namespace BeatSaberModManager.Core
{
    public class PathLogic
    {
        public string installPath;
        public Platform platform;

        private const int SteamAppId = 620980;
        private const string AppFileName = "Beat Saber.exe";


        public string GetInstallationPath()
        {
            string steam = GetSteamLocation();
            if (steam != null)
            {
                if (Directory.Exists(steam))
                {
                    if (File.Exists(Path.Combine(steam, AppFileName)))
                    {
                        platform = Platform.Steam;
                        installPath = steam;
                        return steam;
                    }
                }
            }

            string oculus = GetValidOculusLocation();
            if (oculus != null)
            {
                if (Directory.Exists(oculus))
                {
                    if (File.Exists(Path.Combine(oculus, AppFileName)))
                    {
                        platform = Platform.Oculus;
                        installPath = oculus;
                        return oculus;
                    }
                }
            }

            string fallback = GetFallbackDirectory();
            installPath = fallback;
            return fallback;
        }
        private string GetFallbackDirectory()
        {
            MessageBox.Show("We couldn't seem to find your beatsaber installation, please press \"OK\" and point us to it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return NotFoundHandler();
        }
        private string GetSteamLocation()
        {
            try
            {
                var steamFinder = new SteamFinder();
                if (!steamFinder.FindSteam())
                    return null;

                return steamFinder.FindGameFolder(SteamAppId);
            } catch (Exception ex)
            {
                return null;
            }
           
        }
        private string GetValidOculusLocation()
        {
            const string subFolderPath = @"Software\hyperbolic-magnetism-beat-saber\";

            string path = Registry.LocalMachine.OpenSubKey("SOFTWARE")?.OpenSubKey("WOW6432Node")?.OpenSubKey("Oculus VR, LLC")?.OpenSubKey("Oculus")?.OpenSubKey("Config")?.GetValue("InitialAppLibrary") as string;

            if (path == null)
            {
                // No Oculus Home detected
                return null;
            }

            // With the old Home
            string folderPath = Path.Combine(path, subFolderPath);
            string fullAppPath = Path.Combine(folderPath, AppFileName);

            if (File.Exists(fullAppPath))
            {
                return folderPath;
            }
            else
            {
                // With the new Home / Dash
                using (RegistryKey librariesKey = Registry.CurrentUser.OpenSubKey("Software")?.OpenSubKey("Oculus VR, LLC")?.OpenSubKey("Oculus")?.OpenSubKey("Libraries"))
                {
                    // Oculus libraries uses GUID volume paths like this "\\?\Volume{0fea75bf-8ad6-457c-9c24-cbe2396f1096}\Games\Oculus Apps", we need to transform these to "D:\Game"\Oculus Apps"
                    WqlObjectQuery wqlQuery = new WqlObjectQuery("SELECT * FROM Win32_Volume");
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher(wqlQuery);
                    Dictionary<string, string> guidLetterVolumes = new Dictionary<string, string>();

                    foreach (ManagementBaseObject disk in searcher.Get())
                    {
                        var diskId = ((string) disk.GetPropertyValue("DeviceID")).Substring(11, 36);
                        var diskLetter = ((string) disk.GetPropertyValue("DriveLetter")) + @"\";

                        if (!string.IsNullOrWhiteSpace(diskLetter))
                        {
                            guidLetterVolumes.Add(diskId, diskLetter);
                        }
                    }

                    // Search among the library folders
                    foreach (string libraryKeyName in librariesKey.GetSubKeyNames())
                    {
                        using (RegistryKey libraryKey = librariesKey.OpenSubKey(libraryKeyName))
                        {
                            string libraryPath = (string) libraryKey.GetValue("Path");
                            folderPath = Path.Combine(guidLetterVolumes.First(x => libraryPath.Contains(x.Key)).Value, libraryPath.Substring(49), subFolderPath);
                            fullAppPath = Path.Combine(folderPath, AppFileName);

                            if (File.Exists(fullAppPath))
                            {
                                return folderPath;
                            }
                        }
                    }
                }
            }

            return null;
        }
        private string NotFoundHandler()
        {
            bool found = false;
            while (found == false)
            {
                using (var folderDialog = new FolderBrowserDialog())
                {
                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        string path = folderDialog.SelectedPath;
                        if (File.Exists(Path.Combine(path, AppFileName)))
                        {
                            FormPlatformSelect selector = new FormPlatformSelect(this);
                            selector.ShowDialog();
                            found = true;
                            return path;
                        }
                        else
                        {
                            MessageBox.Show("The directory you selected doesn't contain Beat Saber.exe! please try again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return string.Empty;
        }
        public string ManualFind()
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = folderDialog.SelectedPath;
                    if (File.Exists(Path.Combine(path, AppFileName)))
                    {
                        installPath = path;
                        return folderDialog.SelectedPath;
                    }
                    else
                    {
                        MessageBox.Show("The directory you selected doesn't contain Beat Saber.exe! please try again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            return string.Empty;
        }
    }
}
