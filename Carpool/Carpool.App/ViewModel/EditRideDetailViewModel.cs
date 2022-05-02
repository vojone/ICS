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
        private readonly CarFacade _carFacade;
        private readonly IMediator _mediator;
        private readonly ISession _session;

        public EditRideDetailViewModel(
            RideFacade rideFacade,
            UserFacade userFacade,
            CarFacade carFacade,
            IMediator mediator,
            ISession session) : base(rideFacade, userFacade, carFacade, mediator)
        {
            _rideFacade = rideFacade;
            _userFacade = userFacade;
            _carFacade = carFacade;
            _mediator = mediator;
            _session = session;

            SaveRideCommand = new RelayCommand(OnSaveRide);
            DeleteRideCommand = new RelayCommand(OnDeleteRide);
            GoBackCommand = new RelayCommand(OnGoBack);
            KickParticipantCommand = new RelayCommand<Guid>(OnKickParticipant);
            //Model = RideDetailModel.Empty;

            _mediator.Register<DisplayEditRideMessage>(OnDisplayEditRide);
        }

        public ICommand SaveRideCommand { get; set; }

        public ICommand DeleteRideCommand { get; set; }

        public ICommand GoBackCommand { get; set; }

        public ICommand KickParticipantCommand { get; set; }

        private async void OnSaveRide()
        {
            await SaveEditedRide();
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
                _mediator.Send(new DisplayLastMessage());
            }
        }

        private async void OnDisplayEditRide(DisplayEditRideMessage m)
        {
            await LoadAsync(m.rideId);
            if (Model.Car != null)
            {
                Car = Model.Car;
            }
            Debug.WriteLine("Editing ride with id: " + Model.Id);
            OnPropertyChanged();
        }

        private async void OnKickParticipant(Guid userId)
        {
            var answer = MessageBox.Show(
                "Do you really want to remove the user from your ride?",
                "Caution",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning,
                MessageBoxResult.No);

            if (answer == MessageBoxResult.Yes)
            {
                await UserLeaveRide(userId);
            }
            
        }

        private void OnGoBack()
        {
            _mediator.Send(new DisplayLastMessage());
        }
    }
}
