using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Models;

namespace Website.Repositories
{
    public class FoodRepository : Repository<Food, long>, IFoodRepository
    {

        public FoodRepository(IDbSetFactory dbSetFactory)
            : base(dbSetFactory)
        { }

        public IQueryable<Food> GetByAccountId(string accountId)
        {
            IQueryable<Food> items = Set.Where(e => e.AccountId == accountId);
            return items;
        }
    }
}