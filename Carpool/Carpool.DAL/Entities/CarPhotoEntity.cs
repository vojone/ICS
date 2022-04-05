namespace Carpool.DAL.Entities;

public record CarPhotoEntity(
    Guid Id,
    string Url,
    Guid CarId) : IEntity
{
    public CarEntity? Car { get; set; }
};
