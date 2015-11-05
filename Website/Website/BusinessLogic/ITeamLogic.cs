using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Models;

namespace Website.BusinessLogic
{
    public interface ITeamLogic
    {
        Team Create(Team item);

        bool Delete(long id);

        Team Update(Team item);
        bool Exists(long id);
        Team Get(long id);
        IQueryable<Team> GetAll();
        List<Team> Search(string key);
        void Dispose();
    }
}
