using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Models;

namespace Website.Repositories
{
    public class PathRepository: Repository<Path,long>, IPathRepository 
    {

        public PathRepository(IDbSetFactory dbSetFactory)
            : base(dbSetFactory)
        { }

        public Path GetByActivityId(long activityId)
        {
            Path item = Set.FirstOrDefault(e => e.ActivityId == activityId);
            return item;
        }
    }
}