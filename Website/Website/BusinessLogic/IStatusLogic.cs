using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Models;

namespace Website.BusinessLogic
{
    public interface IStatusLogic
    {
        Status Create(Status item);

        bool Delete(long id);

        Status Update(Status item);
        bool Exists(long id);
        Status Get(long id);
        IQueryable<Status> GetByAccount(string accountId);
        IQueryable<Status> GetByTeam(long teamId);
        void Dispose();
    }
}
