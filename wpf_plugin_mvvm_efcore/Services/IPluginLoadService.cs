using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_plugin_mvvm_efcore.Services
{
    public interface IPluginLoadService
    {
        public Dictionary<string, IUserService> plugins { get; set; }
        public void LoadPlugin();
    }
}
