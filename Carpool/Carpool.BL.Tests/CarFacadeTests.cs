using System;
using System.Linq;
using System.Threading.Tasks;
using Carpool.BL.Facades;
using Carpool.BL.Models;
using Carpool.Common;
using Carpool.Common.Tests;
using Carpool.DAL.Seeds;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace Carpool.BL.Tests
{
    public class CarFacadeTests : CRUDFacadeTestsBase
    {
        private readonly CarFacade _carFacadeSut;

        public CarFacadeTests(ITestOutputHelper output) : base(output)
        {
            _carFacadeSut = new CarFacade(UnitOfWorkFactory, Mapper);
        }

        [Fact]
        public async Task SaveAsync_ThrowsDbUpdateException_WhenCarWithoutOwner()
        {
            //Arrange
            var car = new CarDetailModel(
                Name: @"Fabia",
                Brand: @"Škoda",
                Photo: @"TestUrl",
                Type: CarType.Sport,
                Registration: new DateTime(2005, 5, 4),
                Seats: 4
            );

            //Act & Assert

            //It should throw exception because user is not specified
            //To add car this way parameters must be set correctly
            _ = await Assert.ThrowsAsync<DbUpdateException>(
                async () => await _carFacadeSut.SaveAsync(car)
            );
        
        }

        [Fact]
        public async Task GetAll_SeededKia()
        {
            //Arrange
            var cars = await _carFacadeSut.GetAsync();

            //Act
            var car = cars.Single(i => i.Id == CarSeeds.Kia.Id);

            //Assert
            var expectedModel = Mapper.Map<CarListModel>(CarSeeds.Kia);
            DeepAssert.Equal(expectedModel, car);
        }

        [Fact]
        public async Task GetById_SeededHyundai()
        {
            //Act
            var car = await _carFacadeSut.GetAsync(CarSeeds.Hyundai.Id);

            //Assert
            var expectedModel = Mapper.Map<CarDetailModel>(CarSeeds.Hyundai);
            DeepAssert.Equal(expectedModel, car);
        }

        [Fact]
        public async Task GetById_NonExistentCar()
        {
            //Act
            var car = await _carFacadeSut.GetAsync(CarSeeds.EmptyCar.Id);

            //Assert
            Assert.Null(car);
        }

        [Fact]
        public async Task DeleteById_SeededDeleteKia()
        {
            var toBeDeleted = CarSeeds.DeleteKia;

            //Act
            await _carFacadeSut.DeleteAsync(toBeDeleted.Id);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            Assert.False(await dbxAssert.Cars.AnyAsync(i => i.Id == toBeDeleted.Id));

            Assert.False(await dbxAssert.CarPhotos.AnyAsync(i => i.CarId == toBeDeleted.Id));
        }

        [Fact]
        public async Task InsertOrUpdate_UpdateKia()
        {
            //Arrange
            var car = Mapper.Map<CarDetailModel>(CarSeeds.UpdateKia);
            car.Name += " Updated";
            car.Brand += " Updated";
            car.Seats = 0;

            //Act
            await _carFacadeSut.SaveAsync(car);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var carFromDb = await dbxAssert.Cars
                .Include(i => i.Owner)
                .SingleAsync(i => i.Id == car.Id);
            DeepAssert.Equal(car, Mapper.Map<CarDetailModel>(carFromDb));
        }

       /* [Fact]
        public async Task InsertOrUpdate_UpdateKiaAddNonExistingPhoto_Throws()
        {
            //Arrange
            var car = Mapper.Map<CarDetailModel>(CarSeeds.UpdateKia);

            car.Photos.Add(Mapper.Map<CarPhotoModel>(CarPhotoSeeds.EmptyPhoto));

            //Act & Assert
            _ = await Assert.ThrowsAsync<DbUpdateException>(
                async () => await _carFacadeSut.SaveAsync(car)
            );
        }*/


        /*[Fact]
        public async Task Delete_PhotosOfDeleteKia()
        {
            //Arrange
            var car = Mapper.Map<CarDetailModel>(CarSeeds.DeleteKia);
            Assert.NotEmpty(car.Photos);

            car.Photos.Clear();

            //Act
            await _carFacadeSut.SaveAsync(car);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var carFromDb = await dbxAssert.Cars
                .Include(i => i.Owner)
                .Include(i => i.Photos)
                .SingleAsync(i => i.Id == car.Id);

            DeepAssert.Equal(car, Mapper.Map<CarDetailModel>(carFromDb));
        }*/


        /*[Fact]
        public async Task InsertOrUpdate_NewPhotosOfUpdateKia()
        {
            //Arrange
            const string urlOfNewPhoto1 = @"New_photo_1_of\update\kia\URL.png";
            const string urlOfNewPhoto2 = @"New_photo_2_of\update\kia\URL.jpg";

            var car = Mapper.Map<CarDetailModel>(CarSeeds.UpdateKia);
            car.Photos.Add(new CarPhotoModel(urlOfNewPhoto1));
            car.Photos.Add(new CarPhotoModel(urlOfNewPhoto2));

            //Act
            await _carFacadeSut.SaveAsync(car);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var carFromDb = await dbxAssert.Cars
                .Include(i => i.Owner)
                .Include(i => i.Photos)
                .SingleAsync(i => i.Id == car.Id);

            Assert.Contains(carFromDb.Photos, i => i.Url == urlOfNewPhoto1);
            Assert.Contains(carFromDb.Photos, i => i.Url == urlOfNewPhoto2);
        }*/
    }
}
