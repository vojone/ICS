using Carpool.App.Model;
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
            _mediator.Register<DisplayRideHistoryMessage>(OnDisplayRideHistory);
        }

        public ObservableCollection<RideListModel> Rides { get; set; } = new();

        public ICommand GoBackCommand { get; set; }

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

            Guid? currentUserId = _session.GetLoggedUserId();

            foreach (var item in rides)
            {
                RideWrapper ride = await _rideFacade.GetAsync(item.Id);
                if ((item.ArrivalT < DateTime.Now) && (IsParticipant(ride,currentUserId) || currentUserId == item.DriverId))
                {
                    Rides.Add(item);
                }
            }
        }

        private bool IsParticipant(RideWrapper ride, Guid? userId)
        {
            ParticipantWrapper? participant = ride.Participants.FirstOrDefault(p => p.UserId == userId);
            if (participant != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
