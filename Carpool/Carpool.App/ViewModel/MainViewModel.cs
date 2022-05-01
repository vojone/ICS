using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

        private readonly ISession _session;

        public MainViewModel(
            ILoginScreenViewModel loginScreenViewModel,
            ICreateUserDetailViewModel createUserDetailViewModel,
            IProfileUserDetailViewModel profileUserDetailViewModel,
            ICarEditViewModel carEditViewModel,
            IRideListViewModel rideListViewModel,
            IRideHistoryViewModel rideHistoryViewModel,
            ICreateRideDetailViewModel createRideDetailViewModel,
            IEditRideDetailViewModel editRideDetailViewModel,
            IBookRideDetailViewModel bookRideDetailViewModel,
            ISession session,
            IMediator mediator)
        {
            _session = session;

            LoginScreenViewModel = loginScreenViewModel;
            RideListViewModel = rideListViewModel;
            RideHistoryViewModel = rideHistoryViewModel;
            CreateRideDetailViewModel = createRideDetailViewModel;
            BookRideDetailViewModel = bookRideDetailViewModel;
            EditRideDetailViewModel = editRideDetailViewModel;
            CreateUserDetailViewModel = createUserDetailViewModel;
            ProfileUserDetailViewModel = profileUserDetailViewModel;
            CarEditViewModel = carEditViewModel;

            CurrentViewModel = LoginScreenViewModel;

            mediator.Register<DisplayUserCreateScreenMessage>(OnDisplayUserCreateScreen);
            mediator.Register<DisplayLoginScreenMessage>(OnDisplayLoginScreen);
            mediator.Register<DisplayUserProfileMessage>(OnDisplayUserProfile);

            mediator.Register<DisplayCarInfoMessage>(OnDisplayCarInfo);
            mediator.Register<DisplayLastMessage>(OnDisplayLast);

            mediator.Register<DisplayRideListMessage>(OnDisplayRideList);
            mediator.Register<DisplayRideHistoryMessage>(OnDisplayRideHistory);
            mediator.Register<DisplayCreateRideMessage>(OnDisplayCreateRide);
            mediator.Register<DisplayBookRideMessage>(OnDisplayBookRide);
            mediator.Register<DisplayEditRideMessage>(OnDisplayEditRide);
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

        public ICreateUserDetailViewModel CreateUserDetailViewModel { get; set; }

        public IProfileUserDetailViewModel ProfileUserDetailViewModel { get; set; }

        public ICarEditViewModel CarEditViewModel { get; set; }


        public void OnDisplay(IViewModel viewModel)
        {
            if (CurrentViewModel != null)
            {
                _session.PushViewModel(CurrentViewModel);
            }

            CurrentViewModel = viewModel;
        }


        public IRideListViewModel RideListViewModel{ get; set; }

        public IRideHistoryViewModel RideHistoryViewModel { get; set; }

        public ICreateRideDetailViewModel CreateRideDetailViewModel { get; set; }

        public IEditRideDetailViewModel EditRideDetailViewModel { get; set; }

        public IBookRideDetailViewModel BookRideDetailViewModel { get; set; }

        public void OnDisplayLast(DisplayLastMessage msg)
        {
            Debug.WriteLine("Received go back message!");
            CurrentViewModel = _session.GetLastViewModel() ?? CurrentViewModel;
        }


        public void OnDisplayCarInfo(DisplayCarInfoMessage msg)
        {
            OnDisplay(CarEditViewModel);
        }

        public void OnDisplayUserProfile(DisplayUserProfileMessage msg)
        {
            OnDisplay(ProfileUserDetailViewModel);
        }

        public void OnDisplayUserCreateScreen(DisplayUserCreateScreenMessage msg)
        {
            OnDisplay(CreateUserDetailViewModel);
        }

        public void OnDisplayLoginScreen(DisplayLoginScreenMessage msg)
        {
            OnDisplay(LoginScreenViewModel);
        }

        public void OnDisplayRideList(DisplayRideListMessage msg)
        {
            CurrentViewModel = RideListViewModel;
        }

        public void OnDisplayRideHistory(DisplayRideHistoryMessage msg)
        {
            CurrentViewModel = RideHistoryViewModel;
        }

        public void OnDisplayCreateRide(DisplayCreateRideMessage msg)
        {
            CurrentViewModel = CreateRideDetailViewModel;
        }

        public void OnDisplayBookRide(DisplayBookRideMessage msg)
        {
            CurrentViewModel = BookRideDetailViewModel;
        }

        public void OnDisplayEditRide(DisplayEditRideMessage msg)
        {
            CurrentViewModel = EditRideDetailViewModel;
        }

    }

}

