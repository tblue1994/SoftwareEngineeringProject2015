using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Models;

namespace Website.BusinessLogic
{
    public interface ITargetLogic
    {
        Target Create(Target item);

        bool Delete(long id);

        Target Update(Target item);
        bool Exists(long id);
        Target Get(long id);
        void Dispose();
    }
}
