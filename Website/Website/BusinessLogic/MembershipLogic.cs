using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Models;
using Website.Repositories;

namespace Website.BusinessLogic
{
    public class MembershipLogic: IDisposable, IMembershipLogic
    {
        private IUnitOfWork Unit;
        private IMembershipRepository Repo;

        public MembershipLogic(IUnitOfWork unit, IMembershipRepository repo)
        {
            this.Unit = unit;
            this.Repo = repo;
        }

        public Membership Create(Membership item)
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

        public Membership Leave(long id)
        {
            Membership m = Repo.Get(id);
            m.Status = MembershipStatus.Left;
            Repo.Update(m);
            Unit.SaveChanges();
            return m;
        }

        public Membership Update(Membership item)
        {
            Repo.Update(item);
            Unit.SaveChanges();
            return item;
            
        }

        public bool Exists(long id)
        {
            
            return Repo.GetAll().Count(e => e.Id == id) > 0;
        }
        public Membership Get(long id)
        {
            return Repo.Get(id);
        }

        public IQueryable<Membership> GetByAccount(string id)
        {
            return Repo.GetByAccountId(id);
        }

        public IQueryable<Membership> GetByTeam(long id)
        {
            return Repo.GetByTeamId(id);
        }

        public Membership GetByTeamAndAccount(long id, string accId)
        {
            return Repo.GetTeamAndAccountId(id, accId);
        }
        
        
        /* "standard" C# thread safe dispose pattern */
        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~MembershipLogic()
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