using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Models;
using Website.Repositories;

namespace Website.BusinessLogic
{
    public class ReportLogic : IDisposable, IReportLogic
    {
        private IUnitOfWork Unit;
        private IReportRepository Repo;
        private IActivityRepository actRepo;
        private IAccountRepository aRepo;

        public ReportLogic(IUnitOfWork unit, IReportRepository repo, IActivityRepository a, IAccountRepository ac)
        {
            this.Unit = unit;
            this.Repo = repo;
            this.actRepo = a;
            this.aRepo = ac;
        }

        public void Create()
        {
            DateTime now = DateTime.UtcNow;
            DateTime past = now.AddDays(-1);

            List<Account> all = aRepo.GetAll().ToList();
            foreach (Account a in all)
            {
                List<Activity> activities = actRepo.GetByDate(a.Id, past, now).ToList();
                double duration = activities.Sum(e => e.Duration.TotalHours);
                int steps = activities.Sum(e => e.Steps);
                double distance = activities.Sum(e => e.Distance);
                Repo.Create(new Report { AccountId = a.Id, Distance = distance, Steps = steps, Duration = duration, Date = now });
            }
            Unit.SaveChanges();
        }

        public Report MiniReport(string accountId)
        {
            DateTime now = DateTime.UtcNow;
            DateTime past = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0); ;
            List<Activity> activities = actRepo.GetByDate(accountId, past, now).ToList();
            double duration = activities.Sum(e => (e.EndTime - e.BeginTime).TotalMinutes);
            int steps = activities.Sum(e => e.Steps);
            double distance = activities.Sum(e => e.Distance);
            Report r = new Report { AccountId = accountId, Distance = distance, Steps = steps, Duration = duration, Date = now };
            return r;
        }

        public bool Delete(long id)
        {
            bool deleted = Repo.Delete(id);
            Unit.SaveChanges();
            return deleted;
        }

        public Report Update(Report item)
        {
            Repo.Update(item);
            Unit.SaveChanges();
            return item;

        }

        public bool Exists(long id)
        {

            return Repo.GetAll().Count(e => e.Id == id) > 0;
        }
        public Report Get(long id)
        {
            return Repo.Get(id);
        }

        public IQueryable<Report> GetByAccount(string accountId)
        {
            return Repo.GetByAccountId(accountId);
        }
        /* "standard" C# thread safe dispose pattern */
        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ReportLogic()
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