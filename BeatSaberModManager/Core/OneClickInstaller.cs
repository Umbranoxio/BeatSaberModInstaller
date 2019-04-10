using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace BeatSaberModManager.Core
{
    /// <summary>
    /// Provides methods related to the OneClick installer for https://beatsaver.com/.
    /// </summary>
    static class OneClickInstaller
    {
        private const string OneClickProviderKey = "OneClick-Provider";
        private const string OneClickInstallerName = "BSMG-BeatSaberModInstaller";
        private static readonly Regex BeatSaverId = new Regex(@"(modsaber://song/)?(\d+-\d+)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.ECMAScript);
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
            using (var modsaberKey = Registry.ClassesRoot.OpenSubKey("modsaber"))
            {
                if (modsaberKey == null)
                    return false;
                return modsaberKey.GetValue(OneClickProviderKey) is string provider
                    && provider == OneClickInstallerName;
            }
        }

        public static void Register()
        {
            using (var modsaberKey = Registry.ClassesRoot.CreateSubKey("modsaber"))
            using (var commandKey = modsaberKey.CreateSubKey(@"shell\open\command"))
            {
                modsaberKey.SetValue("URL Protocol", "", RegistryValueKind.String);
                modsaberKey.SetValue(OneClickProviderKey, OneClickInstallerName, RegistryValueKind.String);
                var exeLocation = Assembly.GetEntryAssembly().Location;
                commandKey.SetValue("", $"\"{exeLocation}\" \"--install-song\" \"%1\"");
            }
        }

        public static void Unregister()
        {
            Registry.ClassesRoot.DeleteSubKeyTree("modsaber");
        }

        public static void InstallSong(string idString)
        {
            BroadcastDownloadStatus(DownloadStatus.Started);

            // Check if we got a valid beatsaver id and extract it
            var match = BeatSaverId.Match(idString);
            if (!match.Success)
            {
                BroadcastDownloadStatus(DownloadStatus.Failed);
                return;
            }
            var id = match.Groups[2].Value;

            // Get the custom songs folder
            var bsPath = PathLogic.GetInstallationPath();
            if (string.IsNullOrEmpty(bsPath))
            {
                BroadcastDownloadStatus(DownloadStatus.Failed);
                return;
            }

            try
            {
                // Create an id folder for the song
                var songPath = Path.Combine(bsPath, "CustomSongs", id);
                Directory.CreateDirectory(songPath);

                // Donwload and extract
                var data = Helper.GetFile($"https://beatsaver.com/download/{id}");
                Helper.UnzipFile(data, songPath);
            }
            catch
            {
                BroadcastDownloadStatus(DownloadStatus.Failed);
                return;
            }

            BroadcastDownloadStatus(DownloadStatus.Succeeded);
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
