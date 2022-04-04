using Carpool.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Carpool.DAL.Seeds;

public static class CarPhotoSeeds
{
    public static readonly CarPhotoEntity CarPhoto = new(
        Id: Guid.Parse("B6F27258-AC7B-4772-BED1-9B4EC88C54DD"),
        Url: @"https://www.kia.com/content/dam/kwcms/kme/global/en/assets/vehicles/kia-sportage-nq5-my22/discover/kia-sportage-ice-gls-my22-elp-gallery.jpg",
        CarId: CarSeeds.Kia.Id
    );

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarPhotoEntity>().HasData(
            CarPhoto
        );
    }
}
