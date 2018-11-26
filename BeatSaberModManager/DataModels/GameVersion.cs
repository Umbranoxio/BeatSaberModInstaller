using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatSaberModManager.DataModels
{
    public struct GameVersion
    {
        public string id;
        public string value;
        public string manifest;

        public GameVersion(string _id, string _value, string _manifest)
        {
            id = _id;
            value = _value;
            manifest = _manifest;
        }
    }
}
