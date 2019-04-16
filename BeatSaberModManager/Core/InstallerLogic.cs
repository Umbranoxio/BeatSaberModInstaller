using BeatSaberModManager.DataModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace BeatSaberModManager.Core
{
    public class InstallerLogic
    {

        #region Events
        public event StatusUpdateHandler StatusUpdate;
        public delegate void StatusUpdateHandler(string status);
        #endregion

        #region Variables
        List<ReleaseInfo> releases;
        public string installDirectory;
        #endregion

        #region Constructor
        public InstallerLogic(List<ReleaseInfo> _releases, string _installDirectory)
        {
            releases = _releases;
            installDirectory = _installDirectory;
        }
        #endregion

        #region Logic
        public void Run()
        {
            try
            {
                StatusUpdate("Starting install sequence...");

                ReleaseInfo bsipa = null;
                if ((bsipa = releases.Find(release => release.name.ToLower() == "bsipa")) != null)
                {
                    StatusUpdate($"Downloading...{bsipa.title}");
                    byte[] file = Helper.GetFile(bsipa.downloadLink);
                    StatusUpdate($"Unzipping...{bsipa.title}");
                    Helper.UnzipFile(file, installDirectory);
                    StatusUpdate($"Unzipped! {bsipa.title}");

                    releases.Remove(bsipa); // don't want to double down
                }

                var toInstall = bsipa == null ? installDirectory : Path.Combine(installDirectory, "IPA", "Pending");
                Directory.CreateDirectory(toInstall);

                foreach (ReleaseInfo release in releases)
                {
                    if (release.install)
                    {
                        StatusUpdate($"Downloading...{release.title}");
                        byte[] file = Helper.GetFile(release.downloadLink);
                        StatusUpdate($"Unzipping...{release.title}");
                        Helper.UnzipFile(file, toInstall);
                        StatusUpdate($"Unzipped! {release.title}");
                    }
                }

                // Only ever going to be downloading BSIPA
                Process.Start(new ProcessStartInfo
                {
                    FileName = Path.Combine(installDirectory, "IPA.exe"),
                    WorkingDirectory = installDirectory,
                    Arguments = "-n"
                }).WaitForExit();
                
                StatusUpdate("Install complete!");
            } catch (Exception ex)
            {
                StatusUpdate("Install failed! " + ex.ToString());
                Console.WriteLine("Install failed! " + ex.ToString());
            }
            
        }
        #endregion

        #region Helpers
        public string Quoted(string str)
        {
            return "\"" + str + "\"";
        }
        #endregion

    }
}
