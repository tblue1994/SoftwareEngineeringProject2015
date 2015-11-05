using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Website.Models;

namespace Website.Repositories
{
    public class MembershipRepository: Repository<Membership,long>, IMembershipRepository 
    {

        public MembershipRepository(IDbSetFactory dbSetFactory)
            : base(dbSetFactory)
        { }

        public IQueryable<Membership> GetByAccountId(string accountId)
        {
            IQueryable<Membership> items = Set.Where(e => e.AccountId == accountId);
            return items;
        }

        public IQueryable<Membership> GetByTeamId(long teamId)
        {
            IQueryable<Membership> items = Set.Where(e => e.TeamId == teamId);
            return items;
        }

        public Membership GetTeamAndAccountId(long teamId, string accountId)
        {
            var query = Set.SqlQuery("SELECT * FROM dbo.Memberships WHERE AccountId = @account AND TeamId = @team", new SqlParameter("@account", accountId), new SqlParameter("@team", teamId));
            Membership item =query.FirstOrDefault<Membership>();
            return item;
        }
    }
}