using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Website.Models;

namespace Website.Repositories
{
    public class TeamRepository: Repository<Team,long>, ITeamRepository 
    {

        public TeamRepository(IDbSetFactory dbSetFactory)
            : base(dbSetFactory)
        { }
        public List<Team> Search(string key)
        {
            string[] keys = key.Split(' ');
            List<Team> items = new List<Team>();
            foreach (string s in keys)
            {
                string query = "%" + s + "%";
                items.AddRange(Set.SqlQuery("SELECT * FROM dbo.Teams WHERE Name LIKE @name", new SqlParameter("@name", query)));
            }
            items = items.Distinct().ToList();
            return items;
        }
    }
}