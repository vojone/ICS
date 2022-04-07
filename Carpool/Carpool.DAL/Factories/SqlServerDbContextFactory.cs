using Microsoft.EntityFrameworkCore;

namespace Carpool.DAL.Factories
{
    public class SqlServerDbContextFactory : IDbContextFactory<CarpoolDbContext>
    {
        private readonly string _connectionString;
        private readonly bool _seedDemoData;

        public SqlServerDbContextFactory(string connectionString, bool seedDemoData = false)
        {
            _connectionString = connectionString;
            _seedDemoData = seedDemoData;
        }

        public CarpoolDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CarpoolDbContext>();
            optionsBuilder.UseSqlServer(_connectionString);

            //optionsBuilder.LogTo(System.Console.WriteLine); //Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
            //optionsBuilder.EnableSensitiveDataLogging();

            return new CarpoolDbContext(optionsBuilder.Options, _seedDemoData);
        }
    }
}
