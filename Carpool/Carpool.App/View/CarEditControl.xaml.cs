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
using Carpool.App.ViewModel;

namespace Carpool.App.View
{
    /// <summary>
    /// Interaction logic for CarInfoControl.xaml
    /// </summary>
    public partial class CarEditControl : UserControl
    {
        public CarEditControl(CarEditViewModel carEditViewModel)
        {
            InitializeComponent();
            DataContext = carEditViewModel;
        }

        private void SelectedCarCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ICommand command = ((CarEditViewModel) DataContext).SelectCarCommand;
            if (command.CanExecute(((ComboBox)sender).SelectedIndex))
            {
                command.Execute(((ComboBox)sender).SelectedIndex);
            }
        }
    }
}
