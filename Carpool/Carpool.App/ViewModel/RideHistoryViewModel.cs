using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private readonly IMediator _mediator;
        private readonly ISession _session;
        public RideHistoryViewModel(
            RideFacade rideFacade, 
            IMediator mediator,
            ISession session)
        {
            _rideFacade = rideFacade;
            _mediator = mediator;
            _session = session;

            GoBackCommand = new RelayCommand(OnGoBack);
            DisplayCreateRideCommand = new RelayCommand(OnDisplayCreateRideCommand);
            DisplayRideListCommand = new RelayCommand(OnDisplayRideListCommand);
            _mediator.Register<DisplayRideHistoryMessage>(OnDisplayRideHistory);
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
            var rides = await _rideFacade.GetAsync();

            CurrentUserId = _session.GetLoggedUserId() ?? Guid.Empty;

            foreach (var item in rides)
            {
                RideWrapper ride = await _rideFacade.GetAsync(item.Id);
                if ((item.ArrivalT < DateTime.Now) && 
                    (ParticipantWrapper.IsParticipant(ride, CurrentUserId) || 
                     CurrentUserId == item.DriverId))
                {
                    Rides.Add(item);
                }
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
    }
}
