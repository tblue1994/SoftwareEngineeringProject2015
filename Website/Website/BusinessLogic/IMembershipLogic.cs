using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Models;

namespace Website.BusinessLogic
{
    public interface IMembershipLogic
    {
        Membership Create(Membership item);

        bool Delete(long id);

        Membership Update(Membership item);
        bool Exists(long id);
        Membership Get(long id);

        Membership Leave(long id);
        IQueryable<Membership> GetByAccount(string id);
        IQueryable<Membership> GetByTeam(long id);
        Membership GetByTeamAndAccount(long id, string accId);
        void Dispose();
    }
}
