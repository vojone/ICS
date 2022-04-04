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

    public static readonly LocationEntity Pardubice = new(
        Id: Guid.Parse("EC77A2EF-D8CA-4834-B52F-D50E7C756404"),
        Town: "Pardubice",
        State: "Czech Republic",
        Street: "Perníková"
    );

    public static readonly LocationEntity Olomouc = new(
        Id: Guid.Parse("88E1EEFA-5913-40D9-B0DC-480F710FFD0C"),
        Town: "Olomouc",
        State: "Czech Republic",
        Street: "Olomoucká"
    );

    public static readonly LocationEntity Ostrava = new(
        Id: Guid.Parse("FDA89BB3-2AD1-4920-89ED-6525B403266F"),
        Town: "Ostrava",
        State: "Czech Republic",
        Street: "Uhelná"
    );


    public static readonly LocationEntity Liberec = new(
        Id: Guid.Parse("BA6A70E4-2167-48E2-8E09-75DDCB758858"),
        Town: "Liberec",
        State: "Czech Republic",
        Street: "Náměstí"
    );

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LocationEntity>().HasData(
            Praha,
            Brno,
            Olomouc,
            Pardubice,
            Ostrava,
            Liberec
        );
    }
}
