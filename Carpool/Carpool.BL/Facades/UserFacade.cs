using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Carpool.BL.Models;
using Carpool.DAL.Entities;
using Carpool.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Carpool.BL.Facades
{
    public class UserFacade : CRUDFacade<UserEntity, UserListModel, UserDetailModel>
    {
        public UserFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
        {

        }

        public async Task<UserDetailModel?> GetAsyncWithCars(Guid id)
        {
            await using var uow = UnitOfWorkFactory.Create();
            var query = uow
                .GetRepository<UserEntity>()
                .Get()
                .Where(e => e.Id == id).Include(i => i.Cars);

            return await Mapper.ProjectTo<UserDetailModel>(query).SingleOrDefaultAsync().ConfigureAwait(false);
        }
    }
}
