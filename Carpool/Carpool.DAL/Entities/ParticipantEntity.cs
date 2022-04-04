namespace Carpool.DAL.Entities;

public record ParticipantEntity(
    Guid Id,
    Guid UserId,
    Guid RideId,
    bool HasUserRated
) : IEntity
{
    //Automapper workaround
#nullable disable
    public ParticipantEntity() : this(default, default, default, default) { }
#nullable enable


    public UserEntity? User { get; init; }
    public RideEntity? Ride { get; init; }
}
