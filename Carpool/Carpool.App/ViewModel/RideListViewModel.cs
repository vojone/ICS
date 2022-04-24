using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.App.Model;

namespace Carpool.App.ViewModel
{
    internal class RideListViewModel
    {
        public ICollection<RidesList> Rides { get; set; } = new List<RidesList>()
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
        };
    }
}
