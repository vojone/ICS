namespace Carpool.DAL.Entities;

public class RideEntity : IEntity
{
    public Guid Id { get; set; }

    public Location DepartureL { get; set; }
    
    public Location ArrivalL { get; set; }

    public DateTime DepartureT { get; set; }

    public DateTime ArrivalT { get; set; }

    public uint InitialCapacity { get; set; }

    public uint Capacity { get; set; }

    public CarEntity Car { get; set; }

    public UserEntity Driver { get; set; }

    public ICollection<UserEntity> Participants { get; set; }

    public ICollection<UserEntity> RatedBy { get; set; }
}
