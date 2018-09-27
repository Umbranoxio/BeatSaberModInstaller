using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BeatSaberModManager.Dependencies.SimpleJSON;
using Version = SemVer.Version;

namespace BeatSaberModManager.Core
{
    class UpdateLogic
    {
        static readonly string repo = "Umbranoxio/BeatSaberModInstaller";
        static readonly string releaseURL = $"https://api.github.com/repos/{repo}/releases/latest";

        public Version CurrentVersion()
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return new Version(version, true);
        }

        public Version RemoteVersion()
        {
            string resp = Helper.Get(releaseURL);
            var json = JSON.Parse(resp);

            string tag = json["tag_name"];
            return new Version(tag);
        }

        public bool UpdateAvailable()
        {
            Version current = CurrentVersion();
            Version remote = RemoteVersion();

            return remote > current;
        }
    }
}
