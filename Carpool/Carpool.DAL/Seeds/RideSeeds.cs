using Carpool.Common;
using Carpool.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Carpool.DAL.Seeds;

public static class RideSeeds
{
    public static readonly RideEntity Ride1 = new(
        Id: Guid.NewGuid(),
        DepartureL: new Location("Czech Repulic", "Praha", "Manesova"),
        ArrivalL: new Location("Czech Republic", "Brno", "Cejl"),
        DepartureT: new DateTime(2022, 3, 30, 18, 30, 00),
        ArrivalT: new DateTime(2022, 3, 30, 21, 30, 00),
        InitialCapacity: 3,
        Capacity: 3,
        State: RideState.Planned,
        CarId: CarSeeds.Hyundai.Id,
        DriverId: UserSeeds.Chuck.Id
    )
    {
        Driver = UserSeeds.Chuck,
        Car = CarSeeds.Hyundai,
        Participants = Array.Empty<ParticipantEntity>()
    };

    public static readonly RideEntity Ride2 = new(
        Id: Guid.NewGuid(),
        DepartureL: new Location("Czech Repulic", "Pardubice", "Svobodova"),
        ArrivalL: new Location("Czech Republic", "Svitavy", "Svobodova"),
        DepartureT: new DateTime(2022, 4, 30, 18, 30, 00),
        ArrivalT: new DateTime(2022, 4, 30, 21, 30, 00),
        InitialCapacity: 2,
        Capacity: 2,
        State: RideState.Planned,
        CarId: CarSeeds.Kia.Id,
        DriverId: UserSeeds.Chuck.Id
    )
    {
        Driver = UserSeeds.Chuck,
        Car = CarSeeds.Kia,
        Participants = Array.Empty<ParticipantEntity>()
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RideEntity>().HasData(
            Ride1,
            Ride2
        );
    }
}
