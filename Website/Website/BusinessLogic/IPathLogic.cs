using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Models;

namespace Website.BusinessLogic
{
    public interface IPathLogic
    {
        Path Create(Path item);

        bool Delete(long id);

        Path Update(Path item);
        bool Exists(long id);
        Path Get(long id);
        Path GetByActivity(long id);
        void Dispose();
    }
}
