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
            Id: Guid.Parse("522CFAEC-544C-47FE-882F-E6DBD7CF3AA7"),
            UserId: UserSeeds.Obiwan.Id,
            RideId: RideSeeds.Ride1.Id,
            HasUserRated: false
        )
        {
            User = UserSeeds.Obiwan,
            Ride = RideSeeds.Ride1
        };

        public static readonly ParticipantEntity DeleteParticipant1 = new(
            Id: Guid.Parse("D7844789-DEFE-460B-8E2B-04D42EC4F266"),
            UserId: UserSeeds.Jack.Id,
            RideId: RideSeeds.Ride2.Id,
            HasUserRated: false
        )
        {
            User = UserSeeds.Jack,
            Ride = RideSeeds.Ride2
        };

        public static readonly ParticipantEntity UpdateParticipant = new(
            Id: Guid.Parse("B253D9AD-0DB7-4700-AB74-382C2E95A5C7"),
            UserId: UserSeeds.DeleteLeonardo.Id,
            RideId: RideSeeds.DeleteRide.Id,
            HasUserRated: false
        )
        {
            User = UserSeeds.DeleteLeonardo,
            Ride = RideSeeds.DeleteRide
        };


        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParticipantEntity>().HasData(
                Participant1 with { User = null, Ride = null },
                DeleteParticipant1 with { User = null, Ride = null },
                Participant2 with { User = null, Ride = null },
                UpdateParticipant with { User = null, Ride = null }
            );
        }
    }
}
