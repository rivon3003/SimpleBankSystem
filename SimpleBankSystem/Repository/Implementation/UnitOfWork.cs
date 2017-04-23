using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SimpleBankSystem.Data;
using SimpleBankSystem.Models.Entity;
using SimpleBankSystem.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using SimpleBankSystem.Constants.Web;
using System.Reflection;
using SimpleBankSystem.Extension;
using SimpleBankSystem.Constants.TrackingData;
using SimpleBankSystem.Constants.Value;
using SimpleBankSystem.Models.ViewModel.Account;

namespace SimpleBankSystem.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SbsContext dbContext;
        private readonly Dictionary<Type, IGenericRepository> cachedRepositories = new Dictionary<Type, IGenericRepository>();
        private bool diposed = false;

        private ISession _session;

        public UnitOfWork(SbsContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            _session = httpContextAccessor.HttpContext.Session;
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity);
            if (cachedRepositories.ContainsKey(type))
            {
                return cachedRepositories[type] as IGenericRepository<TEntity>;
            }
            else
            {
                var repository = new GenericRepository<TEntity>(dbContext.Set<TEntity>());
                cachedRepositories[type] = repository;
                return repository;
            }
        }

        public void Save()
        {
            TrackDataRecords();
            dbContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.diposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.diposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public DbContext GetContext()
        {
            return dbContext;
        }

        private void TrackDataRecords()
        {
            var changeSet = dbContext.ChangeTracker.Entries();

            if (changeSet != null)
            {
                var currentDateTime = DateTime.Now;
                Type type;

                //TODO: Remove when Login function completed
                string currentAccount = Common.NotAvailable;
                if (_session.GetObjectFromJson<Account>(SessionName.LoggedAccount) != null)
                {
                    currentAccount = _session.GetObjectFromJson<LoginViewModel>(SessionName.LoggedAccount).AccountNumber.ToString();
                }
                
                var changes = changeSet.Where(c => c.State != EntityState.Unchanged && c.State != EntityState.Deleted);
                foreach (var entry in changes)
                {
                    type = entry.Entity.GetType();
                    if (type.GetProperty(Property.Id) != null)
                    {
                        entry.State = (int)type.GetProperty(Property.Id).GetValue(entry.Entity, null) < 0
                            ? EntityState.Added : EntityState.Modified;
                    }
                    var updAtPr = type.GetProperty(Property.UpdatedDate);
                    var updByPr = type.GetProperty(Property.UpdatedBy);
                    var insAtPr = type.GetProperty(Property.CreatedDate);
                    var insByPr = type.GetProperty(Property.CreatedBy);

                    if (updAtPr != null) type.GetProperty(Property.UpdatedDate).SetValue(entry.Entity, currentDateTime, null);
                    if (updByPr != null) type.GetProperty(Property.UpdatedBy).SetValue(entry.Entity, currentAccount, null);

                    if (insAtPr != null && (type.GetProperty(Property.CreatedDate).GetValue(entry.Entity, null) == null
                        || (DateTime)type.GetProperty(Property.CreatedDate).GetValue(entry.Entity, null) == DateTime.MinValue))
                    {
                        type.GetProperty(Property.CreatedDate).SetValue(entry.Entity, currentDateTime, null);
                    }
                    if (insByPr != null && type.GetProperty(Property.CreatedBy).GetValue(entry.Entity, null) == null)
                    {
                        type.GetProperty(Property.CreatedBy).SetValue(entry.Entity, currentAccount, null);
                    }
                    if (entry.State == EntityState.Added)
                    {
                        if (insAtPr != null) type.GetProperty(Property.CreatedDate).SetValue(entry.Entity, currentDateTime, null);
                        if (insByPr != null) type.GetProperty(Property.CreatedBy).SetValue(entry.Entity, currentAccount, null);
                    }
                }
            }
        }
    }
}
