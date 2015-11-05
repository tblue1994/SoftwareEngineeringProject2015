using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Models;

namespace Website.Repositories
{
    public class BadgeRepository: Repository<Badge,long>, IBadgeRepository 
    {
        public BadgeRepository(IDbSetFactory dbSetFactory)
            : base(dbSetFactory)
        { }
    }
}