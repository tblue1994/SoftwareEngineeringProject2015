using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Models;

namespace Website.BusinessLogic
{
    public interface IReportLogic
    {
        void Create();

        bool Delete(long id);

        Report Update(Report item);
        bool Exists(long id);
        Report Get(long id);
        IQueryable<Report> GetByAccount(string accountId);
        Report MiniReport(string accountId);
        void Dispose();
    }
}
