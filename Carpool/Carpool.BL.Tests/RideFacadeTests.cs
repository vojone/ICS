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


        //Test of filtering of rides by arrival location
        [Fact]
        public async Task FilterByArrivalLocation_SeededRide()
        {
            //Act
            var rides = await _rideFacadeSut
                .FilterAsync(arrivalLoc: RideSeeds.Ride1.ArrivalL);

            //Assert
            var rideListModels = rides as RideListModel[] ?? rides.ToArray();
            Assert.All(rideListModels, ride =>
                Assert.Equal(RideSeeds.Ride1.ArrivalL, ride.ArrivalL)
            );

            Assert.NotEmpty(rideListModels);
        }


        //Test of filtering of rides by departure time
        [Fact]
        public async Task FilterByDepartureTime_SeededRide()
        {
            //Act
            var rides = await _rideFacadeSut
                .FilterAsync(departureTime: RideSeeds.Ride2.DepartureT);

            //Assert
            var rideListModels = rides as RideListModel[] ?? rides.ToArray();
            Assert.All(rideListModels, ride =>
                Assert.Equal(RideSeeds.Ride2.DepartureT, ride.DepartureT)
            );

            Assert.NotEmpty(rideListModels);
        }


        //Test of filtering by dep. time and by availability
        [Fact]
        public async Task FilterByDepartureTimeAndByAvailability_SeededRide()
        {
            //Act
            var rides = await _rideFacadeSut
                .FilterAsync(departureTime: RideSeeds.Ride2.DepartureT, 
                             mustBeAvailable: true);

            //Assert
            var rideListModels = rides as RideListModel[] ?? rides.ToArray();
            Assert.All(rideListModels, ride =>
                {
                    Assert.Equal(RideSeeds.Ride2.DepartureT, ride.DepartureT);
                    Assert.True(ride.Capacity > 0);
                }
            );

            Assert.NotEmpty(rideListModels);
        }


        //Test of filtering by multiple factors
        [Fact]
        public async Task FilterByDepartureArrivalTimeAndDepartureArrivalLoc_SeededRide()
        {
            //Act
            var rides = await _rideFacadeSut
                .FilterAsync(departureTime: RideSeeds.Ride2.DepartureT, 
                             arrivalTime: RideSeeds.Ride2.ArrivalT,
                             departureLoc: RideSeeds.Ride2.DepartureL, 
                             arrivalLoc: RideSeeds.Ride2.ArrivalL);

            //Assert
            var rideListModels = rides as RideListModel[] ?? rides.ToArray();
            Assert.All(rideListModels, ride =>
                {
                    Assert.Equal(RideSeeds.Ride2.DepartureT, ride.DepartureT);
                    Assert.Equal(RideSeeds.Ride2.ArrivalT, ride.ArrivalT);
                    Assert.Equal(RideSeeds.Ride2.DepartureL, ride.DepartureL);
                    Assert.Equal(RideSeeds.Ride2.ArrivalL, ride.ArrivalL);
                }
            );

            Assert.NotEmpty(rideListModels);
        }


        //Test of filtering of rides by specific driver
        [Fact]
        public async Task GetByDriverId_SeededRide()
        {

            //Act
            var rides = await _rideFacadeSut
                .GetByDriverIdAsync(RideSeeds.Ride1.DriverId);

            //Assert
            var rideListModels = rides as RideListModel[] ?? rides.ToArray();
            Assert.All(rideListModels, ride =>
                Assert.Equal(RideSeeds.Ride1.DriverId, ride.DriverId)
            );

            Assert.NotEmpty(rideListModels);
        }


        //Test of filtering of rides by specific participant
        [Fact]
        public async Task GetBySpecificParticipant_SeededRide()
        {
            //Act
            var rides = await _rideFacadeSut
                .GetByParticipantIdAsync(UserSeeds.Jack.Id);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();

            var rideListModels = rides as RideListModel[] ?? rides.ToArray();
            Assert.All(rideListModels, ride =>
                Assert.NotEmpty(dbxAssert.Participants
                    .Where(p => 
                        p.RideId == ride.Id && 
                        p.UserId == UserSeeds.Jack.Id))
            );

            Assert.NotEmpty(rideListModels);
        }


        [Fact]
        public async Task GetByDepartureLoc_NonExistingRide()
        {
            //Act
            var rides = await _rideFacadeSut
                .FilterAsync(departureLoc: "NonExistingPlaceInFarFarGalaxy$");


            //Assert
            Assert.Empty(rides);
        }
        

        [Fact]
        public async Task InsertOrUpdate_NewRideWithoutDriverRideCollisions_NotThrows()
        {
            //Arrange
            var ride = new RideDetailModel(
                DepartureL: "Pardubice",
                ArrivalL: "Hradec",
                DepartureT: new DateTime(2023, 8, 6, 12, 0, 0),
                ArrivalT: new DateTime(2023, 8, 6, 14, 0, 0),
                InitialCapacity: 4,
                Capacity: 4,
                State: RideState.Planned,
                CarId: CarSeeds.Kia.Id,
                DriverId: UserSeeds.Chuck.Id
            );

            //Act
            await _rideFacadeSut.SaveAsync(ride);
        }


        [Fact]
        public async Task InsertOrUpdate_NewRideWithDriverCollisions_Throws()
        {
            //Arrange
            var ride = new RideDetailModel(
                DepartureL: "Pardubice",
                ArrivalL: "Hradec",
                DepartureT: RideSeeds.Ride1.DepartureT,
                ArrivalT: RideSeeds.Ride1.ArrivalT,
                InitialCapacity: 4,
                Capacity: 4,
                State: RideState.Planned,
                CarId: CarSeeds.Kia.Id,
                DriverId: UserSeeds.Chuck.Id
            );

            _ = await Assert.ThrowsAsync<DbUpdateException>(
                async () => await _rideFacadeSut.SaveAsync(ride)
            );
        }


        [Fact]
        public async Task InsertOrUpdate_NewRideWithoutParticipantRideCollisions_NotThrows()
        {
            //Arrange
            var ride = new RideDetailModel(
                DepartureL: "Pardubice",
                ArrivalL: "Hradec",
                DepartureT: new DateTime(2023, 8, 6, 12, 0, 0),
                ArrivalT: new DateTime(2023, 8, 6, 14, 0, 0),
                InitialCapacity: 4,
                Capacity: 4,
                State: RideState.Planned,
                CarId: CarSeeds.Kia.Id,
                DriverId: UserSeeds.Chuck.Id
            )
            {
                Participants =
                {
                    new ParticipantModel(
                        UserId: UserSeeds.Jack.Id,
                        UserName: UserSeeds.Jack.Name,
                        UserSurname: UserSeeds.Jack.Surname,
                        UserRating: UserSeeds.Jack.Rating
                    ),
                }
            };

            //Act
            await _rideFacadeSut.SaveAsync(ride);
        }


        [Fact]
        public async Task InsertOrUpdate_NewRideWithParticipantRideCollisions_Throws()
        {
            //Arrange
            var ride = new RideDetailModel(
                DepartureL: "Pardubice",
                ArrivalL: "Hradec",
                DepartureT: RideSeeds.Ride1.DepartureT,
                ArrivalT: RideSeeds.Ride1.ArrivalT,
                InitialCapacity: 4,
                Capacity: 4,
                State: RideState.Planned,
                CarId: CarSeeds.Hyundai.Id,
                DriverId: UserSeeds.UpdateChuck.Id
            )
            {
                Participants =
                {
                    new ParticipantModel(
                        UserId: UserSeeds.Jack.Id,
                        UserName: UserSeeds.Jack.Name,
                        UserSurname: UserSeeds.Jack.Surname,
                        UserRating: UserSeeds.Jack.Rating
                    ),
                }
            };

            _ = await Assert.ThrowsAsync<DbUpdateException>(
                async () => 
                    
                    await _rideFacadeSut.SaveAsync(ride)
            );
        }


        [Fact]
        public async Task InsertOrUpdate_SameDriverAsParticipant_Throws()
        {
            //Arrange
            var ride = new RideDetailModel(
                DepartureL: "Pardubice",
                ArrivalL: "Hradec",
                DepartureT: new DateTime(2023, 8, 6, 12, 0, 0),
                ArrivalT: new DateTime(2023, 8, 6, 14, 0, 0),
                InitialCapacity: 4,
                Capacity: 4,
                State: RideState.Planned,
                CarId: CarSeeds.Kia.Id,
                DriverId: UserSeeds.Chuck.Id
            )
            {
                Participants =
                {
                    new ParticipantModel(
                        UserId: UserSeeds.Chuck.Id,
                        UserName: UserSeeds.Chuck.Name,
                        UserSurname: UserSeeds.Chuck.Surname,
                        UserRating: UserSeeds.Chuck.Rating
                    ),
                }
            };

            _ = await Assert.ThrowsAsync<DbUpdateException>(
                async () =>

                    await _rideFacadeSut.SaveAsync(ride)
            );
        }


        [Fact]
        public async Task InsertOrUpdate_UpdateRide()
        {
            //Arrange
            var ride = await _rideFacadeSut.GetAsync(RideSeeds.UpdateRide.Id);

            Assert.NotNull(ride);
            if (ride != null)
            {
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
        }


        [Fact]
        public async Task InsertOrUpdate_UpdateCarOfUpdateRide()
        {
            //Arrange
            var ride = await _rideFacadeSut.GetAsync(RideSeeds.UpdateRide.Id);

            Assert.NotNull(ride);
            if (ride != null)
            {
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
            if (ride != null)
            {
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
        }


        [Fact]
        public async Task InsertOrUpdate_RemoveParticipantOfUpdateRide()
        {
            //Arrange
            var ride = await _rideFacadeSut.GetAsync(RideSeeds.Ride1.Id);

            Assert.NotNull(ride); //Just for safety

            if (ride != null)
            {
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
        }


        [Fact]
        public async Task InsertOrUpdate_RemoveAllParticipantsOfUpdateRide()
        {
            //Arrange
            var ride = await _rideFacadeSut.GetAsync(RideSeeds.Ride1.Id);

            Assert.NotNull(ride); //Just for safety
            if (ride != null)
            {
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
        }


        [Fact]
        public async Task InsertOrUpdate_UpdateHasUserRatedAttribute()
        {
            //Arrange
            var ride = await _rideFacadeSut.GetAsync(RideSeeds.Ride1.Id);

            Assert.NotNull(ride); //Just for safety
            if (ride != null)
            {
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
