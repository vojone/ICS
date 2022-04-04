using Carpool.Common;

namespace Carpool.DAL.Entities;

public record CarEntity(
    Guid Id,
    string Name,
    string Brand,
    CarType Type,
    DateOnly Registration,
    uint Seats,
    Guid OwnerId
) : IEntity
{
    public UserEntity? Owner { get; set; }
    public ICollection<CarPhotoEntity> Photos { get; set; } = new List<CarPhotoEntity>();
}

