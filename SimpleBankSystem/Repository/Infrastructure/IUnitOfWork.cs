using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBankSystem.Repository.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
        void Save();
        DbContext GetContext();
    }
}
