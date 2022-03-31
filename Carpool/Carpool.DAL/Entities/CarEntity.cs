using Carpool.Common;

namespace Carpool.DAL.Entities;

public record CarEntity(
    Guid Id,
    string Name,
    string Brand,
    CarType Type,
    DateOnly Registration,
    uint Seats
) : IEntity
{
    public ICollection<PhotoEntity> Photos { get; set; } = new List<PhotoEntity>();
}

