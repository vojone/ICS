using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.App.ViewModel;

namespace Carpool.App.Services
{
    public interface INavigator
    {
        public IViewModel CurrentViewModel { get; set; }
    }
}
