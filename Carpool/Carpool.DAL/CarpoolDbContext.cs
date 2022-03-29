using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Carpool.DAL
{
    public class CarpoolDbContext : DbContext
    {
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<RideEntity> Rides => Set<RideEntity>();
        public DbSet<CarEntity> Cars => Set<CarEntity>();
        public DbSet<PhotoEntity> Photos => Set<PhotoEntity>();
    }
}
