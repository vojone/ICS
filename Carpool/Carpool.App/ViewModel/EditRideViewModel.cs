using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.App.Model;

namespace Carpool.App.ViewModel
{
    internal class EditRideViewModel
    {
        public ICollection<EditRide> EditRide { get; set; } = new List<EditRide>()
        {
            new EditRide() {CoDriver = "James May"},
            new EditRide() {CoDriver = "Jeremy Clarkson"},
            new EditRide() {CoDriver = "Richard Hammond"},
        };
    }
}
