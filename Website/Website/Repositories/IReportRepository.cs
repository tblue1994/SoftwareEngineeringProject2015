using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Models;

namespace Website.Repositories
{
    public interface IReportRepository : IRepository<Report, long>
    {
        IQueryable<Report> GetByAccountId(string accountId);
    }
}
