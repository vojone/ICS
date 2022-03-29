using System.Linq;
using Xunit;

namespace Carpool.DAL.Tests
{
    public class DbContextUserTests
    {
        private readonly CarpoolDbContext CarpoolDbContextSUT;
        public DbContextUserTests()
        {
            CarpoolDbContextSUT = new CarpoolDbContext();
        }

        [Fact]
        public void GetAll_Users()
        {
            var users = CarpoolDbContextSUT.Users.ToArray();

            Assert.True(users.Any());
        }
    }
}
