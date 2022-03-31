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
            hasUserRated: false
        )
        {
            User = UserSeeds.Jack,
            Ride = RideSeeds.Ride1
        };

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParticipantEntity>().HasData(
                Participant1 with { User = null, Ride = null}
            );
        }
    }
}
