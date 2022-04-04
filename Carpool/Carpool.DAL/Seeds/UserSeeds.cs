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
        PhotoId: null,
        Country: null,
        Rating: 0
    );

    public static readonly UserEntity Jack = new(
        Id: Guid.Parse("19CC4BD8-644D-428E-B212-7AC068CA2307"),
        Name: "Jack",
        Surname: "Sparrow",
        PhotoId: null,
        Country: null,
        Rating: 123
    );

    public static readonly UserEntity Chuck = new(
        Id: Guid.Parse("DE5D480F-883C-4F70-81BE-9D7E9B360C31"),
        Name: "Chuck",
        Surname: "Norris",
        PhotoId: null,
        Country: "USA",
        Rating: 454657894
    );

    public static readonly UserEntity Leonardo = new(
        Id: Guid.Parse("5D825E40-9003-4BB1-A0AA-DF76571F0D2C"),
        Name: "Leonardo",
        Surname: "diCaprio",
        PhotoId: null,
        Country: "USA",
        Rating: 1
    );

    public static readonly UserEntity ChuckWithPhoto = Chuck with
    {
        Id = Guid.Parse("82DBAB40-9FF0-4F4E-BC3D-F1C1FCEF42E9"),
        PhotoId = UserPhotoSeeds.UserPhoto.Id,
        Photo = UserPhotoSeeds.UserPhoto
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

        Jack.Rides.Add(ParticipantSeeds.Participant1);
        Obiwan.Rides.Add(ParticipantSeeds.Participant2);
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            Obiwan with { Rides = Array.Empty<ParticipantEntity>() },
            Jack with { Rides = Array.Empty<ParticipantEntity>() },
            Chuck with { Cars = Array.Empty<CarEntity>()},
            ChuckWithPhoto with { Photo = null, Cars = Array.Empty<CarEntity>() },
            UpdateLeonardo,
            DeleteLeonardo
        );
    }
}
