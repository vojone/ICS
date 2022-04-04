using System;
using System.Collections.Generic;
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
                DepartureLId: LocationSeeds.Ostrava.Id,
                ArrivalLId: LocationSeeds.Liberec.Id,
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
        public async Task AddNew_Ride_PersistedWithNewLocation()
        {
            //Arrange
            LocationEntity departureEntity = new(
                Id: Guid.Parse("E490A154-D16A-4C28-AAFF-108A2EAFB1F5"),
                State: "Czech Republic",
                Town: "Pardubice",
                Street: "Dlouhá"
            );

            LocationEntity arrivalEntity = new(
                Id: Guid.Parse("4A762DBE-EEFD-4324-B592-1E560AEBCEEF"),
                State: "Czech Republic",
                Town: "Pardubice",
                Street: "Krátká"
            );


            RideEntity rideEntity = new(
                Id: Guid.Parse("169CCB36-F786-41FE-A8C8-82B9C0AD56A9"),
                InitialCapacity: 3,
                Capacity: 3,
                State: RideState.Planned,
                CarId: CarSeeds.Kia.Id,
                DepartureLId: departureEntity.Id,
                ArrivalLId: arrivalEntity.Id,
                DepartureT: DateTime.MaxValue,
                ArrivalT: DateTime.MinValue,
                DriverId: UserSeeds.Obiwan.Id
            );

            //Act
            CarpoolDbContextSut.Locations.Add(departureEntity);
            CarpoolDbContextSut.Locations.Add(arrivalEntity);
            CarpoolDbContextSut.Rides.Add(rideEntity);
            await CarpoolDbContextSut.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Rides
                .Include(i => i.DepartureL)
                .Include(i => i.ArrivalL)
                .SingleAsync(i => i.Id == rideEntity.Id);
            DeepAssert.Equal(rideEntity, actualEntity);
        }
    }
}
