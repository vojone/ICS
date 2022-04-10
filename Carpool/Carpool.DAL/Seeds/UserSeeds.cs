using Carpool.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;

namespace Carpool.DAL.Seeds;

public static class UserSeeds
{
    public static readonly UserEntity Obiwan = new(
        Id: Guid.Parse("0BF2856B-BB93-4170-9B37-90FF33B2F485"),
        Name: "Obiwan",
        Surname: "Kenobi",
        PhotoUrl: null,
        Country: null,
        Rating: 0
    );

    public static readonly UserEntity Jack = new(
        Id: Guid.Parse("19CC4BD8-644D-428E-B212-7AC068CA2307"),
        Name: "Jack",
        Surname: "Sparrow",
        PhotoUrl: null,
        Country: null,
        Rating: 123
    );

    public static readonly UserEntity Chuck = new(
        Id: Guid.Parse("DE5D480F-883C-4F70-81BE-9D7E9B360C31"),
        Name: "Chuck",
        Surname: "Norris",
        PhotoUrl: "TestUrl",
        Country: "USA",
        Rating: 454657894
    );

    public static readonly UserEntity Leonardo = new(
        Id: Guid.Parse("5D825E40-9003-4BB1-A0AA-DF76571F0D2C"),
        Name: "Leonardo",
        Surname: "diCaprio",
        PhotoUrl: null,
        Country: "USA",
        Rating: 1
    );

    public static readonly UserEntity EmptyUser = new(
        Id: default,
        Name: default!,
        Surname: default!,
        PhotoUrl: default,
        Country: default,
        Rating: default
    );

    public static readonly UserEntity UpdateChuck = Chuck with
    {
        Id = Guid.Parse("82DBAB40-9FF0-4F4E-BC3D-F1C1FCEF42E9"),
        PhotoUrl = "TEST" //check this seed
    };

    public static readonly UserEntity UpdateLeonardo = Leonardo with
    {
        Id = Guid.Parse("061AC62A-5701-490F-937A-95BF1FF822D9")
    };

    public static readonly UserEntity DeleteLeonardo = Leonardo with
    {
        Id = Guid.Parse("4546BED6-D1A9-4E57-BA8A-A967F01C82AB")
    };

    static UserSeeds()
    {
        Chuck.Cars.Add(CarSeeds.Hyundai);
        Chuck.Cars.Add(CarSeeds.Kia);

        Jack.Cars.Add(CarSeeds.DeleteKia);
        Jack.Cars.Add(CarSeeds.UpdateKia);

        Jack.Rides.Add(ParticipantSeeds.Participant1);
        Jack.Rides.Add(ParticipantSeeds.DeleteParticipant1);

        Obiwan.Rides.Add(ParticipantSeeds.Participant2);

        DeleteLeonardo.Rides.Add(ParticipantSeeds.UpdateParticipant);
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            Obiwan with { Cars = Array.Empty<CarEntity>(), Rides = Array.Empty<ParticipantEntity>() },
            Jack with { Cars = Array.Empty<CarEntity>(), Rides = Array.Empty<ParticipantEntity>() },
            Chuck with { Cars = Array.Empty<CarEntity>(), Rides = Array.Empty<ParticipantEntity>() },
            Leonardo with { Cars = Array.Empty<CarEntity>(), Rides = Array.Empty<ParticipantEntity>() },
            UpdateChuck with { Cars = Array.Empty<CarEntity>(), Rides = Array.Empty<ParticipantEntity>() },
            UpdateLeonardo with { Cars = Array.Empty<CarEntity>(), Rides = Array.Empty<ParticipantEntity>() },
            DeleteLeonardo with { Cars = Array.Empty<CarEntity>(), Rides = Array.Empty<ParticipantEntity>() }
        );
    }
}
