using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.App.Wrapper;

namespace Carpool.App.Messages
{
    public class DisplayBookRideMessage : IMessage
    {
        //public RideWrapper ride;
        public Guid rideId;
    }
}
