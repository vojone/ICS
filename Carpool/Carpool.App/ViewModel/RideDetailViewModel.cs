using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Linq;
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
    public class RideDetailViewModel : ViewModelBase, IRideDetailViewModel
    {
        private readonly RideFacade _rideFacade;
        private readonly IMediator _mediator;
        public RideDetailViewModel(RideFacade rideFacade, IMediator mediator)
        {
            _rideFacade = rideFacade;
            _mediator = mediator;

            PrintDataCommand = new RelayCommand(OnPrintData);
            CreateRideCommand = new RelayCommand(OnCreateRide);
            //Model = RideDetailModel.Empty;
            
            _mediator.Register<DisplayCreateRideMessage>(OnDisplayCreateRide);
        }

        public RideWrapper Model { get; private set; }

        public ICommand PrintDataCommand { get; set; }

        public ICommand CreateRideCommand { get; set; }

        private void OnPrintData()
        {
            Debug.WriteLine("-----Ride Debug print-----");
            Debug.WriteLine("DepartureL: "+(Model != null ? Model.DepartureL :  "EMPTY"));
            Debug.WriteLine("ArrivalL: " + (Model != null ? Model.ArrivalL : "EMPTY"));
            Debug.WriteLine("DepartureT: " + (Model != null ? Model.DepartureT : "EMPTY"));
            Debug.WriteLine("ArrivalT: " + (Model != null ? Model.ArrivalT : "EMPTY"));
        }

        private void OnCreateRide()
        {
            _mediator.Send(new DisplayRideListMessage());
        }

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

        //private bool CanSave() => Model?.IsValid ?? false;

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

        private void OnDisplayCreateRide(DisplayCreateRideMessage m)
        {
            Model = RideDetailModel.Empty;
        }
    }
}
