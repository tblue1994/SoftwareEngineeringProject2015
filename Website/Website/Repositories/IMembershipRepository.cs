using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Models;

namespace Website.Repositories
{
    public interface IMembershipRepository : IRepository<Membership, long>
    {
        IQueryable<Membership> GetByAccountId(string accountId);
        IQueryable<Membership> GetByTeamId(long teamId);
        Membership GetTeamAndAccountId(long teamId, string accountId);
    }
}
