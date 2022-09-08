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
using System.Windows.Shapes;
using Carpool.App.ViewModel;

namespace Carpool.App.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel mainViewModel)
        {
            //Suppresses harmless errors in views, there is no simple way to solve them - due to https://weblogs.asp.net/akjoshi/resolving-un-harmful-binding-errors-in-wpf
            #if DEBUG
                System.Diagnostics.PresentationTraceSources.DataBindingSource.Switch.Level = System.Diagnostics.SourceLevels.Critical;
            #endif

            InitializeComponent();
            DataContext = mainViewModel;
        }

        private void LoginWindowControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
