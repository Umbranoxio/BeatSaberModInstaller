using System.IO;
using System.Windows.Forms;
using BeatSaberModManager.Dependencies;
namespace BeatSaberModManager.Core
{
    public class PathLogic
    {
        public string installPath;
        public Platform platform;
         
   
        public string GetInstallationPath()
        {
            string steam = GetSteamLocation();
            string oculus = GetOculusHomeLocation();
            if (steam != null)
            {
                if (Directory.Exists(steam))
                {
                    if (File.Exists(steam + @"\Beat Saber.exe"))
                    {
                        platform = Platform.Steam;
                        installPath = steam;
                        return steam;
                    }
                }
            }
            if (oculus != null)
            {
                if (Directory.Exists(oculus))
                {
                    if (File.Exists(oculus + @"\Beat Saber.exe"))
                    {
                        platform = Platform.Oculus;
                        installPath = oculus;
                        return oculus;
                    }
                }
            }
            string fallback = GetFallbackDirectory();
            installPath = fallback;
            return fallback;
        }
        private string GetFallbackDirectory()
        {
            MessageBox.Show("We couldn't seem to find your beatsaber installation, please press \"OK\" and point us to it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return NotFoundHandler();
        }
        private string GetSteamLocation()
        {
            string path = RegistryWOW6432.GetRegKey64(RegHive.HKEY_LOCAL_MACHINE, 
            @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 620980", @"InstallLocation");
            if (path != null)
            {
                path = path + @"\";
            }
            return path;
        }
        private string GetOculusHomeLocation()
        {
            string path = RegistryWOW6432.GetRegKey32(RegHive.HKEY_LOCAL_MACHINE, 
            @"SOFTWARE\Oculus VR, LLC\Oculus\Config", @"InitialAppLibrary");
            if (path != null)
            {
                path = path + @"Software\hyperbolic-magnetism-beat-saber";
            }
            return path;
        }
        private string NotFoundHandler()
        {
            bool found = false;
            while (found == false)
            {
                using (var folderDialog = new FolderBrowserDialog())
                {
                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        string path = folderDialog.SelectedPath;
                        if (File.Exists(path + @"\Beat Saber.exe"))
                        {
                            FormPlatformSelect selector = new FormPlatformSelect(this);
                            selector.ShowDialog();
                            found = true;
                            return path;
                        }
                        else
                        {
                            MessageBox.Show("The directory you selected doesn't contain Beat Saber.exe! please try again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            return string.Empty;
        }
        public string ManualFind()
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = folderDialog.SelectedPath;
                    if (File.Exists(path + @"\Beat Saber.exe"))
                    {
                        installPath = path;
                        return folderDialog.SelectedPath;
                    }
                    else
                    {
                        MessageBox.Show("The directory you selected doesn't contain Beat Saber.exe! please try again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            return string.Empty;
        }
    }
}
