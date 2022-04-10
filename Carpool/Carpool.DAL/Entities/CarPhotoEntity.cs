namespace Carpool.DAL.Entities;

public record CarPhotoEntity(
    Guid Id,
    string Url,
    Guid CarId) : IEntity
{

    //Parameter less constructor, because of AutoMapper
#nullable disable
    public CarPhotoEntity() : this(default, string.Empty, default) { }
#nullable enable

    public CarEntity? Car { get; set; }
};
