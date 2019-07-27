using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApp5
{
    public class MainWindowViewModel
    {
        public ICommand  MyCommand { get; set; }

        // NEED - Prism.wpf  NUGET
        public DelegateCommand MyCommand2 { get; set; } // to change the can execute in real time

        public MainWindowViewModel()
        {
            MyCommand = new Command(ExecuteMethod, CanExecuteMethod);

            MyCommand2 = new DelegateCommand(ExecuteMethod2, CanExecuteMethod2);

            Task.Run(() =>
            {
                while (true)
                {
                    //CommandManager.InvalidateRequerySuggested();
                    MyCommand2.RaiseCanExecuteChanged();
                    Thread.Sleep(500);
                }
            });
        }

        private void ExecuteMethod(object parameter)
        {
            MessageBox.Show("no code behind!");
        }

        private bool CanExecuteMethod()
        {
            return true;
        }

        // ==================================  DelegateCommand  ======================================================

        private bool CanExecuteMethod2()
        {
            return DateTime.Now.Second % 2 == 0;
        }


        private void ExecuteMethod2()
        {
            MessageBox.Show("no code behind!");
        }

    }
}
