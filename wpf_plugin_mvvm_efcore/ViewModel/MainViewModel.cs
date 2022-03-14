using Base;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using wpf_plugin_mvvm_efcore.Messages;
using wpf_plugin_mvvm_efcore.Tools;

namespace wpf_plugin_mvvm_efcore.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMessenger _messanger;                

        private ViewModelBase _currentView;
        public ViewModelBase CurrentView
        {
            get { return _currentView; }
            set 
            {
                Set(ref _currentView, value);
            }
        }

        

        public MainViewModel(IMessenger messenger) 
        {            
            _messanger = messenger;            

            _messanger.Subscribe<NavigationMessage>(NavigationMessage => {
                CurrentView = (NavigationMessage as NavigationMessage).NewCurrentViewModel;
            });
        }       
    }
}
