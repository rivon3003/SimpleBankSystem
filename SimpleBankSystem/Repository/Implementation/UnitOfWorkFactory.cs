using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SimpleBankSystem.Data;
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
        private SbsContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UnitOfWorkFactory(SbsContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public virtual TUnitOfWork Create()
        {
            return Activator.CreateInstance(typeof(TUnitOfWork), _context, _httpContextAccessor) as TUnitOfWork;
        }
    }
}
