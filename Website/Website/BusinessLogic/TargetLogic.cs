using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Models;
using Website.Repositories;

namespace Website.BusinessLogic
{
    public class TargetLogic : IDisposable, ITargetLogic
    {
        private IUnitOfWork Unit;
        private ITargetRepository Repo;

        public TargetLogic(IUnitOfWork unit, ITargetRepository repo)
        {
            this.Unit = unit;
            this.Repo = repo;
        }

        public Target Create(Target item)
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

        public Target Update(Target item)
        {
            Repo.Update(item);
            Unit.SaveChanges();
            return item;

        }

        public bool Exists(long id)
        {

            return Repo.GetAll().Count(e => e.Id == id) > 0;
        }
        public Target Get(long id)
        {
            return Repo.Get(id);
        }

        /* "standard" C# thread safe dispose pattern */
        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~TargetLogic()
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