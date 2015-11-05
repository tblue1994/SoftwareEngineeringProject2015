using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Models;

namespace Website.Repositories
{
    public class TargetRepository : Repository<Target, long>, ITargetRepository
    {

        public TargetRepository(IDbSetFactory dbSetFactory)
            : base(dbSetFactory)
        { }
    }
}