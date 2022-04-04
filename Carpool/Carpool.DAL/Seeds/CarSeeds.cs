using Carpool.Common;
using Carpool.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Carpool.DAL.Seeds;

public static class CarSeeds
{
    public static readonly CarEntity Hyundai = new(
        Id: Guid.Parse("10D6F922-7B64-4497-8E46-0C8C69354A49"),
        Name: "Santa Fe",
        Brand: "Hyundai",
        Type: CarType.SUV,
        Registration: new DateOnly(2010, 05, 01),
        Seats: 5,
        OwnerId: UserSeeds.Chuck.Id
    )
    {
        Owner = UserSeeds.Chuck
    };

    public static readonly CarEntity Kia = new(
        Id: Guid.Parse("B28B6CA6-E1A7-4779-8384-5147563A1F15"),
        Name: "Sportage",
        Brand: "Kia",
        Type: CarType.Sport,
        Registration: new DateOnly(2012, 05, 04),
        Seats: 4,
        OwnerId: UserSeeds.Chuck.Id
    )
    {
        Owner = UserSeeds.Chuck
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarEntity>().HasData(
            Hyundai with { Owner = null},
            Kia with { Owner = null }
        );
    }
}
