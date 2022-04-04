namespace Carpool.DAL.Entities;

public record UserPhotoEntity(
    Guid Id,
    string Url) : IPhoto;

