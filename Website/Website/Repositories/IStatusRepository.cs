using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Models;

namespace Website.Repositories
{
    public interface IStatusRepository : IRepository<Status, long>
    {
        IQueryable<Status> GetByAccountId(string accountId);
    }
}
