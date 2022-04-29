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
            ICreateUserDetailViewModel createUserDetailViewModel,
            IProfileUserDetailViewModel profileUserDetailViewModel,
            IMediator mediator)
        {
            LoginScreenViewModel = loginScreenViewModel;
            CreateUserDetailViewModel = createUserDetailViewModel;
            ProfileUserDetailViewModel = profileUserDetailViewModel;

            CurrentViewModel = LoginScreenViewModel;

            mediator.Register<DisplayUserCreateScreenMessage>(OnDisplayUserCreateScreen);
            mediator.Register<DisplayLoginScreenMessage>(OnDisplayLoginScreen);
            mediator.Register<DisplayUserProfileMessage>(OnDisplayUserProfile);
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



        public void OnDisplayUserProfile(DisplayUserProfileMessage msg)
        {
            CurrentViewModel = ProfileUserDetailViewModel;
        }

        public void OnDisplayUserCreateScreen(DisplayUserCreateScreenMessage msg)
        {
            
            CurrentViewModel = CreateUserDetailViewModel;
        }

        public void OnDisplayLoginScreen(DisplayLoginScreenMessage msg)
        {

            CurrentViewModel = LoginScreenViewModel;
        }

    }

}

