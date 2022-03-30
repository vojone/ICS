using Carpool.Common;
using Carpool.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Carpool.DAL.Seeds;

public static class CarSeeds
{
    public static readonly CarEntity Hyundai = new(
        Id: Guid.NewGuid(),
        Name: "Santa Fe",
        Brand: "Hyundai",
        Type: CarType.SUV,
        Registration: new DateOnly(2010, 05, 01),
        Seats: 5
    );

    public static readonly CarEntity Kia = new(
        Id: Guid.NewGuid(),
        Name: "Sportage",
        Brand: "Kia",
        Type: CarType.Sport,
        Registration: new DateOnly(2012, 05, 04),
        Seats: 4
    );

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarEntity>().HasData(
            Hyundai with { Photos = Array.Empty<PhotoEntity>()},
            Kia with { Photos = Array.Empty<PhotoEntity>() }
        );
    }
}
