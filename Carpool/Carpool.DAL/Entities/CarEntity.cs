using Carpool.Common;

namespace Carpool.DAL.Entities;

public class CarEntity : IEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Brand { get; set; }

    public CarType Type { get; set; }

    public DateOnly Registration { get; set; }

    public uint Seats { get; set; }

    public ICollection<PhotoEntity> Photos { get; set; }
}
