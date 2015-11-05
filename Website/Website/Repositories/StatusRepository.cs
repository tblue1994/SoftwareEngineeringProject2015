using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Models;

namespace Website.Repositories
{
    public class StatusRepository : Repository<Status, long>, IStatusRepository
    {

        public StatusRepository(IDbSetFactory dbSetFactory)
            : base(dbSetFactory)
        { }

        public IQueryable<Status> GetByAccountId(string accountId)
        {
            IQueryable<Status> items = Set.Where(e => e.AccountId == accountId);
            return items;
        }
    }
}