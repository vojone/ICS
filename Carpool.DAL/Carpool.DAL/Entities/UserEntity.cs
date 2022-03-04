namespace Carpool.DAL.Entities
{
    public class UserEntity
    {
        Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string? PhotoUrl { get; set; }

        public string? Country { get; set; }

        public uint Rating { get; set; }

        public ICollection<Guid> CarsId { get; set; }
        public ICollection<CarEntity>? Cars { get; set; }

        public ICollection<Guid> RidesId { get; set; }
        public ICollection<RideEntity> Rides { get; set; }
    }
}


