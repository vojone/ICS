using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Carpool.DAL.Factories
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CarpoolDbContext>
    {
        public CarpoolDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<CarpoolDbContext> builder = new();
            builder.UseSqlServer(
                @"Data Source=(LocalDB)\MSSQLLocalDB;
                Initial Catalog = Carpool;
                MultipleActiveResultSets = True;
                Integrated Security = True; ");

            return new CarpoolDbContext(builder.Options);
        }
    }
}
