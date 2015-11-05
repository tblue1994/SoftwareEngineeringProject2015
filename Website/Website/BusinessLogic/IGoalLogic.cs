using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Models;

namespace Website.BusinessLogic
{
    public interface IGoalLogic
    {
        Goal Create(Goal goal);

        bool Delete(long id);

        Goal Update(Goal goal);
        bool Exists(long id);
        Goal Get(long id);
        IQueryable<Goal> GetByAccount(string id);
        List<Goal> UpdateCurrentGoals(long activityId);
        List<Goal> FailLateGoals(string id);
        void Dispose();
    }
}
