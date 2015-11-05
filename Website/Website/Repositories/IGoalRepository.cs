using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Models;

namespace Website.Repositories
{
    public interface IGoalRepository : IRepository<Goal, long>
    {
        IQueryable<Goal> GetByAccountId(string accountId);
        IEnumerable<Goal> GetByAccountAndStatus(string accountId, GoalStatus status);
    }
}
