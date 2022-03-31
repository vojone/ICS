using Carpool.Common;
using Carpool.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Carpool.DAL.Seeds;

public static class LocationSeeds
{
    public static readonly LocationEntity Praha = new(
        Id: Guid.Parse("89F1CD79-F598-4A6E-911B-E8BC201768E6"),
        Town: "Praha",
        State: "Czech Republic",
        Street: "Manesova"
    );
    

    public static readonly LocationEntity Brno = new(
        Id: Guid.Parse("0B1C302C-9F1D-401E-9BBC-73D08F7EACC4"),
        Town: "Brno",
        State: "Czech Republic",
        Street: "Cejl"
    );

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LocationEntity>().HasData(
            Praha,
            Brno
        );
    }
}
