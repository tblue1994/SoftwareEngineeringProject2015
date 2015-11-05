using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Models;

namespace Website.Repositories
{
    public class ReportRepository : Repository<Report, long>, IReportRepository
    {

        public ReportRepository(IDbSetFactory dbSetFactory)
            : base(dbSetFactory)
        { }

        public IQueryable<Report> GetByAccountId(string accountId)
        {
            IQueryable<Report> items = Set.Where(e => e.AccountId == accountId);
            return items;
        }
    }
}