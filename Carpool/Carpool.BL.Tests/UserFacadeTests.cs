using System;
using System.Linq;
using System.Threading.Tasks;
using Carpool.BL.Facades;
using Carpool.BL.Models;
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
                Name: UserSeeds.DeleteLeonardo.Name + " updated",
                Surname: UserSeeds.DeleteLeonardo.Surname + " updated",
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

    }
}
