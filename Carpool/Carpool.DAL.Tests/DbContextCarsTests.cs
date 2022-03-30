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
    public class DbContexCarsTests : DbContextTestsBase
    {
        public DbContexCarsTests(ITestOutputHelper output) : base(output)
        {
        }

        public async Task GetExisting_Car()
        {
            //Act
            var entity = await CarpoolDbContextSut.Cars
                .SingleAsync(i => i.Id == CarSeeds.Hyundai.Id);

            //Assert
            DeepAssert.Equal(CarSeeds.Hyundai, entity);
        }

        [Fact]
        public async Task AddNew_Car_Persisted()
        {
            //Arrange
            CarEntity entity = new(
                Guid.Parse("142C54E5-B10F-4956-AB0B-B80007670E3C"),
                Name: "Rapid",
                Brand: "Skoda",
                Type: CarType.Sport,
                Registration: DateOnly.MinValue,
                Seats: 4
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

