using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Models;

namespace Website.BusinessLogic
{
    public interface IFoodLogic
    {
        Food Create(Food item);

        bool Delete(long id);

        Food Update(Food item);
        bool Exists(long id);
        Food Get(long id);
        IQueryable<Food> GetByAccount(string accountId);
        IQueryable<Food> GetByTeam(long teamId);
        void Dispose();
    }
}
