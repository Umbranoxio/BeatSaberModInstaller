using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;
using BeatSaberModManager.DataModels;
using BeatSaberModManager.Dependencies.SimpleJSON;
namespace BeatSaberModManager.Core
{
    public class RemoteLogic
    {
        private const string ModSaberURL = "https://www.modsaber.ml";
        private const string ApiVersion = "1.0";
        private readonly string ApiURL = $"{ModSaberURL}/api/v{ApiVersion}";

        private const Int16 CurrentVersion = 12;
        private string currentGameVersion = string.Empty;
        public List<ReleaseInfo> releases;
        public RemoteLogic()
        {
            releases = new List<ReleaseInfo>();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public void GetCurrentGameVersion()
        {
            string raw = Fetch($"{ApiURL}/site/gameversions");
            var decoded = JSON.Parse(raw);
            var current = decoded[0];
            var value = current["value"];
            currentGameVersion = value;
        }

        public void PopulateReleases()
        {
            string raw = GetModSaberReleases();
            if (raw != null)
            {
                var mods = JSON.Parse(raw);
                for (int i = 0; i < mods.Count; i++)
                {
                    var current = mods[i];
                    var files = current["files"];
                    if (files.Count > 1)
                    {
                        var steam = files[0];
                        var oculus = files[1];
                        CreateRelease(new ReleaseInfo(current["title"], current["version"], current["author"],
                        current["description"], current["weight"], current["gameVersion"], steam["url"], current["category"], Platform.Steam));
                        CreateRelease(new ReleaseInfo(current["title"], current["version"], current["author"],
                        current["description"], current["weight"], current["gameVersion"], oculus["url"], current["category"], Platform.Oculus));
                    }
                    else
                    {
                        CreateRelease(new ReleaseInfo(current["title"], current["version"], current["author"],
                        current["description"], current["weight"], current["gameVersion"], files["steam"]["url"], current["category"], Platform.Default));
                    }
                }
            }
        }

        private string Fetch(string URL) => Helper.Get(URL);

        private string GetModSaberReleases()
        {
            string raw = Fetch($"{ApiURL}/mods/approved");
            var decoded = JSON.Parse(raw);
            int lastPage = decoded["lastPage"];

            JSONArray final = new JSONArray();

            for (int i = 0; i <= lastPage; i++)
            {
                string page = Fetch($"{ApiURL}/mods/approved/{i}");
                var pageDecoded = JSON.Parse(page);
                var mods = pageDecoded["mods"];

                foreach (var x in mods)
                    final.Add(x.Value);
            }
            return final.ToString();
        }

        private void CreateRelease(ReleaseInfo release)
        {
            if (release.gameVersion == currentGameVersion)
            {
                releases.Add(release);
            }
        }

        public void CheckVersion()
        {
            //TODO: Don't be lazy and actually make an auto updater
            Int16 version = Convert.ToInt16(Helper.Get(
                "https://raw.githubusercontent.com/Umbranoxio/BeatSaberModInstaller/master/update.txt"));
            if (version > CurrentVersion)
            {
                MessageBox.Show("Your version of the mod installer is outdated! Please download the new one!", "Update available!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Process.Start("https://github.com/Umbranoxio/BeatSaberModInstaller/releases");
                Process.GetCurrentProcess().Kill();
                Environment.Exit(0);
            }
        }
    }
}
