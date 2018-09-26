using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatSaberModManager.DataModels
{
    public struct ModLink
    {
        public string name;
        public string semver;

        public ModLink(string _name, string _semver)
        {
            name = _name;
            semver = _semver;
        }
    }
}
