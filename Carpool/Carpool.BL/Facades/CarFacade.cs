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
    public class CarFacade : CRUDFacade<CarEntity, CarListModel, CarDetailModel>
    {
        public CarFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
        {
            
        }

        public async Task<IEnumerable<CarListModel>> GetAsyncByOwnerId(Guid ownerId)
        {
            await using var uow = UnitOfWorkFactory.Create();
            var query = uow
                .GetRepository<CarEntity>()
                .Get().Where(e => e.OwnerId == ownerId);

            return await Mapper.ProjectTo<CarListModel>(query).ToArrayAsync().ConfigureAwait(false);
        }
    }
}
