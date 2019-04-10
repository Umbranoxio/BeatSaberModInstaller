using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace BeatSaberModManager.Core
{
    /// <summary>
    /// Provides methods related to the OneClick installer for https://beatsaver.com/.
    /// </summary>
    static class OneClickInstaller
    {
        private const string OneClickProviderKey = "OneClick-Provider";
        private const string OneClickInstallerName = "BSMG-BeatSaberModInstaller";

        private const string CustomSongsFolder = "CustomSongs";
        private const string CustomSabersFolder = "CustomSabers";
        private const string CustomPlatformsFolder = "CustomPlatforms";
        private const string CustomAvatarsFolder = "CustomAvatars";

        private static readonly string[] SupportedProtocols = new[] { "modsaber", "beatdrop" };

        private static PathLogic _pathLogic;
        private static PathLogic PathLogic => _pathLogic = _pathLogic ?? new PathLogic();

        private const int HWND_BROADCAST = 0xffff;
        public static readonly int WM_DL_START = RegisterWindowMessage("WM_DL_START");
        public static readonly int WM_DL_SUCCESS = RegisterWindowMessage("WM_DL_SUCCESS");
        public static readonly int WM_DL_FAIL = RegisterWindowMessage("WM_DL_FAIL");

        // Some native util methods to notify the main window about a song download status.
        [DllImport("user32")]
        private static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);
        [DllImport("user32")]
        private static extern int RegisterWindowMessage(string message);

        /// <summary>Checks whether the OneClick installer is registered.</summary>
        /// <returns>True when registered, false otherwise.</returns>
        public static bool CheckRegistered()
        {
            using (var modsaberKey = Registry.ClassesRoot.OpenSubKey(SupportedProtocols.First()))
            {
                if (modsaberKey == null)
                    return false;
                return modsaberKey.GetValue(OneClickProviderKey) is string provider
                    && provider == OneClickInstallerName;
            }
        }

        public static void Register()
        {
            foreach (var protocol in SupportedProtocols)
                RegisterProtocol(protocol);
        }

        private static void RegisterProtocol(string protocol)
        {
            using (var modsaberKey = Registry.ClassesRoot.CreateSubKey(protocol))
            using (var commandKey = modsaberKey.CreateSubKey(@"shell\open\command"))
            {
                modsaberKey.SetValue("URL Protocol", "", RegistryValueKind.String);
                modsaberKey.SetValue(OneClickProviderKey, OneClickInstallerName, RegistryValueKind.String);
                var exeLocation = Assembly.GetEntryAssembly().Location;
                commandKey.SetValue("", $"\"{exeLocation}\" \"--install\" \"%1\"");
            }
        }

        public static void Unregister()
        {
            foreach (var protocol in SupportedProtocols)
            {
                using (var modsaberKey = Registry.ClassesRoot.OpenSubKey(protocol))
                {
                    // Only remove providers which we also registered
                    if (modsaberKey != null
                        && modsaberKey.GetValue(OneClickProviderKey) is string provider
                        && provider == OneClickInstallerName)
                    {
                        Registry.ClassesRoot.DeleteSubKeyTree(protocol);
                    }
                }
            }
        }

        public static void InstallFile(string link)
        {
            var uri = new Uri(link);
            if (!SupportedProtocols.Contains(uri.Scheme))
                return;

            BroadcastDownloadStatus(DownloadStatus.Started);

            bool downloadOk = false;
            if (uri.Scheme == "modsaber")
            {
                switch (uri.Host)
                {
                    case "song":
                        downloadOk = InstallSong(uri.AbsolutePath.Substring(1));
                        break;
                    case "avatar":
                        downloadOk = InstallFile(uri.AbsolutePath.Substring(1), CustomAvatarsFolder);
                        break;
                    case "saber":
                        downloadOk = InstallFile(uri.AbsolutePath.Substring(1), CustomSabersFolder);
                        break;
                    case "platform":
                        downloadOk = InstallFile(uri.AbsolutePath.Substring(1), CustomPlatformsFolder);
                        break;
                }
            }
            else if (uri.Scheme == "beatdrop")
            {
                switch (uri.Host)
                {
                    case "download":
                        downloadOk = InstallSong(uri.AbsolutePath);
                        break;
                }
            }

            if (downloadOk)
                BroadcastDownloadStatus(DownloadStatus.Succeeded);
            else
                BroadcastDownloadStatus(DownloadStatus.Failed);
        }

        private static bool InstallSong(string id)
        {
            // Get the base folder for all custom stuff
            var bsPath = PathLogic.GetInstallationPath();
            if (string.IsNullOrEmpty(bsPath))
                return false;

            try
            {
                // Create an id folder for the song
                var songPath = Path.Combine(bsPath, CustomSongsFolder, id);
                Directory.CreateDirectory(songPath);

                // Donwload and extract
                var data = Helper.GetFile($"https://beatsaver.com/download/{id}");
                Helper.UnzipFile(data, songPath);
            }
            catch
            {
                return false;
            }

            return true;
        }

        private static bool InstallFile(string link, string folder)
        {
            // Get the base folder for all custom stuff
            var bsPath = PathLogic.GetInstallationPath();
            if (string.IsNullOrEmpty(bsPath))
                return false;

            try
            {
                // Create the folder and get the file name
                var filePath = Path.Combine(bsPath, folder);
                Directory.CreateDirectory(filePath);
                var uri = new Uri(link);
                var fileName = Path.Combine(filePath, uri.Segments.Last());

                // Donwload and extract
                var data = Helper.GetFile(link);
                File.WriteAllBytes(fileName, data);
            }
            catch
            {
                return false;
            }

            return true;
        }

        private static void BroadcastDownloadStatus(DownloadStatus status)
        {
            int message;
            switch (status)
            {
                case DownloadStatus.Started: message = WM_DL_START; break;
                case DownloadStatus.Succeeded: message = WM_DL_SUCCESS; break;
                case DownloadStatus.Failed: message = WM_DL_FAIL; break;
                default: return;
            }
            PostMessage((IntPtr)HWND_BROADCAST, message, IntPtr.Zero, IntPtr.Zero);
        }

        private enum DownloadStatus
        {
            Started,
            Succeeded,
            Failed,
        }
    }
}
