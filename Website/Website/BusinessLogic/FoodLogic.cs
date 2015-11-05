using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Models;
using Website.Repositories;

namespace Website.BusinessLogic
{
    public class FoodLogic : IDisposable, IFoodLogic
    {
        private IUnitOfWork Unit;
        private IFoodRepository Repo;
        private ITeamLogic TeamLogic;

        public FoodLogic(IUnitOfWork unit, IFoodRepository repo, ITeamLogic teamLogic)
        {
            this.Unit = unit;
            this.Repo = repo;
            this.TeamLogic = teamLogic;
        }

        public Food Create(Food item)
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

        public Food Update(Food item)
        {
            Repo.Update(item);
            Unit.SaveChanges();
            return item;

        }

        public bool Exists(long id)
        {
            return Repo.GetAll().Count(e => e.Id == id) > 0;
        }

        public Food Get(long id)
        {
            return Repo.Get(id);
        }

        public IQueryable<Food> GetByAccount(string accountId)
        {
            return Repo.GetByAccountId(accountId);
        }

        public IQueryable<Food> GetByTeam(long id)
        {
            IEnumerable<string> accountIds = TeamLogic.Get(id).Memberships.Select(m => m.AccountId);
            return Repo.GetAll()
                .Where(a => accountIds.Contains(a.AccountId));
        }
        /* "standard" C# thread safe dispose pattern */
        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~FoodLogic()
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