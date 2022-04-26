using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carpool.App.Model
{
    internal class CreateRide
    {
        public string State { get; set; }
        public string Town { get; set; }
        public string Street { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }

        public string State_arr { get; set; }
        public string Town_arr { get; set; }
        public string Street_arr { get; set; }
        public string Description_arr { get; set; }
        public string Date_arr { get; set; }
        public string Time_arr { get; set; }
    }
}
