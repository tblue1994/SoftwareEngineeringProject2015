using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Website.Models;

namespace Website.Repositories
{
    public class AccountRepository : Repository<Account, string>, IAccountRepository
    {
        private UserManager<Account> UserManager;

        public AccountRepository(IWebsiteContext context, IDbSetFactory dbSetFactory)
            : base(dbSetFactory)
        {
            if (context is WebsiteContext)
            {
                this.UserManager = new UserManager<Account>(new UserStore<Account>((WebsiteContext)context));
            }

        }

        public override Account Create(Account item)
        {
            item.Id = Guid.NewGuid().ToString();
            if (UserManager == null)
            {
                return base.Create(item);
            }
            else
            {
                var result = UserManager.Create(item);
                UserManager.AddClaim(item.Id, new System.Security.Claims.Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", item.UserName));

				if(item.FacebookId.HasValue)
				{
					return GetByFacebookId(item.FacebookId.Value);
				}
                else
				{
					return GetByTwitterId(item.TwitterId.Value);
				}
            }
        }

        public Account GetByFacebookId(long facebookId)
        {
            Account item = Set.FirstOrDefault(e => e.FacebookId == facebookId);
            return item;
        }

        public Account GetByTwitterId(long twitterId)
        {
            Account item = Set.FirstOrDefault(e => e.TwitterId == twitterId);
            return item;
        }

        public List<Account> Search(string key)
        {
            string[] keys = key.Split(' ');
            List<Account> items = new List<Account>();
            foreach (string s in keys)
            {
                string query = "%" + s + "%";
                items.AddRange(Set.SqlQuery("SELECT * FROM dbo.AspNetUsers WHERE FullName LIKE @name", new SqlParameter("@name", query)));

            }
            items = items.Distinct().ToList();
            return items;
        }

    }
}