using Carpool.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Carpool.DAL.Seeds;

public static class PhotoSeeds
{
    public static readonly PhotoEntity CarPhoto = new(
        Id: Guid.NewGuid(),
        Url: @"https://www.kia.com/content/dam/kwcms/kme/global/en/assets/vehicles/kia-sportage-nq5-my22/discover/kia-sportage-ice-gls-my22-elp-gallery.jpg"
    );

    public static readonly PhotoEntity UserPhoto = new(
        Id: Guid.NewGuid(),
        Url: @"https://www.picng.com/upload/chuck_norris/png_chuck_norris_54781.png"
    );

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            CarPhoto,
            UserPhoto
        );
    }
}
