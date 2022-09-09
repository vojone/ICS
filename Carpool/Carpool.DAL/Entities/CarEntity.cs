using Carpool.Common;

namespace Carpool.DAL.Entities;

public record CarEntity(
    Guid Id,
    string Name,
    string Brand,
    string? Photo,
    CarType Type,
    DateTime Registration,
    uint Seats,
    Guid OwnerId
) : IEntity
{

    //Parameter less constructor, because of AutoMapper
#nullable disable
    public CarEntity() : this(default, string.Empty, string.Empty, string.Empty, default, default, default, default) { }
#nullable enable

    public UserEntity? Owner { get; set; }
}

