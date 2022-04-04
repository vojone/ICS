using System;
using System.Linq;
using System.Threading.Tasks;
using Carpool.Common;
using Carpool.DAL.Entities;
using Carpool.DAL.Seeds;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace Carpool.DAL.Tests
{
    // Contains only basic use cases to ensure that DB context is well configured
    public class DbContextCarsTests : DbContextTestsBase
    {
        public DbContextCarsTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task GetExisting_Car()
        {
            //Act
            var entity = await CarpoolDbContextSut.Cars
                .SingleAsync(i => i.Id == CarSeeds.Hyundai.Id);

            //Assert
            DeepAssert.Equal(CarSeeds.Hyundai with {Owner = null}, entity);
        }

        [Fact]
        public async Task AddNew_Car_Persisted()
        {
            //Arrange
            CarEntity entity = new(
                Id:Guid.Parse("142C54E5-B10F-4956-AB0B-B80007670E3C"),
                Name: "Rapid",
                Brand: "Skoda",
                Type: CarType.Sport,
                Registration: DateOnly.MinValue,
                Seats: 4,
                OwnerId: UserSeeds.Chuck.Id
            );

            //Act
            CarpoolDbContextSut.Cars.Add(entity);
            await CarpoolDbContextSut.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntities = await dbx.Cars.SingleAsync(i => i.Id == entity.Id);
            DeepAssert.Equal(entity, actualEntities);
        }
    }
}

