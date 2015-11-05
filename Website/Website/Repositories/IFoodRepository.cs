using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Models;

namespace Website.Repositories
{
    public interface IFoodRepository : IRepository<Food, long>
    {
        IQueryable<Food> GetByAccountId(string accountId);
    }
}
