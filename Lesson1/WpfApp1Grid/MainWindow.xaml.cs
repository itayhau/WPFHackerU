using System;
using System.Collections.Generic;
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

namespace WpfApp1Grid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public List<Person> people = new List<Person>();

        public MainWindow()
        {
            InitializeComponent();

            people.Add(new Person { FirstName = "Dana", LastName = "Koren"});
            people.Add(new Person { FirstName = "Keren", LastName = "Koko" });
            people.Add(new Person { FirstName = "Mor", LastName = "Mini" });

            myComboBox.ItemsSource = people;
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            // the method contain the button name!!!
            MessageBox.Show($"Hello {firstNameText.Text}");

            // Not we can delete the XAML click event and this method and it will run
            // If you try to restore it in the XAML you may get a bug in the designer and have to restart the visual studio
            // when you click in XAML Click="" > Ctrl+Space you can specify the event method
            // not like WinForm wheere we could remove just the code and we had to remove event from the designer!
        }
    }
}
