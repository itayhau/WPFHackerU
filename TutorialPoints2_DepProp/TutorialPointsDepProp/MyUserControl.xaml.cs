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

namespace TutorialPointsDepProp
{
    /// <summary>
    /// Interaction logic for MyUserControl.xaml
    /// </summary>
    public partial class MyUserControl : UserControl
    {
        public MyUserControl()
        {
            InitializeComponent();
        }

            public static readonly DependencyProperty SetTextProperty =
                DependencyProperty.Register("SetText", typeof(string), typeof(MyUserControl),
                    new PropertyMetadata("", new PropertyChangedCallback(OnSetTextChanged)));
            public string SetText
            {
                get { return (string)GetValue(SetTextProperty); }
                set { SetValue(SetTextProperty, value); }
            }
            private static void OnSetTextChanged(DependencyObject d,
                DependencyPropertyChangedEventArgs e)
            {
                MyUserControl UserControl1Control = d as MyUserControl;
                UserControl1Control.OnSetTextChanged(e);
            }
            private void OnSetTextChanged(DependencyPropertyChangedEventArgs e)
            {
                tbTest.Text = e.NewValue.ToString();
            }
        }
}
