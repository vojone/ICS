using Carpool.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Carpool.DAL.Seeds;

public static class UserSeeds
{
    public static readonly UserEntity Obiwan = new(
        Id: Guid.Parse("00000000-0000-0000-0000-000000000001"), 
        Name: "Obiwan",
        Surname: "Kenobi",
        PhotoId: null,
        Country: null,
        Rating: 0
    )
    {
        Photo = null,
        Cars = null,
        DrivingRides = null,
        Rides = null
    };

    public static readonly UserEntity Jack = new(
        Id: Guid.NewGuid(),
        Name: "Jack",
        Surname: "Sparrow",
        PhotoId: null,
        Country: null,
        Rating: 123
    )
    {
        Photo = null,
        Cars = null,
        DrivingRides = null,
        Rides = null
    };

    public static readonly UserEntity Chuck = new(
        Id: Guid.Parse("00000000-0000-0000-0000-000000000002"),
        Name: "Chuck",
        Surname: "Norris",
        PhotoId: null,
        Country: "USA",
        Rating: 454657894
    )
    {
        Photo = null,
        Cars = null,
        DrivingRides = null,
        Rides = null
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            Obiwan,
            Jack,
            Chuck
        );
    }
}
