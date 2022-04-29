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
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace Carpool.App.ViewModel
{
    public class UserDetailViewModel : ViewModelBase, IUserDetailViewModel
    {

        private readonly UserFacade _userFacade;
        private readonly IMediator _mediator;

        public UserDetailViewModel(UserFacade userFacade, IMediator mediator)
        {
            _userFacade = userFacade;
            _mediator = mediator;

            DisplayLoginScreenCommand = new RelayCommand(DisplayLoginScreen);

            SaveUserCommand = new AsyncRelayCommand(OnSaveUser, CanSaveUser);

            mediator.Register<NewMessage<UserWrapper>>(OnUserNewMessage);
        }


        public ICommand DisplayLoginScreenCommand { get; set; }

        public ICommand SaveUserCommand { get; set; }


        private void DisplayLoginScreen()
        {
            _mediator.Send(new DisplayLoginScreenMessage());
        }

        private async Task OnSaveUser()
        {
            await SaveAsync();
            DisplayLoginScreen();
        }

        private void OnUserNewMessage(NewMessage<UserWrapper> _)
        {
            Model = GetEmptyUser();
            Model.Validate();
        }

        public UserWrapper? Model { get; private set; }


        public async Task LoadAsync(Guid id)
        {
            Model = await _userFacade.GetAsync(id) ?? GetEmptyUser();
        }

        public UserDetailModel GetEmptyUser()
        {
            return UserDetailModel.Empty;
        }


        public async Task SaveAsync()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            Model = await _userFacade.SaveAsync(Model.Model);
            _mediator.Send(new UpdateMessage<UserWrapper> { Model = Model });
        }


        private bool CanSaveUser()
        {
            return !(Model.HasErrors);
        }



        public async Task DeleteAsync()
        {
            if (Model is null)
            {
                throw new InvalidOperationException("Null model cannot be deleted");
            }

            if (Model.Id != Guid.Empty)
            {
                try
                {
                    await _userFacade.DeleteAsync(Model!.Id);
                }
                catch
                {
                    //TODO showing error msg
                }

                _mediator.Send(new DeleteMessage<UserWrapper>
                {
                    Model = Model
                });
            }
        }
    }
}
