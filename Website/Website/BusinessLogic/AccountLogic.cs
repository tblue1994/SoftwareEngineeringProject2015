using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Models;
using Website.Repositories;

namespace Website.BusinessLogic
{
    public class AccountLogic: IDisposable, IAccountLogic
    {
        private IUnitOfWork Unit;
        private IAccountRepository Repo;
        private ITeamRepository tRepo;
        private IAttainmentRepository attRepo;
        private IMembershipRepository mRepo;

        public AccountLogic(IUnitOfWork unit, IAccountRepository repo, ITeamRepository t, IAttainmentRepository a, IMembershipRepository m)
        {
            this.Unit = unit;
            this.Repo = repo;
            this.tRepo = t;
            this.mRepo = m;
            this.attRepo = a;

        }

        public Account Create(Account account )
        {
            Repo.Create(account);
            Unit.SaveChanges();
            return account;
        }

        public bool Delete(string id)
        {
            bool deleted = Repo.Delete(id);
            Unit.SaveChanges();
            return deleted;
        }

        public Account Update(Account account)
        {
            Repo.Update(account);
            Unit.SaveChanges();
            return account;
            
        }

        public bool AccountExists(string id)
        {
            
            return Repo.GetAll().Count(e => e.Id == id) > 0;
        }
        public Account Get(string id)
        {
            return Repo.Get(id);
        }

        public Account GetByFacebook(long id)
        {
            return Repo.GetByFacebookId(id);
        }

        public Account GetByTwitter(long id)
        {
            return Repo.GetByTwitterId(id);
        }

        public IQueryable<Account> GetAll()
        {
            return Repo.GetAll();
        }

        public List<Account> Search(string key)
        {
            return Repo.Search(key);
        }

        public List<Account> TeammatesWithBadge(string accountId, long badgeId)
        {
            List<Membership> mems = mRepo.GetByAccountId(accountId).ToList();
            List<Team> teams = new List<Team>();
            foreach(Membership m in mems)
            {
                teams.Add(tRepo.Get(m.TeamId));
            }
            mems.Clear();
            foreach(Team t in teams)
            {
                mems.AddRange(mRepo.GetByTeamId(t.Id));
            }
            List<Account> teammates = new List<Account>();
            foreach (Membership m in mems)
            {
                //teammates.Add(Repo.Get(m.AccountId).Attainments.Any(e => e.BadgeId == badgeId));
                Account a = Repo.Get(m.AccountId);
                if(a.Attainments.Any(e => e.BadgeId == badgeId) && a.Id!=accountId)
                {
                    teammates.Add(a);
                }
                
            }
            return teammates;
        }
        /* "standard" C# thread safe dispose pattern */
        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~AccountLogic()
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