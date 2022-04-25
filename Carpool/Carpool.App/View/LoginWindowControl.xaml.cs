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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Carpool.App.View
{
    /// <summary>
    /// Interaction logic for LoginWindowControl.xaml
    /// </summary>
    public partial class LoginWindowControl : UserControl
    {
        public LoginWindowControl()
        {
            InitializeComponent();
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            RideList objSecondWindow = new RideList();
            this.Visibility = Visibility.Hidden;
            objSecondWindow.Show();
        }
    }
}
