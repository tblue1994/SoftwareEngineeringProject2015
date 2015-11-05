using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Models;
using Website.Repositories;

namespace Website.BusinessLogic
{
    public class AttainmentLogic : IDisposable, IAttainmentLogic
    {
        private IUnitOfWork Unit;
        private IAttainmentRepository Repo;
        private IBadgeRepository bRepo;
        private IActivityRepository actRepo;
        private ITargetRepository tRepo;
        private ITeamLogic teamLogic;

        public AttainmentLogic(IUnitOfWork unit, IAttainmentRepository repo, IBadgeRepository b, IActivityRepository a, ITargetRepository t, ITeamLogic teamLogic)
        {
            this.Unit = unit;
            this.Repo = repo;
            this.bRepo = b;
            this.actRepo = a;
            this.tRepo = t;
            this.teamLogic = teamLogic;
        }

        public Attainment Create(Attainment item)
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

        public Attainment Update(Attainment item)
        {
            Repo.Update(item);
            Unit.SaveChanges();
            return item;

        }

        public bool Exists(long id)
        {

            return Repo.GetAll().Count(e => e.Id == id) > 0;
        }
        public Attainment Get(long id)
        {
            return Repo.Get(id);
        }

        public IQueryable<Attainment> GetByAccount(string id)
        {
            return Repo.GetByAccountId(id);
        }

        public IQueryable<Attainment> GetByTeam(long id)
        {
            IEnumerable<string> accountIds = teamLogic.Get(id).Memberships.Select(m => m.AccountId);
            return Repo.GetAll()
                .Where(a => accountIds.Contains(a.AccountId));
        }

        public List<Badge> UnearnedBadges(string accountId)
        {
            List<Attainment> earned = Repo.GetByAccountId(accountId).ToList();
            List<Badge> badges = bRepo.GetAll().ToList();
            List<Badge> unearned = new List<Badge>();
            foreach (Badge b in badges)
            {
                bool match = false;
                foreach (Attainment a in earned)
                {
                    if (a.BadgeId == b.Id)
                    {
                        match = true;
                    }
                }
                if (!match)
                {
                    unearned.Add(b);
                }
            }
            return unearned;
        }

        public List<Attainment> GetNewAttainments(long activityId)
        {
            Activity activity = actRepo.Get(activityId);
            List<Badge> unearned = UnearnedBadges(activity.AccountId);
            List<Attainment> New = new List<Attainment>();
            foreach (Badge b in unearned)
            {
                if (Evaluate(activity.AccountId, b, activityId))
                {
                    New.Add(Create(new Attainment { AccountId = activity.AccountId, BadgeId = b.Id, DateEarned = DateTime.UtcNow }));
                }

            }
            return New;
        }

        private bool Evaluate(string accountId, Badge b, long activityId)
        {
            Target t = tRepo.Get(b.TargetId);
            Activity activity = actRepo.Get(activityId);
            List<Activity> activities = new List<Activity>();
            DateTime begin = activity.EndTime;
            DateTime end = begin;
            if (EqualActivityType(t.ActivityType, activity.Type))
            {
                if (b.Timeline == BadgeTimeline.SingleActivity)
                {
                    activities.Add(activity);
                }
                else if (b.Timeline == BadgeTimeline.Cumulative)
                {
                    activities = actRepo.GetByAccountAndType(accountId, activity.Type).ToList();
                }
                else
                {
                    if (b.Timeline == BadgeTimeline.Daily)
                    {
                        begin.AddDays(-1);
                    }
                    else if (b.Timeline == BadgeTimeline.Weekly)
                    {
                        begin.AddDays(-7);
                    }
                    else if (b.Timeline == BadgeTimeline.Monthly)
                    {
                        begin.AddMonths(-1);
                    }

                    if (t.ActivityType == TargetActivityType.General)
                    {
                        activities = actRepo.GetByDate(accountId, begin, end).ToList();
                    }
                    else
                    {
                        activities = actRepo.GetByDateAndType(accountId, begin, end, activity.Type).ToList();
                    }
                }
            }
            else
            {
                return false;
            }

            double total = 0;

            if (t.Type == TargetType.Duration)
            {
                foreach (Activity a in activities)
                {
                    total += a.Duration.TotalHours;
                }
            }
            else if (t.Type == TargetType.Distance)
            {
                foreach (Activity a in activities)
                {
                    total += a.Distance;
                }
            }
            else if (t.Type == TargetType.Steps)
            {
                foreach (Activity a in activities)
                {
                    total += a.Steps;
                }
            }

            if (total >= t.TargetNumber)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool EqualActivityType(TargetActivityType t, ActivityType a)
        {
            if ((t == TargetActivityType.Biking && a == ActivityType.Biking) ||
                    (t == TargetActivityType.Running && a == ActivityType.Running) ||
                    (t == TargetActivityType.Jogging && a == ActivityType.Jogging) ||
                    (t == TargetActivityType.Walking && a == ActivityType.Walking) ||
                    t == TargetActivityType.General)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /* "standard" C# thread safe dispose pattern */
        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~AttainmentLogic()
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