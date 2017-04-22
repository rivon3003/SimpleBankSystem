using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBankSystem.Repository.Infrastructure
{
    public interface IUnitOfWorkFactory<out TUnitOfWork> where TUnitOfWork : IUnitOfWork
    {
        TUnitOfWork Create();
    }
}
