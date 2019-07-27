using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp5
{
    public class Command : ICommand
    {
        // create also:  class ActionCommand<T> : ICommand

        Action<object> executeMethod;

        Func<bool> canExecuteMethod;

        public Command(Action<object> executeMethod,  Func<bool> canExecuteMethod)
        {
            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            // return canExecuteMethod(); 

            // will be calculated only once
            
            return true;
        }

        public void Execute(object parameter)
        {
            executeMethod(parameter);
        }

    }
}
