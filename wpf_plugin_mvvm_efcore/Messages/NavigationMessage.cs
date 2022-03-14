using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf_plugin_mvvm_efcore.Tools;

namespace wpf_plugin_mvvm_efcore.Messages
{
    public class NavigationMessage:IMessage
    { 
        public ViewModelBase NewCurrentViewModel { get; set; }
    }
}
