using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Website.Models;

namespace Website.Repositories
{
    public class GoalRepository: Repository<Goal,long>, IGoalRepository 
    {

        public GoalRepository(IDbSetFactory dbSetFactory)
            : base(dbSetFactory)
        { }

        public IQueryable<Goal> GetByAccountId(string accountId)
        {
            IQueryable<Goal> items = Set.Where(e => e.AccountId == accountId);
            return items;
        }

        public IEnumerable<Goal> GetByAccountAndStatus(string accountId, GoalStatus status)
        {
            var items = Set.SqlQuery("SELECT * FROM dbo.Goals WHERE AccountId = @account AND Status = @gStatus", new SqlParameter("@account", accountId), new SqlParameter("@gStatus", status));
            return items;
        }
    }
}