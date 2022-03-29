using System.Security.Cryptography.X509Certificates;

namespace Carpool.DAL.Entities;

public record UserEntity(
    Guid Id,
    string Name,
    string Surname,
    Guid PhotoId,
    string? Country,
    uint Rating) : IEntity
{
    public PhotoEntity? Photo { get; init; }
    public ICollection<CarEntity>? Cars { get; init; } = new List<CarEntity>();

    public ICollection<RideEntity>? DrivingRides { get; init; } = new List<RideEntity>();

    public ICollection<ParticipantEntity>? Rides { get; init; } = new List<ParticipantEntity>();

}
    



