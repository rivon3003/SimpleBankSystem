using Microsoft.EntityFrameworkCore;
using SimpleBankSystem.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBankSystem.Repository.Implementation
{
    public class UnitOfWorkFactory<TUnitOfWork, TDbContext> : IUnitOfWorkFactory<TUnitOfWork>
        where TUnitOfWork : UnitOfWork
        where TDbContext : DbContext
    {
        readonly string connectionStringName;

        public UnitOfWorkFactory(string connectionStringName)
        {
            this.connectionStringName = connectionStringName;
        }
        public virtual TUnitOfWork Create()
        {
            var ctx = Activator.CreateInstance(typeof(TDbContext), connectionStringName) as TDbContext;
            return Activator.CreateInstance(typeof(TUnitOfWork), ctx) as TUnitOfWork;
        }
    }
}
