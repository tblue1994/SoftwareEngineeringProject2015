using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Models;
using Website.Repositories;

namespace Website.BusinessLogic
{
    public class BadgeLogic : IDisposable, IBadgeLogic
    {
        private IUnitOfWork Unit;
        private IBadgeRepository Repo;

        public BadgeLogic(IUnitOfWork unit, IBadgeRepository repo)
        {
            this.Unit = unit;
            this.Repo = repo;
        }

        public Badge Create(Badge item)
        {
            Repo.Create(item);
            Unit.SaveChanges();
            return item;
        }

        public bool Delete(long id)
        {
            bool deleted = Repo.Delete(id);
            Unit.SaveChanges();
            return deleted;
        }

        public Badge Update(Badge item)
        {
            Repo.Update(item);
            Unit.SaveChanges();
            return item;
            
        }

        public bool Exists(long id)
        {
            
            return Repo.GetAll().Count(e => e.Id == id) > 0;
        }
        public Badge Get(long id)
        {
            return Repo.Get(id);
        }

        public IQueryable<Badge> GetAll()
        {
            return Repo.GetAll();
        }
        /* "standard" C# thread safe dispose pattern */
        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BadgeLogic()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                
            }
            _disposed = true;
        }
    }
}