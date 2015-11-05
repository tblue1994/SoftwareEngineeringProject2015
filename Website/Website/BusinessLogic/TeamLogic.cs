using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Models;
using Website.Repositories;

namespace Website.BusinessLogic
{
    public class TeamLogic : IDisposable, ITeamLogic
    {
        private IUnitOfWork Unit;
        private ITeamRepository Repo;

        public TeamLogic(IUnitOfWork unit, ITeamRepository repo)
        {
            this.Unit = unit;
            this.Repo = repo;
        }

        public Team Create(Team item)
        {
            Repo.Create(item);
            Unit.SaveChanges();
            return item;
        }

        public bool Delete(long id)
        {
            Team t = Repo.Get(id);
            t.Deleted = true;
            Repo.Update(t);
            Unit.SaveChanges();
            return t.Deleted;
        }

        public Team Update(Team item)
        {
            Repo.Update(item);
            Unit.SaveChanges();
            return item;
            
        }

        public bool Exists(long id)
        {
            
            return Repo.GetAll().Count(e => e.Id == id) > 0;
        }
        public Team Get(long id)
        {
            return Repo.Get(id);
        }

        public IQueryable<Team> GetAll()
        {
            return Repo.GetAll();
        }
        public List<Team> Search(string key)
        {
            return Repo.Search(key);
        }

        /* "standard" C# thread safe dispose pattern */
        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~TeamLogic()
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