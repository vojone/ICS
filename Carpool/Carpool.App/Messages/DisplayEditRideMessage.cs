using System;

namespace Carpool.App.Messages;

public class DisplayEditRideMessage : IMessage
{
    public Guid rideId;
}
