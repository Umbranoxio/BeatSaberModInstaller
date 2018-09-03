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
        public int weight;
        public string gameVersion;
        public string downloadLink;
        public string category;
        public Platform platform;
        public bool install;
        public ReleaseInfo(string _title, string _version, string _author, string _description, int _weight, string _gameVersion, string _downloadLink, string _category, Platform _platform)
        {
            title = _title;
            version = _version;
            author = _author;
            description = _description;
            weight = _weight;
            gameVersion = _gameVersion;
            downloadLink = _downloadLink;
            category = _category;
            platform = _platform;
        }
    }
}
