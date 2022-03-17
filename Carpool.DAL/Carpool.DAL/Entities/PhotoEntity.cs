namespace Carpool.DAL.Entities;

public class PhotoEntity : IEntity
{
    public Guid Id { get; set; }

    public string Url { get; set; }
}
