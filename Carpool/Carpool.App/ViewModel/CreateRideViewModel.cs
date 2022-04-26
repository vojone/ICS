using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.App.Model;

namespace Carpool.App.ViewModel
{
    internal class CreateRideViewModel
    {
        public ICollection<CreateRide> CreateRide { get; set; } = new List<CreateRide>()
        {
            new CreateRide()
            {
                Departure = "Česká republika Brno Božetěchova 1/2 Lorem Ipsum",
                Date = "22.3.2021",
                Time = "20:00:00",

                Arrival = "Austria Vienna Adamsgasse Lorem Ipsum",
                Date_arr = "22.3.2021",
                Time_arr = "23:00:00",
            }
        };
    }
}
