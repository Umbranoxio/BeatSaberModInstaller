using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BeatSaberModManager.Dependencies.SimpleJSON;
using Version = SemVer.Version;

namespace BeatSaberModManager.Core
{
    class UpdateLogic
    {
        static readonly string repo = "Umbranoxio/BeatSaberModInstaller";
        static readonly string releaseURL = $"https://api.github.com/repos/{repo}/releases/latest";

        public JSONNode LatestRelease()
        {
            string resp = Helper.Get(releaseURL);
            return JSON.Parse(resp);
        }

        public Version CurrentVersion()
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return new Version(version, true);
        }

        public Version RemoteVersion()
        {
            JSONNode json = LatestRelease();

            string tag = json["tag_name"];
            return new Version(tag);
        }

        public Version RemoteVersion(JSONNode json)
        {
            string tag = json["tag_name"];
            return new Version(tag);
        }

        public void CheckForUpdates()
        {
            // Define application paths
            string path = Application.ExecutablePath;
            string installDir = Path.GetDirectoryName(path);
            string tempPath = Path.Combine(installDir, "ModManagerOld.exe");

            // Cleanup old releases
            if (File.Exists(tempPath))
                File.Delete(tempPath);

            // Fetch release info
            JSONNode release = LatestRelease();

            // Fetch version info
            Version current = CurrentVersion();
            Version remote = RemoteVersion(release);

            bool updateAvailable = remote > current;
            if (!updateAvailable)
                return;

            // Fetch EXE from release
            JSONArray assets = release["assets"].AsArray;
            JSONObject exe;

            try
            {
                exe = assets.Linq.First(x =>
                {
                    string name = x.Value["name"].ToString().Trim('"');
                    return name.Contains(".exe");
                }).Value.AsObject;
            }
            catch
            {
                // Show handy warning when no EXE can be found
                MessageBox.Show(
                    "A new version of the Mod Manager was found but no update could be downloaded.\n" +
                    "Please check the new release manually.",
                    "Update failed!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );

                Process.Start($"https://github.com/{repo}/releases");
                return;
            }

            // Prompt that a restart is about to happen
            MessageBox.Show(
                "A new version of the Mod Manager is available.\n" +
                "Click OK to install the update." +
                "\n\n" +
                "The mod manager will restart once the update has downloaded.",
                "Update available!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            // Download new version
            string downloadURL = exe["browser_download_url"];
            byte[] newVersion = Helper.GetFile(downloadURL);

            // Move the current version to a temporary file so we can use the same EXE name
            File.Move(path, tempPath);
            File.WriteAllBytes(path, newVersion);

            // Restart with new update
            Process.Start(Application.ExecutablePath);

            Process.GetCurrentProcess().Kill();
            Environment.Exit(0);
        }
    }
}
