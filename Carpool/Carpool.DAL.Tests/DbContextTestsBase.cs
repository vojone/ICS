using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.DAL.Tests.Converters;
using Carpool.DAL.Tests.Factories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace Carpool.DAL.Tests
{
    //Taken from sample project 'CookBook'
    public class DbContextTestsBase : IAsyncLifetime
    {
        protected DbContextTestsBase(ITestOutputHelper output)
        {
            XUnitTestOutputConverter converter = new(output);
            Console.SetOut(converter);

            DbContextFactory = new DbContextSQLiteTestingFactory(GetType().FullName!, seedTestingData: true);

            CarpoolDbContextSut = DbContextFactory.CreateDbContext();
        }

        protected IDbContextFactory<CarpoolDbContext> DbContextFactory { get; }
        protected CarpoolDbContext CarpoolDbContextSut { get; }


        public async Task InitializeAsync()
        {
            await CarpoolDbContextSut.Database.EnsureDeletedAsync();
            await CarpoolDbContextSut.Database.EnsureCreatedAsync();
        }

        public async Task DisposeAsync()
        {
            await CarpoolDbContextSut.Database.EnsureDeletedAsync();
            await CarpoolDbContextSut.DisposeAsync();
        }
    }
}
