using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BeatSaberModManager.Core
{
    /// <summary>
    /// Steam installation path and Steam games folder finder.
    /// </summary>
    class SteamFinder
    {
        public string SteamPath { get; private set; }
        public string[] Libraries { get; private set; }

        /// <summary>
        /// Tries to find the Steam folder and its libraries on the system.
        /// </summary>
        /// <returns>Returns true if a valid Steam installation folder path was found.</returns>
        public bool FindSteam()
        {
            SteamPath = null;

            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                    SteamPath = FindWindowsSteamPath();
                    break;
                default:
                    if (IsUnix())
                        SteamPath = FindUnixSteamPath();
                    break;
            }

            if (SteamPath == null)
                return false;

            return FindLibraries();
        }

        /// <summary>
        /// Retrieves the game folder by reading the game's Steam manifest. The game needs to be marked as installed on Steam.
        /// <para>Returns null if not found.</para>
        /// </summary>
        /// <param name="appId">The game's app id on Steam.</param>
        /// <returns>The path to the game folder.</returns>
        public string FindGameFolder(int appId)
        {
            if (Libraries == null)
                throw new InvalidOperationException("Steam must be found first.");

            foreach (var library in Libraries)
            {
                var gameManifestPath = GetManifestFilePath(library, appId);
                if (gameManifestPath == null)
                    continue;

                var gameFolderName = ReadInstallDirFromManifest(gameManifestPath);
                if (gameFolderName == null)
                    continue;

                return Path.Combine(library, "common", gameFolderName);
            }

            return null;
        }

        /// <summary>
        /// Searches for a game directory that has the specified name in known libraries.
        /// </summary>
        /// <param name="gameFolderName">The game's folder name inside the Steam library.</param>
        /// <returns>The game folders path in the libraries.</returns>
        public IEnumerable<string> FindGameFolders(string gameFolderName)
        {
            if (Libraries == null)
                throw new InvalidOperationException("Steam must be found first.");

            gameFolderName = gameFolderName.ToLowerInvariant();

            foreach (var library in Libraries)
            {
                var folder = Directory.EnumerateDirectories(library)
                    .FirstOrDefault(f => Path.GetFileName(f).ToLowerInvariant() == gameFolderName);

                if (folder != null)
                    yield return folder;
            }
        }

        bool FindLibraries()
        {
            var steamLibraries = new List<string>();
            var steamDefaultLibrary = Path.Combine(SteamPath, "steamapps");
            if (!Directory.Exists(steamDefaultLibrary))
                return false;

            steamLibraries.Add(steamDefaultLibrary);

            /*
             * Get library folders paths from libraryfolders.vdf
             *
             * Libraries are listed like this:
             *     "id"   "library folder path"
             *
             * Examples:
             *     "1"   "D:\\Games\\SteamLibraryOnD"
             *     "2"   "E:\\Games\\steam_games"
             */
            var regex = new Regex(@"""\d+""\s+""(.+)""");
            var libraryFoldersFilePath = Path.Combine(steamDefaultLibrary, "libraryfolders.vdf");
            if (File.Exists(libraryFoldersFilePath))
            {
                foreach (var line in File.ReadAllLines(libraryFoldersFilePath))
                {
                    var match = regex.Match(line);
                    if (!match.Success)
                        continue;

                    var libPath = match.Groups[1].Value;
                    libPath = libPath.Replace("\\\\", "\\"); // unescape the backslashes
                    libPath = Path.Combine(libPath, "steamapps");
                    if (Directory.Exists(libPath))
                        steamLibraries.Add(libPath);
                }
            }

            Libraries = steamLibraries.ToArray();
            return true;
        }

        static string GetManifestFilePath(string libraryPath, int appId)
        {
            var manifestPath = Path.Combine(libraryPath, $"appmanifest_{appId}.acf");
            if (File.Exists(manifestPath))
                return manifestPath;
            else
                return null;
        }

        static string ReadInstallDirFromManifest(string manifestFilePath)
        {
            var regex = new Regex(@"""installdir""\s+""(.+)""");
            foreach (var line in File.ReadAllLines(manifestFilePath))
            {
                var match = regex.Match(line);
                if (!match.Success)
                    continue;

                return match.Groups[1].Value;
            }

            return null;
        }

        static string FindWindowsSteamPath()
        {
            var subRegKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam");
            var path = subRegKey?.GetValue("SteamPath").ToString()
                .Replace('/', '\\'); // not actually required, just for consistency's sake

            if (Directory.Exists(path))
                return path;
            else
                return null;
        }

        static string FindUnixSteamPath()
        {
            string path = null;
            if (Directory.Exists(path = GetDefaultLinuxSteamPath())
                || Directory.Exists(path = GetDefaultMacOsSteamPath()))
            {
                return path;
            }

            return null;
        }

        static string GetDefaultLinuxSteamPath()
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                ".local/share/Steam/"
            );
        }

        static string GetDefaultMacOsSteamPath()
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "Library/Application Support/Steam"
            );
        }

        // https://stackoverflow.com/questions/5116977
        static bool IsUnix()
        {
            var p = (int)Environment.OSVersion.Platform;
            return p == 4 || p == 6 || p == 128;
        }
    }
}
