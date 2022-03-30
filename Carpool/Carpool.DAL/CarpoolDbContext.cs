using Carpool.DAL.Entities;
using Carpool.DAL.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carpool.DAL
{
    public class CarpoolDbContext : DbContext
    {
        private readonly bool _seedDemoData;

        public CarpoolDbContext(DbContextOptions contextOptions, bool seedDemoData = false) : base(contextOptions)
        {
            _seedDemoData = seedDemoData;
        }

        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<RideEntity> Rides => Set<RideEntity>();
        public DbSet<CarEntity> Cars => Set<CarEntity>();
        public DbSet<ParticipantEntity> Participants => Set<ParticipantEntity>();
        public DbSet<PhotoEntity> Photos => Set<PhotoEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RideEntity>()
                .HasOne(i => i.ArrivalL)
                .WithOne();

            modelBuilder.Entity<RideEntity>()
                .HasOne(i => i.DepartureL)
                .WithOne();

            if (!_seedDemoData) return;

            LocationSeeds.Seed(modelBuilder);
            PhotoSeeds.Seed(modelBuilder);
            CarSeeds.Seed(modelBuilder);
            UserSeeds.Seed(modelBuilder);
            RideSeeds.Seed(modelBuilder);
            ParticipantSeeds.Seed(modelBuilder);
        }

    }
}
