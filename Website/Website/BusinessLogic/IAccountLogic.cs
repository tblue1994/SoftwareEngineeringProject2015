using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Models;

namespace Website.BusinessLogic
{
    public interface IAccountLogic
    {
        Account Create(Account account);

        bool Delete(string id);

        Account Update(Account account);
        bool AccountExists(string id);
        Account Get(string id);
        Account GetByFacebook(long id);
        Account GetByTwitter(long id);
        List<Account> Search(string key);
        IQueryable<Account> GetAll();

        List<Account> TeammatesWithBadge(string accountId, long badgeId);
        void Dispose();
    }
}
