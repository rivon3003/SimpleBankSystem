using Microsoft.EntityFrameworkCore;
using SimpleBankSystem.Repository.Infrastructure;
using System;
using System.Linq;
using System.Linq.Expressions;
using SimpleBankSystem.Constants.Repository;
using System.Threading.Tasks;

namespace SimpleBankSystem.Repository.Implementation
{
    public class GenericRepository<TEntity>: IGenericRepository<TEntity> where TEntity : class
    {
        internal DbSet<TEntity> dbSet;
        readonly IQueryable<TEntity> dbSetIQueryable;
        private readonly bool permanentDelete;
        public GenericRepository(DbSet<TEntity> dbSet, bool permanentDelete = true)
        {
            if (dbSet == null) throw new ArgumentNullException(Error.NullDbSet);
            this.dbSet = dbSet as DbSet<TEntity>;
            this.dbSetIQueryable = dbSet as IQueryable<TEntity>;
            this.permanentDelete = permanentDelete;
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedEnumerable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            //if(orderBy != null)
            //{
            //    return orderBy(query);
            //}
            //else
            //{
            return query;
            //}
        }

        public virtual IQueryable<TEntity> Get()
        {
            IQueryable<TEntity> query = dbSet;
            return query;
        }

        public virtual TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }

        public virtual TEntity Insert(TEntity entity)
        {
            return dbSet.Add(entity).Entity;
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (!permanentDelete) throw new Exception(Error.CannotDelete);
            dbSet.Attach(entityToDelete);
            dbSet.Remove(entityToDelete);
        }

        public virtual void DeleteById(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            dbSet.Remove(entityToDelete);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> deletedEntitiesFilter)
        {
            var deletedEntities = dbSet.Where(deletedEntitiesFilter);
            foreach (var deletedEntity in deletedEntities)
            {
                dbSet.Attach(deletedEntity);
                dbSet.Remove(deletedEntity);
            }
        }

        public virtual TEntity Attach(TEntity entity)
        {
            return dbSet.Attach(entity).Entity;
        }
    }
}
