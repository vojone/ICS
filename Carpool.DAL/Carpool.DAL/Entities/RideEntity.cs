namespace Carpool.DAL.Entities;

public class RideEntity : IEntity
{
    public Guid Id { get; set; }

    public UserEntity Driver { get; set; }

    public Location StartLoc { get; set; }

    public Location EndLoc { get; set; }

    public DateTime StartT { get; set; }

    public DateTime EndT { get; set; }

    public int InitialCapacity { get; set; }

    public int Capacity { get; set; }

    public ICollection<UserEntity> Participants { get; set; }

    public CarEntity Car { get; set; }
}
