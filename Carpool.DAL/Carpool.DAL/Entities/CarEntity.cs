namespace Carpool.DAL.Entities;

public class CarEntity : IEntity
{
    public Guid Id { get; set; }

    public string Brand { get; set; }

    public CarType Type { get; set; }

    public DateOnly Registration { get; set; }

    public ICollection<string> Photos { get; set; }

    public int Seats { get; set; }
}
