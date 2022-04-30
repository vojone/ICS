using System;
using System.Linq;
using System.Threading.Tasks;
using Carpool.Common;
using Carpool.Common.Tests;
using Carpool.DAL.Entities;
using Carpool.DAL.Seeds;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace Carpool.DAL.Tests
{
    // Contains only basic use cases to ensure that DB context is well configured
    public class DbContextCarsTests : DbContextTestsBase
    {
        public DbContextCarsTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task GetExisting_CarId()
        {
            //Act
            var entity = await CarpoolDbContextSut.Cars
                .SingleAsync(i => i.Id == CarSeeds.Hyundai.Id);

            //Assert
            DeepAssert.Equal(CarSeeds.Hyundai.Id, entity.Id);
        }

        [Fact]
        public async Task AddNew_Car_without_Photo_Persisted()
        {
            //Arrange
            CarEntity entity = new(
                Id:Guid.Parse("142C54E5-B10F-4956-AB0B-B80007670E3C"),
                Name: "Octavia",
                Brand: "Skoda",
                Photo: null,
                Type: CarType.Sport,
                Registration: DateTime.MinValue,
                Seats: 4,
                OwnerId: UserSeeds.Chuck.Id
            );

            //Act
            CarpoolDbContextSut.Cars.Add(entity);
            await CarpoolDbContextSut.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntities = await dbx.Cars.SingleAsync(i => i.Id == entity.Id);
            DeepAssert.Equal(entity, actualEntities);
        }
        
        [Fact]
        public async Task AddNew_Car_Persisted()
        {
            //Arrange
            CarEntity entity = new(
                Id:Guid.Parse("122C54E5-B10F-4956-AB0B-B80007670E3C"),
                Name: "Rapid",
                Brand: "Skoda",
                Photo: @"New_photo_1_of\update\skoda\URL.png",
                Type: CarType.Sport,
                Registration: DateTime.MinValue,
                Seats: 4,
                OwnerId: UserSeeds.Chuck.Id
            );

            //Act
            CarpoolDbContextSut.Cars.Add(entity);
            await CarpoolDbContextSut.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntities = await dbx.Cars.SingleAsync(i => i.Id == entity.Id);
            DeepAssert.Equal(entity, actualEntities);
        }

        [Fact]
        public async Task Update_UpdateKia()
        {
            var baseEntity = CarSeeds.UpdateKia;
            //Arrange
            CarEntity entity = baseEntity with
            {
                Name = CarSeeds.UpdateKia.Name + " updated",
                Brand = CarSeeds.UpdateKia.Brand + " updated",
                Owner = null
            };

            //Act
            CarpoolDbContextSut.Cars.Update(entity);
            await CarpoolDbContextSut.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntities = await dbx.Cars.SingleAsync(i => i.Id == entity.Id);
            DeepAssert.Equal(entity, actualEntities);
        }


        [Fact]
        public async Task Delete_Car()
        {
            //Arrange
            var entityBase = CarSeeds.DeleteKia;
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var entity = await dbx.Cars.SingleAsync(i => i.Id == entityBase.Id);

            //Act
            Assert.NotNull(entity);
            dbx.Cars.Remove(entity);
            await dbx.SaveChangesAsync();

            //Assert
            Assert.False(await dbx.Cars.AnyAsync(i => i.Id == entityBase.Id));

            //Photo should be deleted with car
            Assert.False(await dbx.CarPhotos.AnyAsync(i => i.CarId == entityBase.Id));

            //User should still exists after deletion of his car
            Assert.True(await dbx.Users.AnyAsync(i => i.Id == entityBase.OwnerId));
        }

        [Fact]
        public async Task Delete_CarPhoto()
        {
            //Arrange
            var entityBase = CarPhotoSeeds.DeleteCarPhoto;
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var entity = await dbx.CarPhotos.SingleAsync(i => i.Id == entityBase.Id);

            //Act
            Assert.NotNull(entity);
            dbx.CarPhotos.Remove(entity);
            await dbx.SaveChangesAsync();

            //Assert
            Assert.False(await dbx.CarPhotos.AnyAsync(i => i.Id == entityBase.Id));


            //Car should still exists after deletion of its photo
            Assert.True(await dbx.Cars.AnyAsync(i => i.Id == entityBase.CarId));
        }
    }
}

