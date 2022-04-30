using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Linq;
using System.Media;
using System.Reflection;
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

namespace Carpool.App.ViewModel
{
    public class EditRideDetailViewModel : RideDetailViewModelBase, IEditRideDetailViewModel
    {
        private readonly RideFacade _rideFacade;
        private readonly UserFacade _userFacade;
        private readonly IMediator _mediator;
        private readonly ISession _session;

        public EditRideDetailViewModel(
            RideFacade rideFacade,
            UserFacade userFacade,
            IMediator mediator,
            ISession session) : base(rideFacade, userFacade, mediator)
        {
            _rideFacade = rideFacade;
            _userFacade = userFacade;
            _mediator = mediator;
            _session = session;

            SaveRideCommand = new RelayCommand(OnSaveRide);
            DeleteRideCommand = new RelayCommand(OnDeleteRide);
            DisplayRideListCommand = new RelayCommand(OnDisplayRideList);
            //Model = RideDetailModel.Empty;

            _mediator.Register<DisplayEditRideMessage>(OnDisplayEditRide);
        }

        public ICommand PrintDataCommand { get; set; }

        public ICommand SaveRideCommand { get; set; }

        public ICommand DeleteRideCommand { get; set; }

        public ICommand DisplayRideListCommand { get; set; }

        private async void OnSaveRide()
        {
            await _rideFacade.SaveAsync(Model.Model);
            _mediator.Send(new DisplayRideListMessage());
        }

        private async void OnDeleteRide()
        {
            var answer = MessageBox.Show(
                "The ride will be deleted.\nAre you sure?",
                "Delete ride",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning,
                MessageBoxResult.No);

            if (answer == MessageBoxResult.Yes && Model != null)
            {
                await _rideFacade.DeleteAsync(Model.Id);
                _mediator.Send(new DisplayRideListMessage());
            }
        }

        private async void OnDisplayEditRide(DisplayEditRideMessage m)
        {
            
            await LoadAsync(m.rideId);
            Debug.WriteLine("Editing ride with id: " + Model.Id + "\nStart location: " + Model.DepartureL);
            OnPropertyChanged();
        }

        private void OnDisplayRideList()
        {
            _mediator.Send(new DisplayRideListMessage());
        }
    }
}
