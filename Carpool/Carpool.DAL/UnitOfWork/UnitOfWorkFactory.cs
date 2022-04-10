using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Carpool.DAL.UnitOfWork
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IDbContextFactory<CarpoolDbContext> _dbContextFactory;

        public UnitOfWorkFactory(IDbContextFactory<CarpoolDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public IUnitOfWork Create() => new UnitOfWork(_dbContextFactory.CreateDbContext());
    }
}
