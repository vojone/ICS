using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carpool.App.Model
{
    internal class CreateRide
    {
        public string Departure { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }

        public string Arrival { get; set; }
        public string Date_arr { get; set; }
        public string Time_arr { get; set; }
    }
}
