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
using Carpool.BL.Facades;

namespace Carpool.App.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(UserFacade userFacade, IMediator mediator)
        {
            LoginScreenViewModel = new LoginScreenViewModel(userFacade, mediator);
            UserDetailViewModel = new UserDetailViewModel(userFacade, mediator);

            CurrentViewModel = LoginScreenViewModel;

            Mediator = mediator;

            Mediator.Register<DisplayUserCreateSreenMessage>(GoToUserCreate);
        }

        public IMediator Mediator { get; set; }

        public ViewModelBase CurrentViewModel { get; set; }

        public LoginScreenViewModel LoginScreenViewModel { get; set; }

        public UserDetailViewModel UserDetailViewModel { get; set; }

        public void GoToUserCreate(DisplayUserCreateSreenMessage msg)
        {
            System.Diagnostics.Debug.WriteLine("Clicked");
            CurrentViewModel = UserDetailViewModel;
            OnPropertyChanged();
        }

        public ICommand DisplayCreateUserView { get; set; }
    }

}

