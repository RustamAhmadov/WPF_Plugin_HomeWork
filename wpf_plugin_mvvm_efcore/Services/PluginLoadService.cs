using Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace wpf_plugin_mvvm_efcore.Services
{

    public class PluginLoadService : IPluginLoadService
    {
        public Dictionary<string, IUserService> plugins { get; set; } =  new();

        public void LoadPlugin()
        {
            string currentProjectPath = Path.GetDirectoryName(
                                Path.GetDirectoryName(
                                     Path.GetDirectoryName(Environment.CurrentDirectory)));
            string root = Path.GetDirectoryName(currentProjectPath);


            string pluginLocation = currentProjectPath + "\\Plugins";

            foreach (var dll in Directory.GetFiles(pluginLocation, "*.dll"))
            {
                AssemblyLoadContext assemblyLoad = new(dll);
                Assembly assembly = assemblyLoad.LoadFromAssemblyPath(dll);

                try
                {
                    foreach (Type type in assembly.GetTypes())
                    {
                        if (typeof(IUserService).IsAssignableFrom(type))
                        {
                            IUserService result = Activator.CreateInstance(type) as IUserService;
                            if (result != null)
                            {
                                plugins.Add(result.InheritorPluginName, result);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
                
            }
        
        }
    }
}
