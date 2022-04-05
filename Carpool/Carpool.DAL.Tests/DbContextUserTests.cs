using System;
using System.Linq;
using System.Threading.Tasks;
using Carpool.DAL.Entities;
using Carpool.DAL.Seeds;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace Carpool.DAL.Tests
{
    // Contains only basic use cases to ensure that DB context is well configured
    public class DbContextUserTests : DbContextTestsBase
    {
        public DbContextUserTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task GetExisting_ChuckId()
        {
            //Act
            var entity = await CarpoolDbContextSut.Users
                .SingleAsync(i => i.Id == UserSeeds.Chuck.Id);

            //Assert
            DeepAssert.Equal(UserSeeds.Chuck.Id, entity.Id);
        }

        [Fact]
        public async Task GetExisting_ChucksPhoto()
        {
            //Act
            var entity = await CarpoolDbContextSut.Users
                .SingleAsync(i => i.Id == UserSeeds.Chuck.Id);
            /*.Include(i => i.PhotoUrl)
            .SingleAsync(i => i.Id == UserSeeds.ChuckWithPhoto.Id);*/

            //Assert
            DeepAssert.Equal(UserSeeds.Chuck.PhotoUrl, entity.PhotoUrl);
        }


        [Fact]
        public async Task GetExisting_ChuckWithCars()
        {
            //Act
            var entity = await CarpoolDbContextSut.Users
                .Include(i => i.Cars)
                .SingleAsync(i => i.Id == UserSeeds.Chuck.Id);

            //Assert
            DeepAssert.Equal(UserSeeds.Chuck.Cars, entity.Cars);
        }

        [Fact]
        public async Task GetExisting_ObiwansRides()
        {
            //Act
            var entities = await CarpoolDbContextSut.Participants
                .Where(i => i.UserId == UserSeeds.Obiwan.Id)
                .ToArrayAsync();


            //Assert
            Assert.Contains(ParticipantSeeds.Participant2 with {Ride = null, User = null}, entities);
        }

        [Fact]
        public async Task AddNew_User_Persisted()
        {
            //Arrange
            UserEntity entity = new(
                Guid.Parse("C5DE45D7-64A0-4E8D-AC7F-BF5CFDFB0EFC"),
                "Tom",
                "Cruise",
                null,
                "USA",
                0
            );

            //Act
            CarpoolDbContextSut.Users.Add(entity);
            await CarpoolDbContextSut.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntities = await dbx.Users.SingleAsync(i => i.Id == entity.Id);
            DeepAssert.Equal(entity, actualEntities);
        }

        [Fact]
        public async Task Update_User_Persisted()
        {
            //Arrange
            UserEntity baseEntity = UserSeeds.UpdateLeonardo;

            UserEntity updatedEntity =
                baseEntity with
                {
                    Name = "Godtier Leonardo",
                    Rating = UInt32.MaxValue
                };

            //Act
            CarpoolDbContextSut.Users.Update(updatedEntity);
            await CarpoolDbContextSut.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Users.SingleAsync(i => i.Id == updatedEntity.Id);
            DeepAssert.Equal(updatedEntity, actualEntity);
        }

        [Fact]
        public async Task Delete_User_ChuckDelete()
        {
            //Arrange
            UserEntity toBeDeleted = UserSeeds.DeleteLeonardo;

            //Act
            CarpoolDbContextSut.Users.Remove(toBeDeleted);
            await CarpoolDbContextSut.SaveChangesAsync();

            //Assert
            Assert.False(await CarpoolDbContextSut.Users.AnyAsync(i => i.Id == toBeDeleted.Id));
        }
    }
}

