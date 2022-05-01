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
            IMediator mediator,
            ISession session)
        {
            _session = session;

            LoginScreenViewModel = loginScreenViewModel;
            CreateUserDetailViewModel = createUserDetailViewModel;
            ProfileUserDetailViewModel = profileUserDetailViewModel;
            CarEditViewModel = carEditViewModel;

            CurrentViewModel = LoginScreenViewModel;

            mediator.Register<DisplayUserCreateScreenMessage>(OnDisplayUserCreateScreen);
            mediator.Register<DisplayLoginScreenMessage>(OnDisplayLoginScreen);
            mediator.Register<DisplayUserProfileMessage>(OnDisplayUserProfile);
            mediator.Register<DisplayCarInfoMessage>(OnDisplayCarInfo);
            mediator.Register<DisplayLastMessage>(OnDisplayLast);
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

    }

}

