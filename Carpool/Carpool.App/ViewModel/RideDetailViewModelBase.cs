using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Carpool.App.Command;
using Carpool.App.Messages;
using Carpool.App.Model;
using Carpool.App.Services;
using Carpool.App.Wrapper;
using Carpool.BL.Facades;
using Carpool.BL.Models;

namespace Carpool.App.ViewModel
{
    public class RideDetailViewModelBase : ViewModelBase, IRideDetailViewModelBase
    {
        private readonly RideFacade _rideFacade;
        private readonly UserFacade _userFacade;
        private readonly IMediator _mediator;
        private readonly ISession _session;
        
        public RideDetailViewModelBase(
            RideFacade rideFacade,
            UserFacade userFacade,
            IMediator mediator)
        {
            _rideFacade = rideFacade;
            _userFacade = userFacade;
            _mediator = mediator;
            //Model = RideDetailModel.Empty;
            
        }

        public RideWrapper Model { get; set; }

        public async Task LoadAsync(Guid id)
        {
            //not running for some reason
            Debug.WriteLine("load async running");
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

        protected bool CanSaveRide()
        {
            return Model is { HasErrors: false };
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
