using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.App.Command;
using Carpool.App.Messages;
using Carpool.App.Model;
using Carpool.App.Services;
using Carpool.BL.Facades;
using Carpool.BL.Models;
using Carpool.Common;

namespace Carpool.App.ViewModel
{
    internal class RideListViewModel : ViewModelBase
    {
        private readonly RideFacade _rideFacade;
        private readonly IMediator _mediator;
        public RideListViewModel(RideFacade rideFacade, IMediator mediator)
        {
            _rideFacade = rideFacade;
            _mediator = mediator;

            //RideSelectedCommand = new RelayCommand<RideListModel>(RideSelected); 
            //RideNewCommand = new RelayCommand(RideNew);

            //mediator.Register<UpdateMessage<RideWrapper>>(RideUpdated);
            //mediator.Register<DeleteMessage<RideWrapper>>(RideDeleted);
        }

        public ObservableCollection<RideListModel> Rides { get; set; } = new();


        public async Task LoadAsync()
        {
            Rides.Clear();
            var rides = await _rideFacade.GetAsync();
            
            foreach (var item in rides)
            {
                Rides.Add(item);
            }
        }

        /*public override void LoadInDesignMode()
        {
            Rides.Add(
                new RideListModel(
                    "Žilina",
                    "Brno",
                    new DateTime(2022,4,30,10,30,0), 
                    new DateTime(2022, 4, 30, 13, 20, 0),
                    4,
                    4,
                    RideState.Planned,
                    null
                    )
            );
        }*/

        //old
        /*public ICollection<RidesList> Rides { get; set; } = new List<RidesList>()
        {
            new RidesList()
            {
                Kdo = "Radek Marek",
                Odkud = "Lanškroun",
                Kam = "Brno",
                OdKdy = "15:30",
                DoKdy = "17:00",
                Datum = "Pondělí 25.5",
                Auto = "Žigulík",
                Hodnoceni = "IRON IV",
                Mista = "2",
            },
            new RidesList()
            {
                Kdo = "Radek Marek",
                Odkud = "Lanškroun",
                Kam = "Brno",
                OdKdy = "15:30",
                DoKdy = "17:00",
                Datum = "Pondělí 25.5",
                Auto = "Žigulík",
                Hodnoceni = "IRON IV",
                Mista = "2",
            },
            new RidesList()
            {
                Kdo = "Radek Marek",
                Odkud = "Lanškroun",
                Kam = "Brno",
                OdKdy = "15:30",
                DoKdy = "17:00",
                Datum = "Pondělí 25.5",
                Auto = "Žigulík",
                Hodnoceni = "IRON IV",
                Mista = "2",
            },
            new RidesList()
            {
                Kdo = "Radek Marek",
                Odkud = "Lanškroun",
                Kam = "Brno",
                OdKdy = "15:30",
                DoKdy = "17:00",
                Datum = "Pondělí 25.5",
                Auto = "Žigulík",
                Hodnoceni = "IRON IV",
                Mista = "2",
            },
            new RidesList()
            {
                Kdo = "Radek Marek",
                Odkud = "Lanškroun",
                Kam = "Brno",
                OdKdy = "15:30",
                DoKdy = "17:00",
                Datum = "Pondělí 25.5",
                Auto = "Žigulík",
                Hodnoceni = "IRON IV",
                Mista = "2",
            },
            new RidesList()
            {
                Kdo = "Radek Marek",
                Odkud = "Lanškroun",
                Kam = "Brno",
                OdKdy = "15:30",
                DoKdy = "17:00",
                Datum = "Pondělí 25.5",
                Auto = "Žigulík",
                Hodnoceni = "IRON IV",
                Mista = "2",
            },
        };*/
    }
}
