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
        public async Task GetExisting_Participants()
        {
            //Act
            var entities = await CarpoolDbContextSut.Participants
                .ToArrayAsync();

            //Assert
            Assert.Contains(ParticipantSeeds.Participant1 with { User = null, Ride = null}, entities);
            Assert.Contains(ParticipantSeeds.Participant2 with { User = null, Ride = null }, entities);
        }

       
    }
}

