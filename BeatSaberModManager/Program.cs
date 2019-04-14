using System;
using System.Windows.Forms;
using System.Reflection;

namespace BeatSaberModManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool close = false;
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                case "--install-song":
                    Console.WriteLine("Installing: {0}", args[i + 1]);
                    Core.OneClickInstaller.InstallSong(args[i + 1]);
                    close = true;
                    break;

                default:
                    break;
                }
            }

            if (close)
                return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

    }
}
