using Base;
using GalaSoft.MvvmLight;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using wpf_plugin_mvvm_efcore.Messages;
using wpf_plugin_mvvm_efcore.Tools;

namespace wpf_plugin_mvvm_efcore.ViewModel
{
    public class UserInfoViewModel : ViewModelBase
    {
        private readonly IMessenger _messanger;
        public User _user { get; set; } = new();

        private bool _newUser = true;

        #region CodeSmells

        //This is array for Enum , it s just for binding to combo-box
        public List<string> UserGender { get; private set; } = new(3) { "Male", "Female", "Others" };

        private string _currentGender = "Male";
        public string CurrentGender
        {
            get
            {
                if (_user != null)
                    _currentGender = _user.Gender.ToString();
                return _currentGender;
            }
            set
            {
                _currentGender = value;
                int _index = 0;
                foreach (string gender in UserGender)
                {
                    if (_currentGender == gender)
                        _user.Gender = (Gender)_index;

                    _index++;
                }
            }
        }
        #endregion


        private BitmapImage _currentImage;
        public BitmapImage CurrentImage
        {
            get
            {
                if (_user.PicturePath == null)
                    _currentImage = new(new(Path.GetFullPath("defImage.png")));
                else
                    _currentImage = new(new(_user.PicturePath));

                return _currentImage;
            }
            set
            {
                Set(ref _currentImage, value);
            }
        }

        private CommandBase _save;
        public CommandBase Save
        {
            get
            {
                return _save ??= new()
                {
                    execute = () =>
                    {
                        NavigationMessage navigationMessage = App.Container.GetInstance<NavigationMessage>();
                        navigationMessage.NewCurrentViewModel = App.Container.GetInstance<HomeViewModel>();
                        _messanger.Send<NavigationMessage>(navigationMessage);

                        if (_newUser)
                        {
                            UserMessage userMessage = new();
                            userMessage.User = _user;
                            _messanger.Send<UserMessage>(userMessage, ViewModelTokens.Token.Home);
                        }
                        _newUser = true;
                        _user = new User();
                    },
                    canExecute = () => true
                };
            }
        }


        private CommandBase _delete;
        public CommandBase Delete
        {
            get
            {
                return _delete ??= new()
                {
                    execute = () =>
                    {                        

                        if (_newUser)
                        {
                            NavigationMessage navigationMessage = App.Container.GetInstance<NavigationMessage>();
                            navigationMessage.NewCurrentViewModel = App.Container.GetInstance<HomeViewModel>();
                            _messanger.Send<NavigationMessage>(navigationMessage);
                        }
                        else
                        {
                            UserDeleteMessage userMessage = new();                            
                            _messanger.Send<UserDeleteMessage>(userMessage, ViewModelTokens.Token.Home);                        

                        }
                        _newUser = true;
                        _user = new User();
                    },
                    canExecute = () => true
                };
            }
        }

        private CommandBase _browseCommand;
        public CommandBase BrowseCommand => _browseCommand ??= new()
        {
            execute = () =>
            {
                OpenFileDialog openFileDialog = new();
                openFileDialog.Title = "Select a picture";
                openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
               "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
               "Portable Network Graphic (*.png)|*.png";
                openFileDialog.ShowDialog();
                if (openFileDialog.FileName != null && openFileDialog.FileName != "")
                {
                    _user.PicturePath = openFileDialog.FileName;
                    CurrentImage = new(new Uri(openFileDialog.FileName));
                }
            },
            canExecute = () => true

        };
        public UserInfoViewModel(IMessenger M)
        {
            _messanger = M;

            _messanger.Subscribe<UserMessage>((UserMessage) =>
            {
                _newUser = false;
                _user = (UserMessage as UserMessage).User;
            }, ViewModelTokens.Token.UserInfo);
        }
    }
}
