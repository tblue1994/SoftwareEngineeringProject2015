using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Models;

namespace Website.Repositories
{
    public class MoodRepository : Repository<Mood, long>, IMoodRepository
    {
        public MoodRepository(IDbSetFactory dbSetFactory)
            : base(dbSetFactory)
        { }
    }
}