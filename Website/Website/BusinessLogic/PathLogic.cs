using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Models;
using Website.Repositories;

namespace Website.BusinessLogic
{
    public class PathLogic: IDisposable, IPathLogic
    {
        private IUnitOfWork Unit;
        private IPathRepository Repo;

        public PathLogic(IUnitOfWork unit, IPathRepository repo)
        {
            this.Unit = unit;
            this.Repo = repo;
        }

        public Path Create(Path item)
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

        public Path Update(Path item)
        {
            Repo.Update(item);
            Unit.SaveChanges();
            return item;
            
        }

        public bool Exists(long id)
        {
            
            return Repo.GetAll().Count(e => e.Id == id) > 0;
        }
        public Path Get(long id)
        {
            return Repo.Get(id);
        }

        public Path GetByActivity(long id)
        {
            return Repo.GetByActivityId(id);
        }
        /* "standard" C# thread safe dispose pattern */
        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~PathLogic()
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