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
    public class UserFacadeTests : CRUDFacadeTestsBase
    {
        private readonly UserFacade _userFacadeSut;

        public UserFacadeTests(ITestOutputHelper output) : base(output)
        {
            _userFacadeSut = new UserFacade(UnitOfWorkFactory, Mapper);
        }

        [Fact]
        public async Task Create_NewUser_DoesNotThrow()
        {
            //Arrange
            var model = new UserDetailModel
            (
                Name: @"Jan",
                Surname: @"Novák",
                PhotoUrl: @"sampleUrl",
                Country: @"Czech Republic",
                Rating: 0
            );

            //Act
            var _ = await _userFacadeSut.SaveAsync(model);
        }

        [Fact]
        public async Task GetAll_SeededChuck()
        {
            //Arrange
            var users = await _userFacadeSut.GetAsync();

            //Act
            var user = users.Single(i => i.Id == UserSeeds.Chuck.Id);

            //Assert
            var expectedModel = Mapper.Map<UserListModel>(UserSeeds.Chuck);
            DeepAssert.Equal(expectedModel,user);
        }

        [Fact]
        public async Task GetById_SeededChuck()
        {
            //Act
            var user = await _userFacadeSut.GetAsync(UserSeeds.Chuck.Id);

            //Assert
            var expectedModel = Mapper.Map<UserDetailModel>(UserSeeds.Chuck);
            DeepAssert.Equal(expectedModel, user);
        }

        [Fact]
        public async Task GetById_NonExistentUser()
        {
            //Act
            var user = await _userFacadeSut.GetAsync(UserSeeds.EmptyUser.Id);

            //Assert
            Assert.Null(user);
        }

        [Fact]
        public async Task DeleteById_SeededDeleteLeonardo()
        {
            //Act
            await _userFacadeSut.DeleteAsync(UserSeeds.DeleteLeonardo.Id);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            Assert.False(await dbxAssert.Users.AnyAsync(i => i.Id == UserSeeds.DeleteLeonardo.Id)); 
        }

        [Fact]
        public async Task InsertOrUpdate_UpdateLeonardo()
        {
            //Arrange
            var user = new UserDetailModel(
                Name: UserSeeds.UpdateLeonardo.Name + " updated",
                Surname: UserSeeds.UpdateLeonardo.Surname + " updated",
                PhotoUrl: @"updated photoUrl",
                Country: UserSeeds.UpdateLeonardo.Country,
                Rating: UserSeeds.UpdateLeonardo.Rating + 10
            )
            {
                Id = UserSeeds.UpdateLeonardo.Id
            };

            //Act
            await _userFacadeSut.SaveAsync(user);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var userFromDb = await dbxAssert.Users.SingleAsync(i => i.Id == user.Id);
            DeepAssert.Equal(user, Mapper.Map<UserDetailModel>(userFromDb));
        }


        [Fact]
        public async Task InsertOrUpdate_NewUserAdded()
        {
            //Arrange
            var user = new UserDetailModel(
                Name: @"Justin",
                Surname: @"Bieber"
            );

            //Act
            user = await _userFacadeSut.SaveAsync(user);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var userFromDb = await dbxAssert.Users.SingleAsync(i => i.Id == user.Id);
            DeepAssert.Equal(user, Mapper.Map<UserDetailModel>(userFromDb));
        }

        [Fact]
        public async Task InsertOrUpdate_UpdateCarsOfUpdateLeonardo()
        {
            //Arrange
            var newCar = new CarDetailModel(
                Name: @"New car",
                Brand: @"BMW",
                Type: CarType.Pickup,
                Registration: new DateTime(1999, 12, 1),
                Seats: 4
            );

            var user = Mapper.Map<UserDetailModel>(UserSeeds.UpdateLeonardo);
            user.Cars.Add(newCar);

            //Act
            await _userFacadeSut.SaveAsync(user);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var userFromDb = await dbxAssert.Users
                .Include(i => i.Cars)
                .SingleAsync(i => i.Id == user.Id);

            Assert.Contains(
                Mapper.Map<UserDetailModel>(userFromDb).Cars, 
                i => i.Name == newCar.Name && 
                     i.Brand == newCar.Brand && 
                     i.Registration == newCar.Registration);
        }

        [Fact]
        public async Task InsertOrUpdate_UpdateCarsOfUpdateLeonardoIncludingPhoto()
        {
            //Arrange
            var newCar = new CarDetailModel(
                Name: @"New car",
                Brand: @"BMW",
                Type: CarType.Pickup,
                Registration: new DateTime(1999, 12, 1),
                Seats: 4
            )
            {
                Photos = {
                    new CarPhotoModel(@"Testing\url"),
                    new CarPhotoModel(@"Testing\url2")
                }
            };

            var user = Mapper.Map<UserDetailModel>(UserSeeds.UpdateLeonardo);
            user.Cars.Add(newCar);

            //Act
            await _userFacadeSut.SaveAsync(user);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var userFromDb = await dbxAssert.Users
                .Include(i => i.Cars)
                .ThenInclude(i => i.Photos)
                .SingleAsync(i => i.Id == user.Id);

            var carFromDb = Mapper.Map<CarDetailModel>(userFromDb.Cars.First(i => i.Name == newCar.Name));

            Assert.Contains(carFromDb.Photos, i => i.Url == newCar.Photos[0].Url);
            Assert.Contains(carFromDb.Photos, i => i.Url == newCar.Photos[1].Url);
        }
    }
}
