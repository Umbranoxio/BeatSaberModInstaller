using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;
using BeatSaberModManager.DataModels;
using BeatSaberModManager.Dependencies.SimpleJSON;
namespace BeatSaberModManager.Core
{
    public class RemoteLogic
    {
        private const string EndPoint = "https://www.modsaber.ml/";
        private const Int16 CurrentVersion = 9;
        public List<ReleaseInfo> releases;
        public RemoteLogic()
        {
            releases = new List<ReleaseInfo>();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }
        public void PopulateReleases()
        {
            string raw = CallApiFunction("api/public/temp/approved");
            if (raw != null)
            {
                var decoded = JSON.Parse(raw);
                var mods = decoded["mods"];
                for (int i = 0; i < mods.Count; i++)
                {
                    var current = mods[i];
                    var files = current["files"];
                    if (files.Count > 1)
                    {
                        var steam = files[0];
                        var oculus = files[1];
                        CreateRelease(new ReleaseInfo(current["title"], current["version"], current["author"],
                        current["description"], current["gameVersion"], steam["url"], Platform.Steam));
                        CreateRelease(new ReleaseInfo(current["title"], current["version"], current["author"],
                        current["description"], current["gameVersion"], oculus["url"], Platform.Oculus));
                    }
                    else
                    {
                        CreateRelease(new ReleaseInfo(current["title"], current["version"], current["author"],
                        current["description"], current["gameVersion"], files["steam"]["url"], Platform.Default));
                    }
                }
            }
        }
        private string CallApiFunction(string function)
        {
            return Helper.Get(string.Format("{0}{1}", EndPoint, function));
        }
        private void CreateRelease(ReleaseInfo release)
        {
            releases.Add(release);
        }
        public void CheckVersion()
        {
            //TODO: Don't be lazy and actually make an auto updater
            Int16 version = Convert.ToInt16(Helper.Get(
                "https://raw.githubusercontent.com/Umbranoxio/BeatSaberModInstaller/master/update.txt"));
            if (version > CurrentVersion)
            {
                MessageBox.Show("Your version of the mod installer is outdated! Please download the new one!", "Update available!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Process.Start("https://github.com/Umbranoxio/BeatSaberModInstaller/releases");
                Process.GetCurrentProcess().Kill();
                Environment.Exit(0);
            }
        }
    }
}
