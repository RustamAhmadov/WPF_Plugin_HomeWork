using GalaSoft.MvvmLight;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using wpf_plugin_mvvm_efcore.Messages;
using wpf_plugin_mvvm_efcore.Services;
using wpf_plugin_mvvm_efcore.Tools;
using wpf_plugin_mvvm_efcore.View;
using wpf_plugin_mvvm_efcore.ViewModel;

namespace wpf_plugin_mvvm_efcore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly Container Container =new();
        
        protected override void OnStartup(StartupEventArgs e)
        {
            SetServices();
            SetStartWindow<HomeViewModel>();

            base.OnStartup(e);  
        }
        private void SetServices()
        {
            //ViewModels
            Container.RegisterSingleton<MainViewModel>();
            Container.RegisterSingleton<HomeViewModel>();
            Container.RegisterSingleton<UserInfoViewModel>();
            //Tools            
            Container.RegisterSingleton<IMessenger, Messenger>();

            //Messages
            Container.RegisterSingleton<NavigationMessage>();

            //Services
            Container.RegisterSingleton<Base.IUserService, UserService>();
            Container.RegisterSingleton<IPluginLoadService, PluginLoadService>();



            Container.Verify();
        }

        private void SetStartWindow<T>() where T : ViewModelBase
        {
            MainViewModel mainViewModel = Container.GetInstance<MainViewModel>();

            //ActivatorCreateInstance<T>();
            mainViewModel.CurrentView = Container.GetInstance<HomeViewModel>(); ;

            MainView mainView = new();
            mainView.DataContext = mainViewModel;
            
            mainView.Show();
        }
    }
}
