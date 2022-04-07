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
    public class DbContextParticipantTests : DbContextTestsBase
    {
        public DbContextParticipantTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task GetExisting_Participant()
        {
            //Act
            var entities = await CarpoolDbContextSut.Participants
                .ToArrayAsync();

            //Assert
            Assert.Contains(ParticipantSeeds.Participant1 with { User = null, Ride = null }, entities);
            Assert.Contains(ParticipantSeeds.Participant2 with { User = null, Ride = null }, entities);
        }

        [Fact]
        public async Task AddNew_Participant_Persisted()
        {
            //Arrange
            ParticipantEntity entity = new(
                Guid.Parse("D00BE6B8-CBC1-4AA9-875B-F4291B5A1272"),
                UserSeeds.Leonardo.Id,
                RideSeeds.Ride1.Id,
                true
            );

            //Act
            CarpoolDbContextSut.Participants.Add(entity);
            await CarpoolDbContextSut.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntities = await dbx.Participants.SingleAsync(i => i.Id == entity.Id);
            DeepAssert.Equal(entity, actualEntities);
        }

        [Fact]
        public async Task Delete_Participant()
        {
            //Arrange
            var entityBase = ParticipantSeeds.DeleteParticipant1;
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var entity = await dbx.Participants
                .SingleAsync(i => i.Id == entityBase.Id);


            //Act
            Assert.NotNull(entity);
            dbx.Participants.Remove(entity);
            await dbx.SaveChangesAsync();

            //Assert
            Assert.False(await dbx.Participants.AnyAsync(i => i.Id == entityBase.Id));

            //Ride and user should still exists after deletion of his participation
            Assert.True(await dbx.Users.AnyAsync(i => i.Id == entityBase.UserId));
            Assert.True(await dbx.Rides.AnyAsync(i => i.Id == entityBase.RideId));
        }

        [Fact]
        public async Task Update_Participant()
        {
            //Arrange
            var entityBase = ParticipantSeeds.UpdateParticipant;
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var entity = await CarpoolDbContextSut.Participants
                .SingleAsync(i => i.Id == entityBase.Id);

            //Act
            Assert.NotNull(entity);

            var updatedEntity = entity with
            {
                HasUserRated = true
            };

            dbx.Participants.Update(updatedEntity);
            await dbx.SaveChangesAsync();

            //Arrange
            var actualEntity = await dbx.Participants.SingleAsync(i => i.Id == entity.Id);
            DeepAssert.Equal(updatedEntity, actualEntity);
        }
    }
}

