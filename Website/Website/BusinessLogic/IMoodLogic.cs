using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Models;

namespace Website.BusinessLogic
{
    public interface IMoodLogic
    {
        Mood Create(Mood item);

        bool Delete(long id);

        Mood Update(Mood item);
        bool Exists(long id);
        Mood Get(long id);
        IQueryable<Mood> GetAll();
        void Dispose();
    }
}
