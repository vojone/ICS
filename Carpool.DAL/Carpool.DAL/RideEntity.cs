namespace Carpool.DAL;

public class RideEntity : IEntity
{
    public Guid Id { get; set; }

    public Location StartLoc { get; set; }

    public Location EndLoc { get; set; }

    public DateTime StartT { get; set; }

    public TimeSpan Duration { get; set; }

    public Guid CarId { get; set; }

    public CarEntity Car { get; set; }
}
