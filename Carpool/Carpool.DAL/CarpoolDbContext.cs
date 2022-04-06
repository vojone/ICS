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
        public DbSet<CarPhotoEntity> CarPhotos => Set<CarPhotoEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<UserEntity>()
                .HasMany(i => i.Rides)
                .WithOne(i => i.User)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<RideEntity>()
                .HasMany(i => i.Participants)
                .WithOne(i => i.Ride)
                .HasForeignKey(i => i.RideId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<RideEntity>()
                .HasOne(i => i.Car)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<RideEntity>()
                .HasOne(i => i.Driver)
                .WithMany(i => i.DrivingRides)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<UserEntity>()
                .HasMany(i => i.Cars)
                .WithOne(i => i.Owner)
                .HasForeignKey(i => i.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<CarEntity>()
                .HasMany(i => i.Photos)
                .WithOne(i => i.Car)
                .HasForeignKey(i => i.CarId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CarEntity>()
                .Property(i => i.Registration)
                .HasConversion(
                    v => v.ToString("dd/MM/yyyy"),
                    v => DateOnly.Parse(v));

            if (!_seedDemoData) return;

            UserSeeds.Seed(modelBuilder);

            CarSeeds.Seed(modelBuilder);
            CarPhotoSeeds.Seed(modelBuilder);
            
            RideSeeds.Seed(modelBuilder);

            ParticipantSeeds.Seed(modelBuilder);
        }

    }
}
