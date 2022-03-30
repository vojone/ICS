namespace Carpool.DAL.Entities;

public record LocationEntity(
    Guid Id,
    string State,
    string Town,
    string Street,
    string? Description = null) : IEntity;

