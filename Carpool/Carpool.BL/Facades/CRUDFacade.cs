using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using Carpool.BL.Models;
using Carpool.DAL;
using Carpool.DAL.Entities;
using Carpool.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Carpool.BL.Facades;

public class CRUDFacade<TEntity, TListModel, TDetailModel>
        where TEntity : class, IEntity
        where TListModel : IModel
        where TDetailModel : class, IModel
{
    protected readonly IMapper Mapper;
    protected readonly IUnitOfWorkFactory UnitOfWorkFactory;

    protected CRUDFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper)
    {
        UnitOfWorkFactory = unitOfWorkFactory;
        Mapper = mapper;
    }

    public async Task DeleteAsync(TDetailModel model) => await this.DeleteAsync(model.Id);

    public async Task DeleteAsync(Guid id)
    {
        await using var uow = UnitOfWorkFactory.Create();
        uow.GetRepository<TEntity>().Delete(id);
        await uow.CommitAsync().ConfigureAwait(false);
    }

    public async Task<TDetailModel?> GetAsync(Guid id)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = uow
            .GetRepository<TEntity>()
            .Get()
            .Where(e => e.Id == id);
        return await Mapper.ProjectTo<TDetailModel>(query).SingleOrDefaultAsync().ConfigureAwait(false);
    }

    public async Task<IEnumerable<TListModel>> GetAsync()
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = uow
            .GetRepository<TEntity>()
            .Get();
        return await Mapper.ProjectTo<TListModel>(query).ToArrayAsync().ConfigureAwait(false);
    }

    public async Task<TDetailModel> SaveAsync(TDetailModel model)
    {
        await using var uow = UnitOfWorkFactory.Create();

        var entity = await uow
            .GetRepository<TEntity>()
            .InsertOrUpdateAsync(model, Mapper)
            .ConfigureAwait(false);
        await uow.CommitAsync();

        return (await GetAsync(entity.Id).ConfigureAwait(false))!;
    }


}
