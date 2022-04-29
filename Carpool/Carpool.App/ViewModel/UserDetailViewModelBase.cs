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
    public class UserDetailViewModelBase : ViewModelBase, IUserDetailViewModelBase
    {

        protected readonly UserFacade _userFacade;
        protected readonly IMediator _mediator;

        public UserDetailViewModelBase(UserFacade userFacade, IMediator mediator)
        {
            _userFacade = userFacade;
            _mediator = mediator;
        }

        public UserWrapper? Model { get; protected set; }


        public ICommand SaveUserCommand { get; set; }


        protected async Task OnSaveUser()
        {
            await SaveAsync();
        }


        protected bool CanSaveUser()
        {
            return Model is { HasErrors: false };
        }


        public UserDetailModel GetEmptyUser()
        {
            return UserDetailModel.Empty;
        }


        public async Task LoadAsync(Guid id)
        {
            Model = await _userFacade.GetAsync(id) ?? GetEmptyUser();
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
