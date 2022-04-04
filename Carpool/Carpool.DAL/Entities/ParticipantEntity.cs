namespace Carpool.DAL.Entities;

public record ParticipantEntity(
    Guid Id,
    Guid UserId,
    Guid RideId,
    bool HasUserRated
) : IEntity
{
    public UserEntity? User { get; init; }
    public RideEntity? Ride { get; init; }
}
