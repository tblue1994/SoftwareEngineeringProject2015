using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Models;

namespace Website.BusinessLogic
{
    public interface IActivityLogic
    {
        Activity Create(Activity activity);

        bool Delete(long id);

        Activity Update(Activity activity);
        bool ActivityExists(long id);
        Activity Get(long id);
        IQueryable<Activity> GetByAccount(string id);
        IQueryable<Activity> GetByTeam(long id);
        List<Activity> GetByDate(string id, DateTime begin, DateTime end);
        void Dispose();
    }
}
