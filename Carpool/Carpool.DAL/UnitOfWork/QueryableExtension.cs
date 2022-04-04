using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Carpool.DAL.UnitOfWork
{
    //Taken from the sample project 'CookBook'
    public static class QueryableExtensions
    {
        public static async Task PreLoadChangeTracker<TEntity>(this IQueryable<TEntity> dbSet, Guid entityId, IModel model, CancellationToken cancellationToken) where TEntity : class, IEntity
            => await dbSet
                .IncludeFirstLevelNavigationProperties(model)
                .Where(e => e.Id == entityId)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

        public static IQueryable<TEntity> IncludeFirstLevelNavigationProperties<TEntity>(this IQueryable<TEntity> query, Microsoft.EntityFrameworkCore.Metadata.IModel model) where TEntity : class
        {
            var navigationProperties = model.FindEntityType(typeof(TEntity))?.GetNavigations();
            if (navigationProperties == null)
                return query;

            foreach (var navigationProperty in navigationProperties)
            {
                query = query.Include(navigationProperty.Name);
            }

            return query;
        }
    }
}
