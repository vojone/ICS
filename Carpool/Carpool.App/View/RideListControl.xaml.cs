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

namespace Carpool.App.View
{
    /// <summary>
    /// Interaction logic for RideListControl.xaml
    /// </summary>
    public partial class RideListControl : UserControlBase
    {
        public RideListControl()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonCreateUser_Copy_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
