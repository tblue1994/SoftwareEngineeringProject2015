using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Models;

namespace WebsiteTest
{
    class MockDatabase : IWebsiteContext
    {
        private MockAccess<Account> accounts;
        private MockAccess<Team> teams;
        private MockAccess<Membership> memberships;

        public MockDatabase()
        {
            accounts = new MockAccess<Account>();
            teams = new MockAccess<Team>();
            memberships = new MockAccess<Membership>();
        }

        public IDbAccess<Account> Accounts
        {
            get { return accounts; }
        }

        public IDbAccess<Team> Teams
        {
            get { return teams; }
        }

        public IDbAccess<Membership> Memberships
        {
            get { return memberships; }
        }


        public int SaveChanges()
        {
            return 0;
        }

        public void Dispose()
        {

        }


        public void Put(Account account)
        {
            accounts[account.Id] = account;
        }

        public void Put(Membership membership)
        {
            memberships[membership.Id] = membership;
        }

        public void Put(Team team)
        {
            teams[team.Id] = team;
        }
    }
}
