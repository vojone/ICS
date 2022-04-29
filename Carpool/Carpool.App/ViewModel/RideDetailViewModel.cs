using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.App.Messages;
using Carpool.App.Model;
using Carpool.App.Services;
using Carpool.App.Wrapper;
using Carpool.BL.Facades;
using Carpool.BL.Models;

namespace Carpool.App.ViewModel
{
    public class RideDetailViewModel : ViewModelBase, IRideDetailViewModel
    {
        private readonly RideFacade _rideFacade;
        private readonly IMediator _mediator;
        public RideDetailViewModel(RideFacade rideFacade, IMediator mediator)
        {
            _rideFacade = rideFacade;
            _mediator = mediator;
        }

        public RideWrapper Model { get; private set; }

        public async Task LoadAsync(Guid id)
        {
            Model = await _rideFacade.GetAsync(id) ?? RideDetailModel.Empty;
        }

        public async Task SaveAsync()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            Model = await _rideFacade.SaveAsync(Model.Model);
            _mediator.Send(new UpdateMessage<RideWrapper> { Model = Model });
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
                    await _rideFacade.DeleteAsync(Model!.Id);
                }
                catch
                {
                    //TODO showing error msg
                }

                _mediator.Send(new DeleteMessage<RideWrapper>
                {
                    Model = Model
                });
            }
        }
    }
}
