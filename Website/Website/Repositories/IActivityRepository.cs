using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Models;

namespace Website.Repositories
{
    public interface IActivityRepository :IRepository<Activity,long>
    {
        IQueryable<Activity> GetByAccountId(string accountId);
        IEnumerable<Activity> GetByDate(string accountId, DateTime start, DateTime end);
        IEnumerable<Activity> GetByDateAndType(string accountId, DateTime start, DateTime end, ActivityType type);
        IEnumerable<Activity> GetByAccountAndType(string accountId, ActivityType type);
    }
}
