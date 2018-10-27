using BeatSaberModManager.Dependencies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BeatSaberModManager.Core
{
    public class InstalledPluginsLogic
    {
        private static readonly List<string> BeatSaberDirs = new List<string>() { @"Beat Saber_Data\Managed", @"IPA\Data\Managed" };
        private const string PluginDir = "Plugins";

        public Dictionary<string, string> Plugins { get; set; }

        private Assembly LoadBeatSaberAssemblies(string installPath)
        {
            Assembly illusionPlugin = null;

            foreach (var dir in BeatSaberDirs)
            {
                var path = Path.Combine(installPath, dir);
                if (!Directory.Exists(path))
                    continue;
                var dlls = Directory.GetFiles(path, "*.dll");
                foreach (var dll in dlls)
                {
                    try
                    {
                        var assembly = Assembly.LoadFile(dll);
                        if (assembly.GetName().Name == "IllusionPlugin")
                        {
                            illusionPlugin = assembly;
                        }
                    }
                    catch { } // Skip unloadable DLLs
                }
            }
            return illusionPlugin;
        }

        /// <summary>
        /// Loads the currently installed Plugins 
        /// </summary>
        /// <param name="installPath">BeatSaber installation path</param>
        /// <returns>Installed Plugins as (name, version)Dictionary</returns>
        public Dictionary<string, string> LoadInstalledPlugins(string installPath)
        {
            Plugins = new Dictionary<string, string>();
            AssemblyLocator.Init();
            Assembly illusionPlugin = LoadBeatSaberAssemblies(installPath);

            if (illusionPlugin == null) return Plugins;

            // IPlugin interface is implemented by Mods
            Type iPlugin = illusionPlugin.GetType("IllusionPlugin.IPlugin");

            string pluginPath = Path.Combine(installPath, PluginDir);
            if (!Directory.Exists(pluginPath)) return Plugins;

            var pluginFiles = Directory.GetFiles(pluginPath, "*.dll");
            foreach (var pluginFile in pluginFiles)
            {
                try
                {
                    var assembly = Assembly.LoadFile(pluginFile);
                    foreach (var type in assembly.GetExportedTypes())
                    {
                        if (!type.GetInterfaces().Contains(iPlugin))
                            continue;
                        object pluginInstance = Activator.CreateInstance(type);
                        
                        string name = null;
                        string version = null;

                        // We can not access these properties directly because they could be private
                        foreach (var property in type.GetRuntimeProperties())
                        {
                            if (property.Name == "Name" || property.Name == "IPlugin.Name" || property.Name == "IllusionPlugin.IPlugin.Name")
                                name = (string)property.GetValue(pluginInstance);
                            else if (property.Name == "Version" || property.Name == "IPlugin.Version" || property.Name == "IllusionPlugin.IPlugin.Version")
                                version = (string)property.GetValue(pluginInstance);
                        }
                        AddPlugin(name, version);
                    }
                }
                catch { } // Skip unloadable DLLs

            }
            return Plugins;
        }

        /// <summary>
        /// This method tries to correct slight differences between plugin names.
        /// The Plugin name on modsaber isnt always identical to the name inside the DLL.
        /// </summary>
        /// <param name="name">plugin name</param>
        /// <returns>corrected name</returns>
        private string GeneralizeName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("Plugin", "");
            return name;
        }

        private void AddPlugin(string name, string version)
        {
            Plugins.Add(GeneralizeName(name), version);
        }

        public string GetInstalledVersion(string name)
        {
            name = GeneralizeName(name);
            if (Plugins == null)
                return null;
            if (!Plugins.ContainsKey(name))
                return null;
            return Plugins[name];
        }
    }
}
