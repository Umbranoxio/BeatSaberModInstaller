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
        private const string BeatModsURL = "https://beatmods.com";
#else
        private const string BeatModsURL = "https://beatmods.com";
#endif

        private const string ApiVersion = "1";
        private readonly string ApiURL = $"{BeatModsURL}/api/v{ApiVersion}";

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

            string raw = GetBeatModsReleases();
            if (raw != null)
            {
                var mods = JSON.Parse(raw);
                for (int i = 0; i < mods.Count; i++)
                {
                    var current = mods[i];

                    List<ModLink> dependsOn = NodeToLinks(current["dependencies"]);
                    List<ModLink> conflictsWith = NodeToLinks(current["conflicts"]);

                    var files = current["downloads"];

                    JSONNode steam = files[0];
                    JSONNode oculus = files[1];

                    for (var f = 0; f < files.Count; ++f)
                    {
                        files[f]["url"] = BeatModsURL + files[f]["url"];
                    }

                    if (files.Count > 1)
                    {
                        for (var f = 0; f < files.Count; ++f)
                        {
                            if (files[f]["type"] == "steam") steam = files[f];
                            if (files[f]["type"] == "oculus") oculus = files[f];
                        }

                        CreateRelease(
                            new ReleaseInfo(current["name"], current["name"], current["version"], current["author"]["username"],
                            current["description"], 0, "0.13.2", steam["url"],
                            current["category"], Platform.Steam, dependsOn, conflictsWith));

                        CreateRelease(
                            new ReleaseInfo(current["name"], current["name"], current["version"], current["author"]["username"],
                            current["description"], 0, "0.13.2", oculus["url"],
                            current["category"], Platform.Oculus, dependsOn, conflictsWith));
                    }
                    else
                    {
                        CreateRelease(
                            new ReleaseInfo(current["name"], current["name"], current["version"], current["author"]["username"],
                            current["description"], 0, "0.13.2", files[0]["url"],
                            current["category"], Platform.Default, dependsOn, conflictsWith));
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
                var parsed = JSON.Parse(str);
                ModLink link = new ModLink(parsed["name"], parsed["version"]);
                links.Add(link);
            }

            return links;
        }

        private string Fetch(string URL) => Helper.Get(URL);

        private string GetBeatModsReleases()
        {
            string raw = Fetch($"{ApiURL}/mod?status=approved");
            var decoded = JSON.Parse(raw);
            return decoded.ToString();
        }

        private void CreateRelease(ReleaseInfo release)
        {
            //if (release.gameVersion != selectedGameVersion.value)
            //    return;

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
