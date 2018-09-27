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
        private const string ModSaberURL = "https://www.modsaber.org";
        private const string ApiVersion = "1.0";
        private readonly string ApiURL = $"{ModSaberURL}/api/v{ApiVersion}";

        private const Int16 CurrentVersion = 14;
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

                    List<ModLink> dependsOn = NodeToLinks(current["dependsOn"]);
                    List<ModLink> conflictsWith = NodeToLinks(current["conflictsWith"]);

                    var files = current["files"];
                    if (files.Count > 1)
                    {
                        var steam = files[0];
                        var oculus = files[1];

                        CreateRelease(
                            new ReleaseInfo(current["name"], current["title"], current["version"], current["author"],
                            current["description"], current["weight"], current["gameVersion"],
                            steam["url"], current["category"], Platform.Steam, dependsOn, conflictsWith));

                        CreateRelease(
                            new ReleaseInfo(current["name"], current["title"], current["version"], current["author"],
                            current["description"], current["weight"], current["gameVersion"],
                            oculus["url"], current["category"], Platform.Oculus, dependsOn, conflictsWith));
                    }
                    else
                    {
                        CreateRelease(
                            new ReleaseInfo(current["name"], current["title"], current["version"], current["author"],
                            current["description"], current["weight"], current["gameVersion"],
                            files["steam"]["url"], current["category"], Platform.Default, dependsOn, conflictsWith));
                    }
                }
            }
        }

        private string[] AsArray(JSONArray arrayJson)
        {
            string[] array = new string[arrayJson.Count];
            int index = 0;
            foreach (JSONNode node in arrayJson)
            {
                array[index] = (string)node.ToString().Trim('"');
                index += 1;
                    }
            return array;
                }

        private List<ModLink> NodeToLinks (JSONNode node)
        {
            string[] arr = AsArray(node.AsArray);
            List<ModLink> links = new List<ModLink>();

            foreach (string str in arr)
            {
                string[] split = str.Split('@');
                ModLink link = new ModLink(split[0], split[1]);
                links.Add(link);
            }

            return links;
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
                releases.Add(release);
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
