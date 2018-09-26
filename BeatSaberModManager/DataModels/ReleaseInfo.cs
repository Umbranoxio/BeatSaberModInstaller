using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatSaberModManager.DataModels
{
    public class ReleaseInfo
    {
        public string name;
        public string title;
        public string author;

        public string version;
        public string description;

        public int weight;
        public string category;

        public string gameVersion;
        public string downloadLink;

        public Platform platform;

        public bool install;
        public bool disabled;

        public List<ModLink> dependsOn;
        public List<ModLink> conflictsWith;

        public ReleaseInfo(
            string _name, string _title, string _version, string _author, string _description,
            int _weight, string _gameVersion, string _downloadLink, string _category,
            Platform _platform, List<ModLink> _dependsOn, List<ModLink> _conflictsWith
        )
        {
            name = _name;
            title = _title;
            version = _version;
            author = _author;
            description = _description;
            weight = _weight;
            gameVersion = _gameVersion;
            downloadLink = _downloadLink;
            category = _category;
            platform = _platform;
            dependsOn = _dependsOn;
            conflictsWith = _conflictsWith;
        }
    }
}
