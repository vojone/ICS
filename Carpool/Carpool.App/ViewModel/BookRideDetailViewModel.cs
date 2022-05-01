using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Linq;
using System.Media;
using System.Reflection;
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
    public class BookRideDetailViewModel : RideDetailViewModelBase, IBookRideDetailViewModel
    {
        private readonly RideFacade _rideFacade;
        private readonly UserFacade _userFacade;
        private readonly CarFacade _carFacade;
        private readonly IMediator _mediator;
        private readonly ISession _session;

        public BookRideDetailViewModel(
            RideFacade rideFacade,
            UserFacade userFacade,
            CarFacade carFacade,
            IMediator mediator,
            ISession session) : base(rideFacade, userFacade, mediator)
        {
            _rideFacade = rideFacade;
            _userFacade = userFacade;
            _carFacade = carFacade;
            _mediator = mediator;
            _session = session;
            
            BookRideCommand = new RelayCommand(OnBookRide,CanSaveRide);
            DisplayUserProfileCommand = new RelayCommand(OnDisplayUserProfile);
            //Model = RideDetailModel.Empty;

            _mediator.Register<DisplayBookRideMessage>(OnDisplayBookRide);
        }

        public ICommand PrintDataCommand { get; set; }

        public ICommand BookRideCommand { get; set; }

        public ICommand DisplayUserProfileCommand { get; set; }

        public UserWrapper Driver { get; set; }

        public CarWrapper? Car { get; set; }

        private async Task OnBookRide()
        {
            Guid currentUserId = _session.GetLoggedUser() ?? Guid.Empty;
            if (currentUserId != Guid.Empty)
            {
                if (ParticipantWrapper.IsParticipant( Model, currentUserId))
                {
                    await UserLeaveRide(currentUserId);
                }
                else
                {
                    await UserJoinRide(currentUserId);
                }
                _mediator.Send(new DisplayRideListMessage());
            }
        }

        private async Task UserJoinRide(Guid currentUserId)
        {
            UserWrapper currentUserWrapper = await _userFacade.GetAsync(currentUserId);

            ParticipantModel CurrentUserParticipantModel = new ParticipantModel(
                currentUserId,
                currentUserWrapper.Name,
                currentUserWrapper.Surname,
                currentUserWrapper.Rating
            );
            ParticipantWrapper CurrentUserParticipantWrapper = new ParticipantWrapper(CurrentUserParticipantModel);
            Model.Participants.Add(CurrentUserParticipantWrapper);

            await SaveAsync();
            OnPropertyChanged();
        }

        private async Task UserLeaveRide(Guid currentUserId)
        {
            UserWrapper currentUserWrapper = await _userFacade.GetAsync(currentUserId);

            ParticipantWrapper currentUserParticipant = Model.Participants.First(i => i.UserId == currentUserId);
            Model.Participants.Remove(currentUserParticipant);
            
            await SaveAsync();
            OnPropertyChanged();
        }

        private async Task OnDisplayBookRide(DisplayBookRideMessage m)
        {
            await LoadAsync(m.rideId);
            Car = await _carFacade.GetAsync(Model.CarId);
            Driver = await _userFacade.GetAsync(Model.DriverId);
            OnPropertyChanged();
        }

        private void OnDisplayUserProfile()
        {
            _mediator.Send(new DisplayUserProfileMessage());
        }
    }
}
