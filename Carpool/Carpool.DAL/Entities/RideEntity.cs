using System.ComponentModel.DataAnnotations.Schema;
using Carpool.Common;

namespace Carpool.DAL.Entities;

public record RideEntity(
    Guid Id,
    Guid DepartureLId,
    Guid ArrivalLId,
    DateTime DepartureT,
    DateTime ArrivalT,
    uint InitialCapacity,
    uint Capacity,
    RideState State,
    Guid CarId,
    Guid DriverId) : IEntity
{

    public LocationEntity? DepartureL { get; set; }
    public LocationEntity? ArrivalL { get; set; }
    public CarEntity? Car { get; set; }
    public UserEntity? Driver { get; set; }
    public ICollection<ParticipantEntity> Participants { get; set; } = new List<ParticipantEntity>();
}

