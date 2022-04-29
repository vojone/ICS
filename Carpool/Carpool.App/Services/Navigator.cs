using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.App.ViewModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Carpool.App.Services
{
    public class Navigator : ObservableObject, INavigator
    {
        private IViewModel _currentViewModel;
        public IViewModel CurrentViewModel { get; set; }
    }
}
