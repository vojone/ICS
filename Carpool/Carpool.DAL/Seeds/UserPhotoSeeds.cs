using Carpool.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Carpool.DAL.Seeds;

public static class UserPhotoSeeds
{
    public static readonly UserPhotoEntity UserPhoto = new(
        Id: Guid.Parse("2D37D286-70C1-4B9F-8874-88E5E4F7DA36"),
        Url: @"https://www.picng.com/upload/chuck_norris/png_chuck_norris_54781.png"
    );

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserPhotoEntity>().HasData(
            UserPhoto
        );
    }
}
