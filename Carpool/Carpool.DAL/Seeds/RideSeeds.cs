using Carpool.Common;
using Carpool.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Carpool.DAL.Seeds;

public static class RideSeeds
{
    public static readonly RideEntity Ride1 = new(
        Id: Guid.Parse("27F22460-BF89-4068-AA2E-D0C481C03681"),
        DepartureL: "Praha",
        ArrivalL: "Brno",
        DepartureT: new DateTime(2022, 3, 30, 18, 30, 00),
        ArrivalT: new DateTime(2022, 3, 30, 21, 30, 00),
        InitialCapacity: 3,
        Capacity: 3,
        State: RideState.Planned,
        CarId: CarSeeds.Hyundai.Id,
        DriverId: UserSeeds.Chuck.Id
    )
    {
        Car = CarSeeds.Hyundai,
        Driver = UserSeeds.Chuck
    };

    public static readonly RideEntity Ride2 = new(
        Id: Guid.Parse("27720133-5190-4159-9527-AD4F21B7E1D7"),
        DepartureL: "Praha",
        ArrivalL: "Brno",
        DepartureT: new DateTime(2022, 4, 30, 18, 30, 00),
        ArrivalT: new DateTime(2022, 4, 30, 21, 30, 00),
        InitialCapacity: 2,
        Capacity: 2,
        State: RideState.Planned,
        CarId: CarSeeds.Kia.Id,
        DriverId: UserSeeds.Chuck.Id
    )
    {
        Car = CarSeeds.Kia,
        Driver = UserSeeds.Chuck
    };

    public static readonly RideEntity UpdateRide = new(
        Id: Guid.Parse("121EC629-BA60-4EBA-9ED8-C755F7EF87CF"),
        DepartureL: "Kolín",
        ArrivalL: "Praha",
        DepartureT: new DateTime(2022, 4, 30, 18, 30, 00),
        ArrivalT: new DateTime(2022, 4, 30, 21, 30, 00),
        InitialCapacity: 2,
        Capacity: 2,
        State: RideState.Planned,
        CarId: CarSeeds.Kia.Id,
        DriverId: UserSeeds.UpdateChuck.Id
    )
    {
        Car = CarSeeds.Kia,
        Driver = UserSeeds.UpdateChuck
    };

    public static readonly RideEntity DeleteRide = new(
        Id: Guid.Parse("14956E87-4F84-47C4-BD34-FFB02A703CFC"),
        DepartureL: "Praha",
        ArrivalL: "Brno",
        DepartureT: new DateTime(2022, 4, 30, 18, 30, 00),
        ArrivalT: new DateTime(2022, 4, 30, 21, 30, 00),
        InitialCapacity: 2,
        Capacity: 2,
        State: RideState.Planned,
        CarId: CarSeeds.Kia.Id,
        DriverId: UserSeeds.Chuck.Id
    )
    {
        Car = CarSeeds.Kia,
        Driver = UserSeeds.Chuck
    };

    public static readonly RideEntity EmptyRide = new(
        Id: default,
        DepartureL: default!,
        ArrivalL: default!,
        ArrivalT: default,
        DepartureT: default,
        InitialCapacity: default,
        Capacity: default,
        State: default,
        CarId: default,
        DriverId: default
    );

    static RideSeeds()
    {
        Ride1.Participants.Add(ParticipantSeeds.Participant1);
        Ride1.Participants.Add(ParticipantSeeds.Participant2);

        Ride2.Participants.Add(ParticipantSeeds.DeleteParticipant1);

        DeleteRide.Participants.Add(ParticipantSeeds.DeleteParticipant1);
        DeleteRide.Participants.Add(ParticipantSeeds.UpdateParticipant);
    }


    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RideEntity>().HasData(
            Ride1 with { Participants = Array.Empty<ParticipantEntity>(), Car = null, Driver = null },
            Ride2 with { Participants = Array.Empty<ParticipantEntity>(), Car = null, Driver = null },
            DeleteRide with { Participants = Array.Empty<ParticipantEntity>(), Car = null, Driver = null },
            UpdateRide with { Participants = Array.Empty<ParticipantEntity>(), Car = null, Driver = null }
        );
    }
}
