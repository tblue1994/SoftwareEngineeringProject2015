using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Models;

namespace Website.Repositories
{
    public class AttainmentRepository: Repository<Attainment,long>, IAttainmentRepository 
    {

        public AttainmentRepository(IDbSetFactory dbSetFactory)
            : base(dbSetFactory)
        { }

        public IQueryable<Attainment> GetByAccountId(string accountId)
        {
            IQueryable<Attainment> items = Set.Where(e => e.AccountId == accountId);
            return items;
        }
    }
}