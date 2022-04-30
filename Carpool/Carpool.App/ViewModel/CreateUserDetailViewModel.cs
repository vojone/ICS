using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Carpool.App.Command;
using Carpool.App.Messages;
using Carpool.App.Services;
using Carpool.App.Wrapper;
using Carpool.BL.Facades;
using Carpool.BL.Models;


namespace Carpool.App.ViewModel
{
    public class CreateUserDetailViewModel : UserDetailViewModelBase, ICreateUserDetailViewModel
    {
        public CreateUserDetailViewModel(
            UserFacade userFacade, 
            IMediator mediator) : base(userFacade, mediator)
        {
            CancelCommand = new RelayCommand(DisplayLoginScreenControl);

            SaveUserCommand = new AsyncRelayCommand(OnSaveUser, CanSaveUser);

            mediator.Register<NewMessage<UserWrapper>>(OnUserNewMessage);
        }

        public ICommand SaveUserCommand { get; set; }

        public ICommand CancelCommand { get; set; }


        private void DisplayLoginScreenControl()
        {
            Mediator.Send(new DisplayLoginScreenMessage());
        }

        protected new async Task OnSaveUser()
        {
            await base.OnSaveUser();

            DisplayLoginScreenControl();
        }

        private void OnUserNewMessage(NewMessage<UserWrapper> _)
        {
            Model = GetEmptyUser();
            Model.Validate();
        }
    }
}
