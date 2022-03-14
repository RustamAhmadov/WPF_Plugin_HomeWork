using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace wpf_plugin_mvvm_efcore.Tools
{
    public class CommandBase : ICommand
    {
        public Action execute;
        public Func<bool> canExecute;
        public event EventHandler CanExecuteChanged;

        public CommandBase(Action execute, Func<bool> canExecute)
        {
            
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public CommandBase()
        {

        }
        public bool CanExecute(object parameter)
        {
            return canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            execute.Invoke();
        }
    }
}
