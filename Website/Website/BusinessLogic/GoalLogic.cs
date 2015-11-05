using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Models;
using Website.Repositories;

namespace Website.BusinessLogic
{
    public class GoalLogic : IDisposable, IGoalLogic
    {
        private IUnitOfWork Unit;
        private IGoalRepository Repo;
        private ITargetRepository tRepo;
        private IActivityRepository actRepo;

        public GoalLogic(IUnitOfWork unit, IGoalRepository repo, ITargetRepository t, IActivityRepository a)
        {
            this.Unit = unit;
            this.Repo = repo;
            this.tRepo = t;
            this.actRepo = a;
        }

        public Goal Create(Goal goal)
        {
            goal = Repo.Create(goal);
            Unit.SaveChanges();
            return goal;
        }

        public bool Delete(long id)
        {
            bool deleted = Repo.Delete(id);
            Unit.SaveChanges();
            return deleted;
        }

        public Goal Update(Goal goal)
        {
            Repo.Update(goal);
            Unit.SaveChanges();
            return goal;

        }

        public IQueryable<Goal> GetByAccount(string id)
        {
            return Repo.GetByAccountId(id);
        }

        public bool Exists(long id)
        {

            return Repo.GetAll().Count(e => e.Id == id) > 0;
        }
        public Goal Get(long id)
        {
            return Repo.Get(id);
        }

        public List<Goal> FailLateGoals(string id)
        {
            List<Goal> failed = new List<Goal>();
            List<Goal> current = Repo.GetByAccountAndStatus(id, GoalStatus.Current).ToList();
            foreach (Goal g in current)
            {
                DateTime Now = DateTime.UtcNow;
                int result = DateTime.Compare(g.EndDate, Now);
                if (result < 0)
                {
                    g.Status = GoalStatus.Failed;
                    failed.Add(Update(g));
                }
            }
            return failed;
        }

        public List<Goal> UpdateCurrentGoals(long activityId)
        {
            Activity activity = actRepo.Get(activityId);
            List<Goal> updated = new List<Goal>();
            List<Goal> current = Repo.GetByAccountAndStatus(activity.AccountId, GoalStatus.Current).ToList();
            foreach (Goal g in current)
            {
                Target t = tRepo.Get(g.TargetId);
                if ((t.ActivityType == TargetActivityType.Biking && activity.Type == ActivityType.Biking) ||
                    (t.ActivityType == TargetActivityType.Running && activity.Type == ActivityType.Running) ||
                    (t.ActivityType == TargetActivityType.Jogging && activity.Type == ActivityType.Jogging) ||
                    (t.ActivityType == TargetActivityType.Walking && activity.Type == ActivityType.Walking) ||
                    t.ActivityType == TargetActivityType.General)
                {
                    if (t.Type == TargetType.Duration)
                    {
                        TimeSpan time = activity.Duration;
                        g.Progress += time.TotalMinutes;
                    }
                    else if (t.Type == TargetType.Distance)
                    {
                        g.Progress += activity.Distance;
                    }
                    else if (t.Type == TargetType.Steps)
                    {
                        g.Progress += activity.Steps;
                    }
                }
                if (t.TargetNumber < g.Progress)
                {
                    g.Status = GoalStatus.Completed;
                    g.EndDate = DateTime.UtcNow;
                }

                updated.Add(Repo.Update(g));

            }
            Unit.SaveChanges();
            return updated;
        }

        /* "standard" C# thread safe dispose pattern */
        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~GoalLogic()
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