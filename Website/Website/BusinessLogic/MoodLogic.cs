using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Models;
using Website.Repositories;

namespace Website.BusinessLogic
{
    public class MoodLogic : IDisposable, IMoodLogic
    {
        private IUnitOfWork Unit;
        private IMoodRepository Repo;

        public MoodLogic(IUnitOfWork unit, IMoodRepository repo)
        {
            this.Unit = unit;
            this.Repo = repo;
        }

        public Mood Create(Mood item)
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

        public Mood Update(Mood item)
        {
            Repo.Update(item);
            Unit.SaveChanges();
            return item;

        }

        public bool Exists(long id)
        {

            return Repo.GetAll().Count(e => e.Id == id) > 0;
        }
        public Mood Get(long id)
        {
            return Repo.Get(id);
        }

        public IQueryable<Mood> GetAll()
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

        ~MoodLogic()
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