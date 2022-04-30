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
using Carpool.BL.Facades;
using Carpool.BL.Models;

namespace Carpool.App.ViewModel
{
    public class RideHistoryViewModel : ViewModelBase, IRideHistoryViewModel
    {
        private readonly RideFacade _rideFacade;
        private readonly IMediator _mediator;
        public RideHistoryViewModel(RideFacade rideFacade, IMediator mediator)
        {
            _rideFacade = rideFacade;
            _mediator = mediator;

            DisplayRideListCommand = new RelayCommand(OnDisplayRideList);
        }

        public ObservableCollection<RideListModel> Rides { get; set; } = new();

        public ICommand DisplayRideListCommand { get; set; }

        private void OnDisplayRideList()
        {
            _mediator.Send(new DisplayRideListMessage());
        }

        //filtering probably?
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
