using System.ComponentModel.DataAnnotations.Schema;
using Carpool.Common;

namespace Carpool.DAL.Entities;

public record RideEntity(
    Guid Id,
    string DepartureL,
    string ArrivalL,
    DateTime DepartureT,
    DateTime ArrivalT,
    uint InitialCapacity,
    uint Capacity,
    RideState State,
    Guid CarId,
    Guid DriverId,
    string? Description = null) : IEntity
{
    public CarEntity? Car { get; set; }
    public UserEntity? Driver { get; init; }
    public ICollection<ParticipantEntity> Participants { get; init; } = new List<ParticipantEntity>();
}

