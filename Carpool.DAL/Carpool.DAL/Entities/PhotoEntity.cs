namespace Carpool.DAL.Entities;

public class Photo : IEntity
{
    public Guid Id { get; set; }

    public string Url { get; set; }
}
