using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _2207
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Person MyPerson1 { get; set; }

        public MainWindow()
        {
            
            InitializeComponent();

            MyPerson1 = new Person { Name = "Very special person!" };

            this.DataContext = this;
        }

        public class Person : INotifyPropertyChanged
        {
            private string name;
            public string Name {
                get
                {
                    return this.name;
                }
                set
                {
                    this.name = value;
                    OnPropertyChanged("Name");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            public override string ToString()
            {
                return $"Person name {Name}";
            }

            private void OnPropertyChanged(string property)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(property));
                }
            }
        }
    }
}
