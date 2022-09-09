using System.Security.Cryptography.X509Certificates;

namespace Carpool.DAL.Entities;

public record UserEntity(
    Guid Id,
    string Name,
    string Surname,
    DateTime RegistrationDate,
    string? PhotoUrl,
    string? Country,
    uint Rating) : IEntity
{
    public ICollection<CarEntity> Cars { get; init; } = new List<CarEntity>();

    public ICollection<RideEntity> DrivingRides { get; init; } = new List<RideEntity>();

    public ICollection<ParticipantEntity> Rides { get; init; } = new List<ParticipantEntity>();

}
    



