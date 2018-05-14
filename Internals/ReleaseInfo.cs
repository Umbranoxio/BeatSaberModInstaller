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
        public bool Install;
        public ReleaseInfo(string _version, string _link ,string _name, bool _install)
        {
            Version = _version;
            Link = _link;
            Name = _name;
            Install = _install;
            
        }
    }
}
