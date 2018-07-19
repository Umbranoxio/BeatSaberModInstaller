using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeatSaberModInstaller.Internals
{
    public class ReleaseInfo
    {
        public string Version;
        public string Link;
        public string Name;
        public string Author;
        public string GitPath;
        public string Tag;
        public int ReleaseId;
        public bool Install;
        public ReleaseInfo(string _version, string _link ,string _name, bool _install, string _author, string _gitPath, int _releaseId, string _tag)
        {
            Version = _version;
            Link = _link;
            Name = _name;
            Install = _install;
            Author = _author;
            GitPath = _gitPath;
            ReleaseId = _releaseId;
            Tag = _tag;
        }
    }
}
