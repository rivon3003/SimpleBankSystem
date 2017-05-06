using Microsoft.EntityFrameworkCore;
using SimpleBankSystem.Models.Result;
using System;
using System.Threading.Tasks;

namespace SimpleBankSystem.Repository.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
        void Save();
        Task<SaveResultModel> SaveAsync();
        DbContext GetContext();
    }
}
