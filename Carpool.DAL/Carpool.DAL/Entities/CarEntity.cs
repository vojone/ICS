namespace Carpool.DAL.Entities;

public class CarEntity : IEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Brand { get; set; }

    public CarType Type { get; set; }

    public DateOnly Registration { get; set; }

    public ICollection<string> PhotosUrls { get; set; }

    public uint Seats { get; set; }
}
