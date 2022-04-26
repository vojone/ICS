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
                State = "Česká republika",
                Town = "Brno",
                Street = "Božetěchova 1/2",
                Description = "Lorem Ipsum",
                Date = "22.3.2021",
                Time = "20:00:00",

                State_arr = "Austria",
                Town_arr = "Vienna",
                Street_arr = "Adamsgasse",
                Description_arr = "Lorem Ipsum",
                Date_arr = "22.3.2021",
                Time_arr = "23:00:00",
            }
        };
    }
}
