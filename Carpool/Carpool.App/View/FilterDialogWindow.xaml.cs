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
using Microsoft.Win32;

namespace Carpool.App.View
{
    /// <summary>
    /// Interaction logic for BookRideControl.xaml
    /// </summary>
    public partial class FilterDialogWindow : Window
    {


        public FilterDialogWindow()
        {
            InitializeComponent();
            DepartureTime.Value = DateTime.Now;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public DateTime? GetArrivalTime()
        {
            if (ArrTimeChecked.IsChecked == null)
            {
                return null;
            }

            return (bool)ArrTimeChecked.IsChecked ? ArrivalTime.Value : null;
        }

        public DateTime? GetDepartureDateTime()
        {
            if (DepartureTimeChecked.IsChecked == null)
            {
                return null;
            }

            return (bool)DepartureTimeChecked.IsChecked ? DepartureTime.Value : null;
        }

        public string? GetArrivalLocation()
        {
            if (ArrPlaceChecked.IsChecked == null)
            {
                return null;
            }

            return (bool)ArrPlaceChecked.IsChecked ? ArrPlace.Text : null;
        }

        public string? GetDepartureLocation()
        {
            if (DepPlaceChecked.IsChecked == null)
            {
                return null;
            }

            return (bool)DepPlaceChecked.IsChecked ? DepPlace.Text : null;
        }


        public bool GetAvailabilityFlag()
        {
            if (AvailabilityFlag.IsChecked == null)
            {
                return false;
            }

            return (bool)AvailabilityFlag.IsChecked;
        }
    }
}
