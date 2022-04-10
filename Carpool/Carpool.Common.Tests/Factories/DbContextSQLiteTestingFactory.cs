using Carpool.DAL;
using Microsoft.EntityFrameworkCore;

namespace Carpool.Common.Tests.Factories
{
    //Taken from sample project 'CookBook'
    public class DbContextSQLiteTestingFactory : IDbContextFactory<CarpoolDbContext>
    {
        private readonly string _databaseName;
        private readonly bool _seedTestingData;

        public DbContextSQLiteTestingFactory(string databaseName, bool seedTestingData = false)
        {
            _databaseName = databaseName;
            _seedTestingData = seedTestingData;
        }
        public CarpoolDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<CarpoolDbContext> builder = new();
            builder.UseSqlite($"Data Source={_databaseName};Cache=Shared");

            return new CarpoolDbContext(builder.Options, _seedTestingData);
        }
    }
}
