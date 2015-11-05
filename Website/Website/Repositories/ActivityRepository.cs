using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Website.Models;

namespace Website.Repositories
{
    public class ActivityRepository : Repository<Activity, long>, IActivityRepository
    {

        public ActivityRepository(IDbSetFactory dbSetFactory)
            : base(dbSetFactory)
        { }
        public IQueryable<Activity> GetByAccountId(string accountId)
        {
            IQueryable<Activity> items = Set.Where(e => e.AccountId == accountId);
            return items;
        }

        public IEnumerable<Activity> GetByAccountAndType(string accountId, ActivityType type)
        {
            var items = Set.SqlQuery("SELECT * FROM dbo.Activities WHERE AccountId = @account AND Type = @aType", new SqlParameter("@account", accountId), new SqlParameter("@aType", type));
            return items;
        }

        public IEnumerable<Activity> GetByDate(string accountId, DateTime start, DateTime end)
        {
            var items = Set.SqlQuery("SELECT * FROM dbo.Activities AS a WHERE a.AccountId = @account AND a.EndTime >= @start AND a.EndTime <= @finish", new SqlParameter("@account", accountId), new SqlParameter("@start", start), new SqlParameter("@finish", end));
            return items;
        }

        public IEnumerable<Activity> GetByDateAndType(string accountId, DateTime start, DateTime end, ActivityType type)
        {
            var items = Set.SqlQuery("SELECT * FROM dbo.Activities AS a WHERE a.AccountId = @account AND a.EndTime >= @start AND a.EndTime <= @finish AND a.Type = @aType", new SqlParameter("@account", accountId), new SqlParameter("@start", start), new SqlParameter("@finish", end), new SqlParameter("@aType", type));
            return items;
        }
    }
}