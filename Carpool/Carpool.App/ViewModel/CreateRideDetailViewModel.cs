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
    public class CreateRideDetailViewModel : RideDetailViewModelBase, ICreateRideDetailViewModel
    {
        private readonly RideFacade _rideFacade;
        private readonly UserFacade _userFacade;
        private readonly CarFacade _carFacade;
        private readonly IMediator _mediator;
        private readonly ISession _session;

        public CreateRideDetailViewModel(
            RideFacade rideFacade,
            UserFacade userFacade,
            IMediator mediator,
            ISession session) : base(rideFacade, userFacade, mediator)
        {
            _rideFacade = rideFacade;
            _userFacade = userFacade;
            _mediator = mediator;
            _session = session;

            PrintDataCommand = new RelayCommand(OnPrintData);
            CreateRideCommand = new RelayCommand(OnCreateRide);
            //Model = RideDetailModel.Empty;

            _mediator.Register<DisplayCreateRideMessage>(OnDisplayCreateRide);
            DisplayUserProfileCommand = new RelayCommand(OnDisplayUserProfile);
        }

        public ICommand PrintDataCommand { get; set; }

        public ICommand CreateRideCommand { get; set; }

        public ICommand DisplayUserProfileCommand { get; set; }

        public UserWrapper Driver { get; set; }

        public CarWrapper Car { get; set; }

        private void OnDisplayUserProfile()
        {
            _mediator.Send(new DisplayUserProfileMessage());
        }

        private void OnPrintData()
        {
            var answer = MessageBox.Show(
                "Ahoy me mate.\nPlease turn the volume down a bit, a short waveform audio file be played.\nJolly be off, then!",
                "Greetin' mate",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning,
                MessageBoxResult.No);

            if (answer == MessageBoxResult.Yes)
            {
                SoundPlayer player = new SoundPlayer(@"https://www.dropbox.com/s/co1v1mhpf1vrzq2/giga-chad-theme.wav?dl=1");
                player.Load();
                player.Play();
            }
            
            Debug.WriteLine("-----Ride Debug print-----");
            Debug.WriteLine("DepartureL: " + (Model != null ? Model.DepartureL : "EMPTY"));
            Debug.WriteLine("ArrivalL: " + (Model != null ? Model.ArrivalL : "EMPTY"));
            Debug.WriteLine("DepartureT: " + (Model != null ? Model.DepartureT : "EMPTY"));
            Debug.WriteLine("ArrivalT: " + (Model != null ? Model.ArrivalT : "EMPTY"));
        }

        private async Task OnCreateRide()
        {
            if (CanSaveRide())
            {
                Debug.WriteLine("Can save!");
            }
            Model.CarId = Car.Id;
            Model.DriverId = Driver.Id;
            await SaveAsync();
            _mediator.Send(new DisplayRideListMessage());
        }

        private async Task OnDisplayCreateRide(DisplayCreateRideMessage m)
        {

            var loggedUserId = _session.GetLoggedUser();
            Debug.WriteLine("User id: " + loggedUserId);

            if (loggedUserId != null)
            {
                UserDetailModel? driver = await _userFacade.GetAsync((Guid)loggedUserId);
                Driver = driver;
                Debug.WriteLine("User has cars: "+Driver.Cars.Count);
            }
            else
            {
                //error message not logged in
            }

            base.Model = RideDetailModel.Empty;
            OnPropertyChanged();
        }
    }
}
