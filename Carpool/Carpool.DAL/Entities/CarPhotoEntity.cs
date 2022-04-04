namespace Carpool.DAL.Entities;

public record CarPhotoEntity(
    Guid Id,
    string Url,
    Guid CarId) : IPhoto
{
    public CarEntity? Car { get; set; }
};
