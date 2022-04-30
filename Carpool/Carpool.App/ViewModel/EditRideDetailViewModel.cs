using System;
using System.Collections.Generic;
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

            EditRideCommand = new RelayCommand(OnEditRide);
            DisplayUserProfileCommand = new RelayCommand(OnDisplayUserProfile);
            //Model = RideDetailModel.Empty;

            _mediator.Register<DisplayBookRideMessage>(OnDisplayBookRide);
        }

        public ICommand PrintDataCommand { get; set; }

        public ICommand EditRideCommand { get; set; }

        public ICommand DisplayUserProfileCommand { get; set; }

        private void OnEditRide()
        {
            _mediator.Send(new DisplayRideListMessage());
        }

        private async void OnDisplayBookRide(DisplayBookRideMessage m)
        {
            await LoadAsync(m.rideId);
            OnPropertyChanged();
        }

        private void OnDisplayUserProfile()
        {
            _mediator.Send(new DisplayUserProfileMessage());
        }
    }
}
