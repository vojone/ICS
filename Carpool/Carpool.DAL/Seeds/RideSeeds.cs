using Carpool.Common;
using Carpool.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Carpool.DAL.Seeds;

public static class RideSeeds
{
    public static readonly RideEntity Ride1 = new(
        Id: Guid.Parse("27F22460-BF89-4068-AA2E-D0C481C03681"),
        DepartureLId: LocationSeeds.Praha.Id,
        ArrivalLId: LocationSeeds.Brno.Id,
        DepartureT: new DateTime(2022, 3, 30, 18, 30, 00),
        ArrivalT: new DateTime(2022, 3, 30, 21, 30, 00),
        InitialCapacity: 3,
        Capacity: 3,
        State: RideState.Planned,
        CarId: CarSeeds.Hyundai.Id,
        DriverId: UserSeeds.Chuck.Id
    )
    {
        DepartureL = LocationSeeds.Praha,
        ArrivalL = LocationSeeds.Brno,
        Driver = UserSeeds.Chuck,
        Car = CarSeeds.Hyundai,
        Participants = Array.Empty<ParticipantEntity>()
    };

    public static readonly RideEntity Ride2 = new(
        Id: Guid.Parse("27720133-5190-4159-9527-AD4F21B7E1D7"),
        DepartureLId: LocationSeeds.Praha.Id,
        ArrivalLId: LocationSeeds.Brno.Id,
        DepartureT: new DateTime(2022, 4, 30, 18, 30, 00),
        ArrivalT: new DateTime(2022, 4, 30, 21, 30, 00),
        InitialCapacity: 2,
        Capacity: 2,
        State: RideState.Planned,
        CarId: CarSeeds.Kia.Id,
        DriverId: UserSeeds.Chuck.Id
    )
    {
        DepartureL = LocationSeeds.Praha,
        ArrivalL = LocationSeeds.Brno,
        Driver = UserSeeds.Chuck,
        Car = CarSeeds.Kia,
        Participants = Array.Empty<ParticipantEntity>()
    };

    static RideSeeds()
    {
        Ride1.ArrivalL = LocationSeeds.Praha;
        Ride1.DepartureL = LocationSeeds.Brno;
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RideEntity>().HasData(
            Ride1 with { DepartureL = null, ArrivalL = null, Driver = null, Participants = Array.Empty<ParticipantEntity>(), Car = null},
            Ride2 with { DepartureL = null, ArrivalL = null, Driver = null, Participants = Array.Empty<ParticipantEntity>(), Car = null}
        );
    }
}
