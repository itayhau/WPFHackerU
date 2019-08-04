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

namespace WPF0408
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<User> users = new List<User>();

        public MainWindow()
        {
            InitializeComponent();

            users.Add(new User() { Id = 1, Name = "John Doe", Birthday = new DateTime(1971, 7, 23), Smoker = true , Display = MyEnum .Percent});
            users.Add(new User() { Id = 2, Name = "Jane Doe", Birthday = new DateTime(1974, 1, 17), Smoker = false, Display = MyEnum.Value });
            users.Add(new User() { Id = 3, Name = "Sammy Doe", Birthday = new DateTime(1991, 9, 2), Smoker = true, Display = MyEnum.Percent });

            dgUsers.ItemsSource = users;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine();
        }

    
    }

    public enum MyEnum { Percent = 1, Value = 2 }

    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Smoker { get; set; }

        public DateTime Birthday { get; set; }

        public MyEnum Display { get; set; }
    }

    
}
