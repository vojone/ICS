using System.Linq;
using System.Threading.Tasks;
using Carpool.DAL.Seeds;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace Carpool.DAL.Tests
{
    public class DbContextUserTests : DbContextTestsBase
    {
        public DbContextUserTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task GetExistingChuck()
        {
            var entity = await CarpoolDbContextSut.Users.SingleAsync(i => i.Id == UserSeeds.Chuck.Id);

            //Assert
            DeepAssert.Equal(UserSeeds.Chuck, entity);
        }
    }
}

