using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatSaberModManager.DataModels
{
    public class ReleaseInfo
    {
        public string title;
        public string version;
        public string author;
        public string description;
        public string gameVersion;
        public string downloadLink;
        public Platform platform;
        public bool install;
        public ReleaseInfo(string _title, string _version, string _author, string _description, string _gameVersion, string _downloadLink, Platform _platform)
        {
            title = _title;
            version = _version;
            author = _author;
            description = _description;
            gameVersion = _gameVersion;
            downloadLink = _downloadLink;
            platform = _platform;
        }
    }
}
