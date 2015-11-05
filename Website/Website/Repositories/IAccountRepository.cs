using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Models;

namespace Website.Repositories
{
    public interface IAccountRepository : IRepository<Account, string>
    {
        Account GetByTwitterId(long twitterId);
        Account GetByFacebookId(long facebookId);
        List<Account> Search(string key);
    }
}
