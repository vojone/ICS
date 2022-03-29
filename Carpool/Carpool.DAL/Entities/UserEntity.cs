namespace Carpool.DAL.Entities
{
    public class UserEntity
    {
        Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public PhotoEntity? Photo { get; set; }

        public string? Country { get; set; }

        public uint Rating { get; set; }

        public ICollection<CarEntity>? Cars { get; set; }

        public ICollection<RideEntity>? DrivingRides { get; set; }

        public ICollection<ParticipantsEntity>? Rides { get; set; }
    }
}


