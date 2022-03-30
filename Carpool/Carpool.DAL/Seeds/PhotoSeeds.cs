using Carpool.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Carpool.DAL.Seeds;

public static class PhotoSeeds
{
    public static readonly PhotoEntity CarPhoto = new(
        Id: Guid.Parse("B6F27258-AC7B-4772-BED1-9B4EC88C54DD"),
        Url: @"https://www.kia.com/content/dam/kwcms/kme/global/en/assets/vehicles/kia-sportage-nq5-my22/discover/kia-sportage-ice-gls-my22-elp-gallery.jpg"
    );

    public static readonly PhotoEntity UserPhoto = new(
        Id: Guid.Parse("2D37D286-70C1-4B9F-8874-88E5E4F7DA36"),
        Url: @"https://www.picng.com/upload/chuck_norris/png_chuck_norris_54781.png"
    );

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PhotoEntity>().HasData(
            CarPhoto,
            UserPhoto
        );
    }
}
