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
using System.Windows;
using System.Windows.Input;
using Carpool.App.Command;
using Carpool.App.Messages;
using Carpool.App.Services;
using Carpool.App.Wrapper;
using Carpool.BL.Facades;
using Carpool.BL.Models;
using Microsoft.EntityFrameworkCore;

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
            ISession session) : base(rideFacade, userFacade, carFacade, mediator)
        {
            _rideFacade = rideFacade;
            _userFacade = userFacade;
            _carFacade = carFacade;
            _mediator = mediator;
            _session = session;
            
            BookRideCommand = new RelayCommand(OnBookRide,CanSaveRide);
            GoBackCommand = new RelayCommand(OnGoBack);
            //Model = RideDetailModel.Empty;

            _mediator.Register<DisplayBookRideMessage>(OnDisplayBookRide);
        }

        public ICommand BookRideCommand { get; set; }

        public ICommand GoBackCommand { get; set; }

        public Guid? CurrentUserId { get; set; }

        private async void OnBookRide()
        {
            Guid currentUserId = _session.GetLoggedUserId() ?? Guid.Empty;
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
                _mediator.Send(new DisplayLastMessage());
            }
        }

        private async void OnDisplayBookRide(DisplayBookRideMessage m)
        {
            CurrentUserId = _session.GetLoggedUserId();
            await LoadAsync(m.rideId);
            OnPropertyChanged();
        }

        private void OnGoBack()
        {
            _mediator.Send(new DisplayLastMessage());
        }
    }
}
