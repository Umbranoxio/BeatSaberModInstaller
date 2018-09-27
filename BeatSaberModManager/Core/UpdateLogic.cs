using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatSaberModManager.Core
{
    class UpdateLogic
    {
        static readonly string repo = "Umbranoxio/BeatSaberModInstaller";
        static readonly string releaseURL = $"https://api.github.com/repos/{repo}/releases/latest";
    }
}
