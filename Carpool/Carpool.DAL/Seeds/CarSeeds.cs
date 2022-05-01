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
        Photo: "https://autobible.euro.cz/wp-content/uploads/2020/06/Hyundai-Santa-Fe-9.jpg",
        Type: CarType.SUV,
        Registration: new DateTime(2010, 05, 01),
        Seats: 5,
        OwnerId: UserSeeds.Chuck.Id
    )
    {
        Owner = UserSeeds.Chuck
    };

    public static readonly CarEntity DeleteHyundai = new(
        Id: Guid.Parse("3E900C75-E505-46E9-8B5D-84B47399171A"),
        Name: "Santa Fe",
        Brand: "Hyundai",
        Photo: "Test",
        Type: CarType.SUV,
        Registration: new DateTime(2010, 05, 01),
        Seats: 5,
        OwnerId: UserSeeds.DeleteChuck.Id
    )
    {
        Owner = UserSeeds.DeleteChuck
    };

    public static readonly CarEntity Kia = new(
        Id: Guid.Parse("B28B6CA6-E1A7-4779-8384-5147563A1F15"),
        Name: "Sportage",
        Brand: "Kia",
        Photo: "https://wallpaperaccess.com/full/5685573.jpg",
        Type: CarType.Sport,
        Registration: new DateTime(2012, 05, 04),
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
        Photo: "Test",
        Type: CarType.Minivan,
        Registration: new DateTime(2012, 10, 04),
        Seats: 4,
        OwnerId: UserSeeds.Jack.Id
    )
    {
        Owner = UserSeeds.Jack
    };

    
    public static readonly CarEntity UpdateKia = new(
        Id: Guid.Parse("13B84DC5-B61E-4A57-8E68-354ECC328301"),
        Name: "Update",
        Brand: "Kia",
        Photo: "Test",
        Type: CarType.Minivan,
        Registration: new DateTime(2002, 11, 04),
        Seats: 4,
        OwnerId: UserSeeds.UpdateLeonardo.Id
    )
    {
        Owner = UserSeeds.UpdateLeonardo
    };

    public static readonly CarEntity DeleteteKiaPhoto = new(
        Id: Guid.Parse("33B84DC5-B61E-4A57-8E68-354ECC328301"),
        Name: "Update",
        Brand: "Kia",
        Photo: "Test",
        Type: CarType.Minivan,
        Registration: new DateTime(2002, 11, 04),
        Seats: 4,
        OwnerId: UserSeeds.UpdateLeonardo.Id
    )
    {
        Owner = UserSeeds.UpdateLeonardo
    };


    public static readonly CarEntity EmptyCar = new(
        Id: default,
        Name: default!,
        Brand: default!,
        Photo: default!,
        Type: default,
        Registration: default,
        Seats: default,
        OwnerId: default
    );

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarEntity>().HasData(
            UpdateKia with { Owner = null},
            Hyundai with { Owner = null},
            Kia with { Owner = null},
            DeleteKia with { Owner = null},
            DeleteteKiaPhoto with{Owner = null}
        );
    }
}
