using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.App.Command;
using Carpool.App.Messages;
using Carpool.App.Services;
using Carpool.App.Wrapper;
using Carpool.BL.Facades;
using Carpool.BL.Models;

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
        }


        public UserWrapper? Model { get; private set; }

        public async Task LoadAsync(Guid id)
        {
            Model = await _userFacade.GetAsync(id) ?? UserDetailModel.Empty;
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

        private bool CanSave() => Model?.IsValid ?? false;


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
