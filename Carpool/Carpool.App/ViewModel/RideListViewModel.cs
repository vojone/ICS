using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
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
using Carpool.Common;

namespace Carpool.App.ViewModel
{
    public class RideListViewModel : ViewModelBase, IRideListViewModel
    {
        private readonly RideFacade _rideFacade;
        private readonly IMediator _mediator;
        public RideListViewModel(RideFacade rideFacade, IMediator mediator)
        {
            _rideFacade = rideFacade;
            _mediator = mediator;

            //RideSelectedCommand = new RelayCommand<RideListModel>(RideSelected); 
            //RideNewCommand = new RelayCommand(RideNew);

            //mediator.Register<UpdateMessage<RideWrapper>>(RideUpdated);
            //mediator.Register<DeleteMessage<RideWrapper>>(RideDeleted);
            FilterRidesCommand = new RelayCommand(OnFilterRides);
            DisplayCreateRideCommand = new RelayCommand(OnDisplayCreateRide);
            DisplayBookRideCommand = new RelayCommand<Guid>(OnDisplayBookRide);
        }

        public ICommand FilterRidesCommand { get; set; }

        public ICommand DisplayCreateRideCommand { get; set; }

        public ICommand DisplayBookRideCommand { get; set; }

        public ObservableCollection<RideListModel> Rides { get; set; } = new();

        private void OnFilterRides()
        {
            LoadAsync();
        }

        private void OnDisplayCreateRide()
        {
            _mediator.Send(new DisplayCreateRideMessage());
        }
        
        private async void OnDisplayBookRide(Guid rideId)
        {
            Debug.WriteLine("Booking ride with id: "+rideId);
            //RideWrapper ride = await _rideFacade.GetAsync(rideId);
            DisplayBookRideMessage msg = new DisplayBookRideMessage();
            msg.rideId = rideId;
            _mediator.Send(msg);
        }

        public async Task LoadAsync()
        {
            Rides.Clear();
            var rides = await _rideFacade.GetAsync();
            
            foreach (var item in rides)
            {
                Rides.Add(item);
            }
        }

    }
}
