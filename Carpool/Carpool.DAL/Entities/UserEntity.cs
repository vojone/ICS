using System.Security.Cryptography.X509Certificates;

namespace Carpool.DAL.Entities;

public record UserEntity(
    Guid Id,
    string Name,
    string Surname,
    Guid? PhotoId,
    string? Country,
    uint Rating) : IEntity
{

    public PhotoEntity? Photo { get; set; }
    public ICollection<CarEntity>? Cars { get; set; } = new List<CarEntity>();

    public ICollection<RideEntity>? DrivingRides { get; set; } = new List<RideEntity>();

    public ICollection<ParticipantEntity>? Rides { get; set; } = new List<ParticipantEntity>();

}
    



