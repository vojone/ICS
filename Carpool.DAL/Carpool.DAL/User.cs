using System.Data.Common;

namespace Carpool.DAL
{
    public class User
    {
        Guid Id { get; set; }

        public string Login { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Photo { get; set; }

        public int Rating { get; set; }

        public DateTime LicenceSince { get; set; }

        public Distance Driven { get; set; }

        public ICollection<CarEntity> Cars { get; set; }

        public ICollection<Guid> CarsId { get; set; } 
    }
}
