using Base;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using wpf_plugin_mvvm_efcore.Tools;
using wpf_plugin_mvvm_efcore.Services;
using wpf_plugin_mvvm_efcore.Messages;

namespace wpf_plugin_mvvm_efcore.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;

                Users.Clear();
                if (_searchText.Length != 0)
                {
                    bool check;
                    for (int i = 0; i < UsersPrivate.Count; i++)
                    {
                        check = true;
                        for (int j = 0; j < _searchText.Length; j++)
                        {                            
                            if (UsersPrivate[i].FirstName.ToLower()[j] != _searchText.ToLower()[j])
                            {
                                check = false;
                                break;
                            }                             
                        }
                        if (check == true)
                            Users.Add(UsersPrivate[i]);
                    }
                }
                else
                    for (int i = 0; i < UsersPrivate.Count; i++)
                    {
                        Users.Add(UsersPrivate[i]);
                    }
            }
        }
       

        private readonly IPluginLoadService _pluginLoadService;

        private readonly IMessenger _messanger;
        private ObservableCollection<User> UsersPrivate { get; set; } = new();
        public ObservableCollection<User> Users { get; set; } = new();
        public User SelectedUser { get; set; }

        private Dictionary<string, IUserService> _services;

        #region Commands
        private CommandBase _add;
        public CommandBase Add => _add ??= new CommandBase()
        {
            execute = delegate ()
            {
                NavigationMessage navigationMessage = App.Container.GetInstance<NavigationMessage>();
                navigationMessage.NewCurrentViewModel = App.Container.GetInstance<UserInfoViewModel>();
                _messanger.Send<NavigationMessage>(navigationMessage);
            },
            canExecute = delegate () { return true; }
        };

        private CommandBase _userSelected;
        public CommandBase UserSelected => _userSelected ??= new()
        {
            execute = () =>
            {
                NavigationMessage navigationMessage = App.Container.GetInstance<NavigationMessage>();
                navigationMessage.NewCurrentViewModel = App.Container.GetInstance<UserInfoViewModel>();
                _messanger.Send<NavigationMessage>(navigationMessage);


                UserMessage userMessage = new();
                userMessage.User = SelectedUser;
                _messanger.Send<UserMessage>(userMessage, ViewModelTokens.Token.UserInfo);
            },
            canExecute = () => true
        };

        private CommandBase _saveChanges;
        public CommandBase SaveChanges => _saveChanges ??= new()
        {
            execute = () =>
            {
                foreach (var service in _services)
                {
                    service.Value.SaveChangesAsync();
                }
            },
            canExecute = () => true
        };
        #endregion

        public HomeViewModel(IPluginLoadService PLS, IMessenger M, IUserService userService)
        {
            _messanger = M;

            _pluginLoadService = PLS;
            _pluginLoadService?.LoadPlugin();

            if (_pluginLoadService.plugins.Count > 0)
            {
                _services = _pluginLoadService.plugins;
                //Just for example
                if (_services.ContainsKey("EFCorePluginName"))
                {
                    UsersPrivate = new(_services["EFCorePluginName"].GetUsers());
                    foreach (var item in UsersPrivate)
                    {
                        Users.Add(item);
                    }
                }

            }

            _services.Add(userService.InheritorPluginName, userService);

            _messanger.Subscribe<UserMessage>((x) =>
            {
                User u = (x as UserMessage).User;
                Users.Add(u);
                UsersPrivate.Add(u);
                foreach (var service in _services)
                {
                    service.Value.Add(u);
                }
            }, ViewModelTokens.Token.Home);

            //Just for test
            _messanger.Subscribe<UserDeleteMessage>((x) =>
            {
                UsersPrivate.Remove(SelectedUser);
                Users.Remove(SelectedUser);
                foreach (var service in _services)
                {
                    service.Value.Remove(SelectedUser);
                }
            }, ViewModelTokens.Token.Home);
        }
    }
}
