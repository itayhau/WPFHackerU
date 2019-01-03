using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using WPFUI.ViewModels;

namespace WPFUI
{
    /// <summary>
    /// Starting point of our application
    /// </summary>
    class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize(); // starts up some processes
        }

        // OnStartUp event
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>(); // displaying the root view of this model
        }
    }
}
