

using System;

using System.Collections.ObjectModel;

using System.Windows;

using wpf_plugin_mvvm_efcore.Tools;

namespace Testing
{
    class MainViewModel
    {
        private CommandBase _item;
        public CommandBase ItemSelectedCommand => _item ??= new()
        {
            execute = delegate ()
            {
                MessageBox.Show("HIHIH");
            },
            canExecute = () => true
        };

       
        
        public ObservableCollection<string> Cities { get; private set; }

      
        public MainViewModel()       {
            
            
            Cities = new ObservableCollection<string>() { "Bangalore", "New York", "Sydney", "London", "Washington" };
        }

       
        
    }
}
