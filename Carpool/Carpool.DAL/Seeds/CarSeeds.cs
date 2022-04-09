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


    public static readonly CarEntity DeleteKia = new(
        Id: Guid.Parse("A26D094F-129A-4FA1-AE44-6039091BB040"),
        Name: "Delete",
        Brand: "Kia",
        Type: CarType.Minivan,
        Registration: new DateOnly(2012, 10, 04),
        Seats: 4,
        OwnerId: UserSeeds.Jack.Id
    )
    {
        Owner = UserSeeds.Jack
    };


    public static readonly CarEntity UpdateKia = Kia with
    {
        Id = Guid.Parse("13B84DC5-B61E-4A57-8E68-354ECC328301"),
        OwnerId = UserSeeds.DeleteLeonardo.Id,
        Owner = UserSeeds.DeleteLeonardo
    };


    public static readonly CarEntity EmptyCar = new(
        Id: default,
        Name: default!,
        Brand: default!,
        Type: default,
        Registration: default,
        Seats: default,
        OwnerId: default
    );

    static CarSeeds()
    {
        Kia.Photos.Add(CarPhotoSeeds.CarPhoto);
        DeleteKia.Photos.Add(CarPhotoSeeds.DeleteCarPhoto);
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarEntity>().HasData(
            Hyundai with { Owner = null, Photos = Array.Empty<CarPhotoEntity>() },
            Kia with { Owner = null, Photos = Array.Empty<CarPhotoEntity>() },
            DeleteKia with { Owner = null, Photos = Array.Empty<CarPhotoEntity>() },
            UpdateKia with { Owner = null, Photos = Array.Empty<CarPhotoEntity>() }
        );
    }
}
