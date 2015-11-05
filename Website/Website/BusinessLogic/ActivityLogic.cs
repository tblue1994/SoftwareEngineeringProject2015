using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Website.Models;
using Website.Repositories;

namespace Website.BusinessLogic
{
    public class ActivityLogic : IDisposable, IActivityLogic
    {
        private IUnitOfWork Unit;
        private IActivityRepository Repo;
        private ITeamLogic TeamLogic;

        public ActivityLogic(IUnitOfWork unit, IActivityRepository repo, ITeamLogic teamLogic)
        {
            this.Unit = unit;
            this.Repo = repo;
            this.TeamLogic = teamLogic;
        }

        public Activity Create(Activity activity)
        {
            Repo.Create(activity);
            Unit.SaveChanges();
            return activity;
        }

        public bool Delete(long id)
        {
            bool deleted = Repo.Delete(id);
            Unit.SaveChanges();
            return deleted;
        }

        public Activity Update(Activity activity)
        {
            Repo.Update(activity);
            Unit.SaveChanges();
            return activity;
        }

        public bool ActivityExists(long id)
        {

            return Repo.GetAll().Count(e => e.Id == id) > 0;
        }
        public Activity Get(long id)
        {
            return Repo.Get(id);
        }

        public IQueryable<Activity> GetByAccount(string id)
        {
            return Repo.GetByAccountId(id);
        }

        public IQueryable<Activity> GetByTeam(long id)
        {
            IEnumerable<string> accountIds = TeamLogic.Get(id).Memberships.Select(m => m.AccountId);
            return Repo.GetAll()
                .Where(a => accountIds.Contains(a.AccountId));
        }

        public List<Activity> GetByDate(string id, DateTime begin, DateTime end)
        {
            return Repo.GetByDate(id, begin, end).ToList();
        }

        /* "standard" C# thread safe dispose pattern */
        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ActivityLogic()
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