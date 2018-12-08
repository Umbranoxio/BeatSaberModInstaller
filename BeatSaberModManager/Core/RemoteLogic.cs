using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using BeatSaberModManager.DataModels;
using BeatSaberModManager.Dependencies.SimpleJSON;
using SemVer;
using Version = SemVer.Version;

namespace BeatSaberModManager.Core
{
    public class RemoteLogic
    {
#if DEBUG
        private const string ModSaberURL = "https://staging.modsaber.org";
#else
        private const string ModSaberURL = "https://www.modsaber.org";
#endif

        private const string ApiVersion = "1.1";
        private readonly string ApiURL = $"{ModSaberURL}/api/v{ApiVersion}";

        public GameVersion[] gameVersions;
        public GameVersion selectedGameVersion;

        public List<ReleaseInfo> releases;

        public RemoteLogic()
        {
            releases = new List<ReleaseInfo>();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public void GetGameVersions()
        {
            string raw = Fetch($"{ApiURL}/site/gameversions");
            var gameVersionsRaw = JSON.Parse(raw);

            List<GameVersion> gvList = new List<GameVersion>();
            for (int i = 0; i < gameVersionsRaw.Count; i++)
            {
                var current = gameVersionsRaw[i];

                GameVersion gv = new GameVersion(
                    current["id"],
                    current["value"],
                    current["manifest"]
                );

                gvList.Add(gv);
            }

            gameVersions = gvList.ToArray();
            selectedGameVersion = gameVersions[0];
        }

        public void PopulateReleases()
        {
            releases.Clear();

            string raw = GetModSaberReleases();
            if (raw != null)
            {
                var mods = JSON.Parse(raw);
                for (int i = 0; i < mods.Count; i++)
                {
                    var current = mods[i];

                    List<ModLink> dependsOn = NodeToLinks(current["links"]["dependencies"]);
                    List<ModLink> conflictsWith = NodeToLinks(current["links"]["conflicts"]);

                    var files = current["files"];
                    if (files.Count > 1)
                    {
                        var steam = files[0];
                        var oculus = files[1];

                        CreateRelease(
                            new ReleaseInfo(current["name"], current["details"]["title"], current["version"], current["details"]["author"]["name"],
                            current["details"]["description"], current["meta"]["weight"], current["gameVersion"]["value"],
                            steam["url"], current["meta"]["category"], Platform.Steam, dependsOn, conflictsWith));

                        CreateRelease(
                            new ReleaseInfo(current["name"], current["details"]["title"], current["version"], current["details"]["author"]["name"],
                            current["details"]["description"], current["meta"]["weight"], current["gameVersion"]["value"],
                            oculus["url"], current["meta"]["category"], Platform.Oculus, dependsOn, conflictsWith));
                    }
                    else
                    {
                        CreateRelease(
                            new ReleaseInfo(current["name"], current["details"]["title"], current["version"], current["details"]["author"]["name"],
                            current["details"]["description"], current["meta"]["weight"], current["gameVersion"]["value"],
                            files["steam"]["url"], current["meta"]["category"], Platform.Default, dependsOn, conflictsWith));
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

        private List<ModLink> NodeToLinks(JSONNode node)
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
            string raw = Fetch($"{ApiURL}/mods/approved/all");
            var decoded = JSON.Parse(raw);
            int lastPage = decoded["lastPage"];

            JSONArray final = new JSONArray();

            for (int i = 0; i <= lastPage; i++)
            {
                string page = Fetch($"{ApiURL}/mods/approved/all/{i}");
                var pageDecoded = JSON.Parse(page);
                var mods = pageDecoded["mods"];

                foreach (var x in mods)
                    final.Add(x.Value);
            }
            return final.ToString();
        }

        private void CreateRelease(ReleaseInfo release)
        {
            if (release.gameVersion != selectedGameVersion.value)
                return;

            ReleaseInfo current = releases.Find(x => x.name == release.name);
            if (current == null)
            {
                releases.Add(release);
                return;
            }

            Version currentVersion = new Version(current.version);
            Version newVersion = new Version(release.version);
           
            if (release.platform == Platform.Default )
            {
                releases.Remove(current);
                releases.Add(release);
            } else
            {
                if (currentVersion < newVersion)
                {
                    releases.Remove(current);
                }
                releases.Add(release);
            }
           
        }
    }
}
