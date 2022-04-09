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
        public async Task Create_NewCar_DoesNotThrow()
        {
            //Arrange
            var carModel = new CarDetailModel(
                Name: @"Passat",
                Brand: @"Volkswagen",
                Type: CarType.SUV,
                Registration: DateOnly.ParseExact("01/01/2020", "dd/MM/yyyy"),
                Seats: 5
            );

            var _ = await _carFacadeSut.SaveAsync(carModel);
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
            var user = await _carFacadeSut.GetAsync(CarSeeds.Hyundai.Id);

            //Assert
            var expectedModel = Mapper.Map<CarDetailModel>(CarSeeds.Hyundai);
            DeepAssert.Equal(expectedModel, user);
        }

        [Fact]
        public async Task GetById_NonExistentCar()
        {
            //Act
            var user = await _carFacadeSut.GetAsync(CarSeeds.EmptyCar.Id);

            //Assert
            Assert.Null(user);
        }

        [Fact]
        public async Task DeleteById_SeededDeleteKia()
        {
            //Act
            await _carFacadeSut.DeleteAsync(CarSeeds.DeleteKia.Id);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            Assert.False(await dbxAssert.Users.AnyAsync(i => i.Id == CarSeeds.DeleteKia.Id));
        }

        [Fact]
        public async Task InsertOrUpdate_UpdateKia()
        {
            //Arrange
            var car = new CarDetailModel(
                Name: CarSeeds.UpdateKia.Name + " updated",
                Brand: CarSeeds.UpdateKia.Brand + " updated",
                Type: CarSeeds.UpdateKia.Type,
                Registration: CarSeeds.UpdateKia.Registration,
                Seats: CarSeeds.UpdateKia.Seats
            )
            {
                Id = CarSeeds.UpdateKia.Id
            };

            //Act
            await _carFacadeSut.SaveAsync(car);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var carFromDb = await dbxAssert.Users.SingleAsync(i => i.Id == car.Id);
            DeepAssert.Equal(car, Mapper.Map<CarDetailModel>(carFromDb));
        }


        [Fact]
        public async Task InsertOrUpdate_NewCarAdded()
        {
            //Arrange
            var car = new CarDetailModel(
                Name: @"Fabia",
                Brand: @"Škoda",
                Type: CarType.Sport,
                Registration: DateOnly.ParseExact("01/01/2020", "dd/MM/yyyy"),
                Seats: 4
            )
            {
                Id = Guid.Parse("A2093221-A59F-42B2-800E-35736ABF88DA")
            };

            //Act
            await _carFacadeSut.SaveAsync(car);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var carFromDb = await dbxAssert.Users.SingleAsync(i => i.Id == car.Id);
            DeepAssert.Equal(car, Mapper.Map<CarDetailModel>(carFromDb));
        }

    }
}
