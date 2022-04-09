using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carpool.Common;
using Carpool.Common.Tests;
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
        public async Task GetExisting_Ride1OnlyId()
        {
            //Act
            var entity = await CarpoolDbContextSut.Rides
                .SingleAsync(i => i.Id == RideSeeds.Ride1.Id);

            //Assert
            DeepAssert.Equal(RideSeeds.Ride1.Id, entity.Id);
        }

        [Fact]
        public async Task GetExisting_ParticipantsOfRide1()
        {
            //Act
            var entities = await CarpoolDbContextSut.Participants
                .Where(i => i.RideId == RideSeeds.Ride1.Id)
                .ToArrayAsync();

            //Assert
            Assert.Contains(ParticipantSeeds.Participant1 with { User = null, Ride = null}, entities);
            Assert.Contains(ParticipantSeeds.Participant2 with { User = null, Ride = null }, entities);
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
                DepartureL:"Ostrava",
                ArrivalL: "Liberec",
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


        [Fact]
        public async Task Delete_Ride()
        {
            //Arrange
            var baseEntity = RideSeeds.DeleteRide;
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var toBeDeleted = await dbx.Rides
                .SingleAsync(i => i.Id == baseEntity.Id);

            //Act
            Assert.NotNull(toBeDeleted);
            dbx.Rides.Remove(toBeDeleted);
            await dbx.SaveChangesAsync();

            //Assert
            Assert.False(await dbx.Rides.AnyAsync(i => i.Id == baseEntity.Id));

            //Driver and car should be still in DB
            Assert.True(await dbx.Users.AnyAsync(i => i.Id == baseEntity.DriverId));
            Assert.True(await dbx.Cars.AnyAsync(i => i.Id == baseEntity.CarId));

            //Participants not
            Assert.False(await dbx.Participants.AnyAsync(i => i.RideId == baseEntity.Id));
        }
    }
}
