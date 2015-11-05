using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Models;

namespace Website.BusinessLogic
{
    public interface IAttainmentLogic
    {
        Attainment Create(Attainment item);

        bool Delete(long id);

        Attainment Update(Attainment item);
        bool Exists(long id);
        Attainment Get(long id);
        IQueryable<Attainment> GetByAccount(string id);
        IQueryable<Attainment> GetByTeam(long id);
        List<Attainment> GetNewAttainments(long activityId);
        List<Badge> UnearnedBadges(string accountId);

        void Dispose();
    }
}
