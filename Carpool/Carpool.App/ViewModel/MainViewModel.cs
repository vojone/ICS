using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Markup;
using Carpool.App.Command;
using Carpool.App.Factories;
using Carpool.App.Messages;
using Carpool.App.Services;
using Carpool.App.View;
using Carpool.App.Wrapper;
using Carpool.BL.Facades;

namespace Carpool.App.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        private IViewModel? _currentViewModel;

        public MainViewModel(
            ILoginScreenViewModel loginScreenViewModel,
            IUserDetailViewModel userDetailViewModel,
            IRideListViewModel rideListViewModel,
            IRideDetailViewModel rideDetailViewModel,
            IMediator mediator)
        {
            LoginScreenViewModel = loginScreenViewModel;
            UserDetailViewModel = userDetailViewModel;
            RideListViewModel = rideListViewModel;
            RideDetailViewModel = rideDetailViewModel;

            CurrentViewModel = LoginScreenViewModel;

            mediator.Register<DisplayUserCreateScreenMessage>(OnDisplayUserCreateScreen);
            mediator.Register<DisplayLoginScreenMessage>(OnDisplayLoginScreen);
        }

        public IViewModel? CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public ILoginScreenViewModel LoginScreenViewModel { get; set; }

        public IUserDetailViewModel UserDetailViewModel { get; set; }

        public IRideListViewModel RideListViewModel{ get; set; }

        public IRideDetailViewModel RideDetailViewModel { get; set; }


        public void OnDisplayUserProfile(DisplayUserCreateScreenMessage msg)
        {

            CurrentViewModel = UserDetailViewModel;
        }


        public void OnDisplayUserCreateScreen(DisplayUserCreateScreenMessage msg)
        {
            
            CurrentViewModel = RideDetailViewModel;
        }

        public void OnDisplayLoginScreen(DisplayLoginScreenMessage msg)
        {

            CurrentViewModel = LoginScreenViewModel;
        }

    }

}

