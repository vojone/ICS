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
    public class DbContextRideTests : DbContextTestsBase
    {
        public DbContextRideTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task GetExisting_Ride1Id()
        {
            //Act
            var entity = await CarpoolDbContextSut.Rides
                .SingleAsync(i => i.Id == RideSeeds.Ride1.Id);

            //Assert
            DeepAssert.Equal(RideSeeds.Ride1.Id, entity.Id);
        }

        [Fact]
        public async Task AddNew_Ride_Persisted()
        {
            //Arrange
            RideEntity entity = new(
                Guid.Parse("169CCB36-F786-41FE-A8C8-82B9C0AD56A9"),
                InitialCapacity: 3,
                Capacity: 3,
                State: RideState.Planned,
                CarId: CarSeeds.Kia.Id,
                DepartureLId: LocationSeeds.Praha.Id,
                ArrivalLId: LocationSeeds.Brno.Id,
                DepartureT: DateTime.MaxValue,
                ArrivalT: DateTime.MinValue,
                DriverId: UserSeeds.Obiwan.Id
            );

            //Act
            CarpoolDbContextSut.Rides.Add(entity);
            await CarpoolDbContextSut.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntities = await dbx.Rides.SingleAsync(i => i.Id == entity.Id);
            DeepAssert.Equal(entity, actualEntities);
        }
    }
}
