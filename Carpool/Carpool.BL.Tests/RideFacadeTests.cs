using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Carpool.BL.Facades;
using Carpool.BL.Models;
using Carpool.Common;
using Carpool.Common.Tests;
using Carpool.DAL.Seeds;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Carpool.BL.Tests
{
    public class RideFacadeTests : CRUDFacadeTestsBase
    {
        private readonly RideFacade _rideFacadeSut;

        public RideFacadeTests(ITestOutputHelper output) : base(output)
        {
            _rideFacadeSut = new RideFacade(UnitOfWorkFactory, Mapper);
        }


        [Fact]
        public async Task Create_NewRide_DoesNotThrow()
        {
            //Arrange
            var model = new RideDetailModel(
                DepartureL: @"Hradec Králové",
                ArrivalL: @"Pardubice",
                DepartureT: new DateTime(2022, 4, 10, 12, 30, 00),
                ArrivalT: new DateTime(2022, 4, 10, 14, 30, 00),
                InitialCapacity: 4,
                Capacity: 4,
                State: RideState.Planned,
                CarId: CarSeeds.Hyundai.Id,
                DriverId: UserSeeds.Chuck.Id
            );
            

            //Act
            var _ = await _rideFacadeSut.SaveAsync(model);
        }


        [Fact]
        public async Task GetAll_SeededRides()
        {
            //Arrange
            var rides = await _rideFacadeSut.GetAsync();

            //Act
            var ride = rides.Single(i => i.Id == RideSeeds.Ride1.Id);

            //Assert
            var expectedModel = Mapper.Map<RideListModel>(RideSeeds.Ride1);
            DeepAssert.Equal(expectedModel, ride);
        }


        [Fact]
        public async Task GetById_SeededRide1()
        {
            //Act
            var ride = await _rideFacadeSut.GetAsync(RideSeeds.Ride1.Id);

            //Assert
            var expectedRideModel = Mapper.Map<RideDetailModel>(RideSeeds.Ride1);

            Assert.NotNull(ride);
            DeepAssert.Equal(expectedRideModel, ride);
        }


        [Fact]
        public async Task GetById_NonExistentRide()
        {
            //Act
            var ride = await _rideFacadeSut.GetAsync(RideSeeds.EmptyRide.Id);

            //Assert
            Assert.Null(ride);
        }

        //Demonstrates filtering of rides by arrival location
        [Fact]
        public async Task GetByArrivalLocation_SeededRide()
        {
            //Arrange
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();

            //Act
            var rides = dbxAssert.Rides
                .Where(i => i.ArrivalL == RideSeeds.Ride1.ArrivalL);

            //Assert
            Assert.All(rides, ride =>
                Assert.Equal(ride.ArrivalL, RideSeeds.Ride1.ArrivalL)
            );

            Assert.Contains(rides, i => i.Id == RideSeeds.Ride1.Id);
        }

        //Demonstrates filtering of rides by driver
        [Fact]
        public async Task GetByDriverId_SeededRide()
        {
            //Arrange
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();

            //Act
            var rides = dbxAssert.Rides
                .Where(i => i.DriverId == RideSeeds.Ride1.DriverId);

            //Assert
            Assert.Contains(rides, i => i.Id == RideSeeds.Ride1.Id);
        }

        //Demonstrates filtering of rides by participants
        [Fact]
        public async Task GetBySpecificParticipant_SeededRide()
        {
            //Arrange
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();

            //Act
            var participationWRide = dbxAssert.Rides.Join(dbxAssert.Participants,
                                             ride => ride.Id,
                                             participant => participant.RideId,
                                             (ride, participant) => new { Ride = ride, Participant = participant })
                                                .Where(i => i.Participant.UserId == UserSeeds.Jack.Id).ToList();

            //Assert
            Assert.All(participationWRide, 
                p => Assert.Equal(p.Participant.UserId, UserSeeds.Jack.Id)
            );
        }

        //Demonstrates filtering of rides by participants
        [Fact]
        public async Task GetByDepartureTime_SeededRide()
        {
            //Arrange
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();

            //Act
            var rides = dbxAssert.Rides
                .Where(i => i.DepartureT == RideSeeds.Ride2.DepartureT);

            //Assert
            Assert.All(rides, ride => 
                Assert.Equal(ride.DepartureT, RideSeeds.Ride2.DepartureT)
            );
        }


        [Fact]
        public async Task InsertOrUpdate_UpdateRide()
        {
            //Arrange
            var ride = await _rideFacadeSut.GetAsync(RideSeeds.UpdateRide.Id);

            Assert.NotNull(ride);
            ride.ArrivalT = new DateTime(2022, 10, 11, 14, 10, 00);
            ride.State = RideState.OnGoing;

            //Act
            await _rideFacadeSut.SaveAsync(ride);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var rideFromDb = await dbxAssert.Rides
                .SingleAsync(i => i.Id == ride.Id);

            DeepAssert.Equal(ride with {Driver = null, Car = null}, Mapper.Map<RideDetailModel>(rideFromDb));
        }


        [Fact]
        public async Task InsertOrUpdate_UpdateCarOfUpdateRide()
        {
            //Arrange
            var ride = await _rideFacadeSut.GetAsync(RideSeeds.UpdateRide.Id);

            Assert.NotNull(ride);
            ride.CarId = CarSeeds.Hyundai.Id;

            //Act
            await _rideFacadeSut.SaveAsync(ride);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var rideFromDb = await dbxAssert.Rides
                .Include(i => i.Car)
                .SingleAsync(i => i.Id == ride.Id);

            DeepAssert.Equal(CarSeeds.Hyundai with {Owner = null}, rideFromDb.Car);
        }


        [Fact]
        public async Task InsertOrUpdate_AddNewParticipantsToUpdateRide()
        {
            //Arrange
            var newParticipant = new ParticipantModel(
                UserId: UserSeeds.Leonardo.Id,
                UserName: UserSeeds.Leonardo.Name,
                UserSurname: UserSeeds.Leonardo.Surname,
                UserRating: UserSeeds.Leonardo.Rating
            );

            var ride = await _rideFacadeSut.GetAsync(RideSeeds.UpdateRide.Id);

            Assert.NotNull(ride); //Just for safety
            ride.Participants.Add(newParticipant);

            //Act
            await _rideFacadeSut.SaveAsync(ride);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var rideFromDb = await dbxAssert.Rides
                .Include(i => i.Participants)
                .ThenInclude(i => i.User)
                .SingleAsync(i => i.Id == ride.Id);

            var participantsFromDb = Mapper.Map<RideDetailModel>(rideFromDb).Participants;
            Assert.Contains(participantsFromDb, i => i.UserId == newParticipant.UserId);
        }


        [Fact]
        public async Task InsertOrUpdate_RemoveParticipantOfUpdateRide()
        {
            //Arrange
            var ride = await _rideFacadeSut.GetAsync(RideSeeds.Ride1.Id);

            Assert.NotNull(ride); //Just for safety
            Assert.NotEmpty(ride.Participants);
            var toBeDeleted = ride.Participants[0];

            ride.Participants.Remove(toBeDeleted);

            //Act
            await _rideFacadeSut.SaveAsync(ride);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var rideFromDb = await dbxAssert.Rides
                .Include(i => i.Participants)
                .ThenInclude(i => i.User)
                .SingleAsync(i => i.Id == ride.Id);

            Assert.DoesNotContain(rideFromDb.Participants, i => i.UserId == toBeDeleted.UserId);
        }


        [Fact]
        public async Task InsertOrUpdate_RemoveAllParticipantsOfUpdateRide()
        {
            //Arrange
            var ride = await _rideFacadeSut.GetAsync(RideSeeds.Ride1.Id);

            Assert.NotNull(ride); //Just for safety
            Assert.NotEmpty(ride.Participants);

            ride.Participants.Clear();

            //Act
            await _rideFacadeSut.SaveAsync(ride);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var rideFromDb = await dbxAssert.Rides
                .Include(i => i.Participants)
                .ThenInclude(i => i.User)
                .SingleAsync(i => i.Id == ride.Id);

            Assert.False(rideFromDb.Participants.Any());
        }


        [Fact]
        public async Task InsertOrUpdate_UpdateHasUserAttribute()
        {
            //Arrange
            var ride = await _rideFacadeSut.GetAsync(RideSeeds.Ride1.Id);

            Assert.NotNull(ride); //Just for safety
            Assert.NotEmpty(ride.Participants);
            Assert.False(ride.Participants[0].HasUserRated);
            ride.Participants[0].HasUserRated = true;

            //Act
            await _rideFacadeSut.SaveAsync(ride);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var rideFromDb = await dbxAssert.Rides
                .Include(i => i.Participants)
                .ThenInclude(i => i.User)
                .SingleAsync(i => i.Id == ride.Id);

            var participantsFromDb = Mapper.Map<RideDetailModel>(rideFromDb).Participants;
            Assert.True(participantsFromDb[0].HasUserRated);
        }


        [Fact]
        public async Task DeleteById_SeededDeleteRide()
        {
            //Act
            await _rideFacadeSut.DeleteAsync(RideSeeds.DeleteRide.Id);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            Assert.False(await dbxAssert.Rides.AnyAsync(i => i.Id == RideSeeds.DeleteRide.Id));
        }
    }
}
