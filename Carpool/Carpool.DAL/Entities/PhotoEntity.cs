namespace Carpool.DAL.Entities;

public record PhotoEntity(
    Guid Id,
    string Url
) : IEntity;

