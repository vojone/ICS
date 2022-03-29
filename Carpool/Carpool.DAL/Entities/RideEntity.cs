using Carpool.Common;

namespace Carpool.DAL.Entities;

public record RideEntity(
    Guid Id,
    Location DepartureL,
    Location ArrivalL,
    DateTime DepartureT,
    DateTime ArrivalT,
    uint InitialCapacity,
    uint Capacity,
    RideState State,
    Guid CarId,
    Guid DriverId) : IEntity
{
    public CarEntity Car { get; set; }
    public UserEntity Driver { get; set; }
    public ICollection<ParticipantEntity> Participants { get; set; } = new List<ParticipantEntity>();
}

