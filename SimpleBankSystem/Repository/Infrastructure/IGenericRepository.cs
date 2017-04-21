using System;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleBankSystem.Repository.Infrastructure
{
    public interface IGenericRepository
    {

    }

    public interface IGenericRepository<TEntity> : IGenericRepository where TEntity : class
    {
            IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedEnumerable<TEntity>> orderBy = null, string includeProperties = "");
            TEntity Insert(TEntity entity);
            TEntity GetById(object id);
            void DeleteById(object id);
            void Delete(TEntity entityToDelete);
            void Delete(Expression<Func<TEntity, bool>> deletedEntitiesFilter);
            TEntity Attach(TEntity entity);
    }
}
