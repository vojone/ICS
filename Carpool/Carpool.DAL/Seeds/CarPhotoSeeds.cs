using Carpool.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Carpool.DAL.Seeds;

public static class CarPhotoSeeds
{
    public static readonly CarPhotoEntity CarPhoto = new(
        Id: Guid.Parse("B6F27258-AC7B-4772-BED1-9B4EC88C54DD"),
        Url:
        @"https://www.kia.com/content/dam/kwcms/kme/global/en/assets/vehicles/kia-sportage-nq5-my22/discover/kia-sportage-ice-gls-my22-elp-gallery.jpg",
        CarId: CarSeeds.Kia.Id
    )
    {
        Car = CarSeeds.Kia
    };

    public static readonly CarPhotoEntity DeleteCarPhoto = new(
        Id: Guid.Parse("7BA8C605-1E79-414C-A9CD-BE5FAF72A3BC"),
        Url: @"DeleteCarPhotoPicture",
        CarId: CarSeeds.DeleteKia.Id
    )
    {
        Car = CarSeeds.DeleteKia
    };

    public static readonly CarPhotoEntity EmptyPhoto = new(
        default,
        default!,
        default
    );

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarPhotoEntity>().HasData(
            CarPhoto with {Car = null},
            DeleteCarPhoto with { Car = null }
        );
    }
}
