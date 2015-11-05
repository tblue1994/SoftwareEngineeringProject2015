using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Models;

namespace Website.BusinessLogic
{
    public interface IBadgeLogic
    {
        Badge Create(Badge item);

        bool Delete(long id);

        Badge Update(Badge item);
        bool Exists(long id);
        Badge Get(long id);
        IQueryable<Badge> GetAll();
        void Dispose();
    }
}
