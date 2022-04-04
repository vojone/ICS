using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Carpool.DAL.Seeds
{
    public static class ParticipantSeeds
    {
        public static readonly ParticipantEntity Participant1 = new(
            Id: Guid.Parse("D3EA65A8-0F71-4C6D-A893-0763E8D4C533"),
            UserId: UserSeeds.Jack.Id,
            RideId: RideSeeds.Ride1.Id,
            HasUserRated: false
        )
        {
            User = UserSeeds.Jack,
            Ride = RideSeeds.Ride1
        };

        public static readonly ParticipantEntity Participant2 = new(
            Id: Guid.Parse("D733D8FC-427A-4A86-AC08-D70D8AEDD6E5"),
            UserId: UserSeeds.Obiwan.Id,
            RideId: RideSeeds.Ride1.Id,
            HasUserRated: false
        )
        {
            User = UserSeeds.Obiwan,
            Ride = RideSeeds.Ride1
        };

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParticipantEntity>().HasData(
                Participant1 with { User = null, Ride = null},
                Participant2 with { User = null, Ride = null }
            );
        }
    }
}
