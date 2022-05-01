using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        private readonly ISession _session;
        public RideListViewModel(RideFacade rideFacade, IMediator mediator, ISession session)
        {
            _rideFacade = rideFacade;
            _mediator = mediator;
            _session = session;

            //RideSelectedCommand = new RelayCommand<RideListModel>(RideSelected); 
            //RideNewCommand = new RelayCommand(RideNew);

            //mediator.Register<UpdateMessage<RideWrapper>>(RideUpdated);
            //mediator.Register<DeleteMessage<RideWrapper>>(RideDeleted);
            FilterRidesCommand = new RelayCommand(OnFilterRides);
            DisplayRideHistoryCommand = new RelayCommand(OnDisplayRideHistory);
            DisplayCreateRideCommand = new RelayCommand(OnDisplayCreateRide);
            OpenRideCommand = new RelayCommand<Guid>(OnOpenRide);
            DisplayUserProfileCommand = new RelayCommand(OnDisplayUserProfile);
        }

        public ICommand FilterRidesCommand { get; set; }

        public ICommand DisplayRideHistoryCommand { get; set; }

        public ICommand DisplayCreateRideCommand { get; set; }

        public ICommand OpenRideCommand { get; set; }

        public ICommand DisplayUserProfileCommand { get; set; }

        public ObservableCollection<RideListModel> Rides { get; set; } = new();

        public Guid? CurrentUserId { get; set; }

        public String ButtonText = "Book";
        private void OnFilterRides()
        {
            LoadAsync();
        }

        private void OnDisplayCreateRide()
        {
            _mediator.Send(new DisplayCreateRideMessage());
        }
        
        private async Task OnOpenRide(Guid rideId)
        {
            Guid currentUserId = _session.GetLoggedUser() ?? Guid.Empty;
            RideWrapper ride = await _rideFacade.GetAsync(rideId) ?? RideDetailModel.Empty;
;
            if (ride.DriverId == currentUserId)
            {
                Debug.WriteLine("Editing ride with id: " + rideId);
                DisplayEditRideMessage msg = new DisplayEditRideMessage();
                msg.rideId = rideId;
                _mediator.Send(msg);
            }
            else
            {
                Debug.WriteLine("Booking/Leaving ride with id: " + rideId);
                DisplayBookRideMessage msg = new DisplayBookRideMessage();
                msg.rideId = rideId;
                _mediator.Send(msg);
            }
            //RideWrapper ride = await _rideFacade.GetAsync(rideId);
            
        }

        private void OnDisplayUserProfile()
        {
            _mediator.Send(new DisplayUserProfileMessage());
        }

        private void OnDisplayRideHistory()
        {
            _mediator.Send(new DisplayRideHistoryMessage());
        }

        public async Task LoadAsync()
        {
            CurrentUserId = _session.GetLoggedUser();
            Debug.WriteLine("Ride list user id "+CurrentUserId);
            Rides.Clear();
            var rides = await _rideFacade.GetAsync();
            
            foreach (var item in rides)
            {
                Rides.Add(item);
            }

        }

    }
}
