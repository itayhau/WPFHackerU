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

namespace Bubble
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hi from the outter button!");
        }

        private void OuterElippse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Title = "Outer ellipsed has changed the title!";
        }

        private void InnerButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("I am the inner button!"); // tunnel this ! yeah!!
        }

        private void Button_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("Mouse move by the outter button"); 
            //this.txt1.Text = Mouse.GetPosition(Application.Current.MainWindow).X.ToString();
            this.Title = "X = " + Mouse.GetPosition(Application.Current.MainWindow).X.ToString();
        }

        private void InnerElippse_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            this.Title = "Bubble = " + Mouse.GetPosition(Application.Current.MainWindow).X.ToString();
        }
    }
}
