using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.App.Factories;
using Carpool.App.Messages;
using Carpool.App.Services;

namespace Carpool.App.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(
            ILoginScreenViewModel loginScreenViewModel,
            IMediator mediator)
        {
            LoginScreenViewModel = loginScreenViewModel;
        }

        public ILoginScreenViewModel LoginScreenViewModel { get; }


    }
    
}

