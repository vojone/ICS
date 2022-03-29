namespace Carpool.DAL.Entities;

public class ParticipantEntity : IEntity
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public UserEntity User { get; set; }

    public Guid RideId { get; set; }
    public RideEntity Ride { get; set; }

    public bool hasUserRated { get; set; }
}

