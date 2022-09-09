using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    public class RideHistoryViewModel : ViewModelBase, IRideHistoryViewModel
    {
        private readonly RideFacade _rideFacade;
        private readonly UserFacade _userFacade;
        private readonly IMediator _mediator;
        private readonly ISession _session;
        public RideHistoryViewModel(
            RideFacade rideFacade, 
            UserFacade userFacade,
            IMediator mediator,
            ISession session)
        {
            _rideFacade = rideFacade;
            _userFacade = userFacade;
            _mediator = mediator;
            _session = session;

            GoBackCommand = new RelayCommand(OnGoBack);
            DisplayCreateRideCommand = new RelayCommand(OnDisplayCreateRideCommand);
            DisplayRideListCommand = new RelayCommand(OnDisplayRideListCommand);
            _mediator.Register<DisplayRideHistoryMessage>(OnDisplayRideHistory);

            RateDriverCommand = new AsyncRelayCommand<Guid>(OnRateDriverCommand, CanRateDriver);
        }

        public ObservableCollection<RideListModel> Rides { get; set; } = new();

        public IUserRateViewModel UserRateViewModel { get; set; }

        public ICommand GoBackCommand { get; set; }

        public ICommand RateDriverCommand { get; set; }

        public ICommand DisplayCreateRideCommand { get; set; }

        public ICommand DisplayRideListCommand { get; set; }


        public Guid CurrentUserId { get; set; }

        private async Task OnRate(Guid id)
        {
            await UserRateViewModel.IncreaseRating(id);
        }

        private void OnGoBack()
        {
            _mediator.Send(new DisplayLastMessage());
        }

        private async void OnDisplayRideHistory(DisplayRideHistoryMessage m)
        {
            await LoadAsync();
        }

        //filtering probably?
        public async Task LoadAsync()
        {
            Rides.Clear();

            CurrentUserId = _session.GetLoggedUserId() ?? Guid.Empty;

            var rides = await _rideFacade
                .FilterAsync(arrivalTime: DateTime.Now);

            foreach (var item in rides)
            {
                Rides.Add(item);
            }
        }

        public void OnDisplayCreateRideCommand()
        {
            _mediator.Send(new DisplayCreateRideMessage());
        }

        public void OnDisplayRideListCommand()
        {
            _mediator.Send(new DisplayRideListMessage());
        }
        public async Task OnRateDriverCommand(Guid rideId)
        {
            var res = await _rideFacade.SendStar(rideId, CurrentUserId, _userFacade);
            Debug.WriteLine("Sending a star!");

            if (res)
            {
                Rides.Clear();
                await LoadAsync();
            }
        }

        public bool CanRateDriver(Guid rideId)
        {
            var ride = Rides.FirstOrDefault(r => r.Id == rideId);
           
            return ride != null && ride.Participants
                .Any(p => p.UserId == CurrentUserId && !p.HasUserRated);
        }
    }
}
