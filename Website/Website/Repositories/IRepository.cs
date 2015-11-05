using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Repositories
{
    public interface IRepository<T,k>
    {
        T Create(T item);

        bool Delete(k key);

        T Update(T item);

        T Get(k key);

        IQueryable<T> GetAll();
    }
}
