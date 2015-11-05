using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Website.Models
{
    public interface IDbAccess<T> : IQueryable<T>
    {
        void AddOrUpdate(Expression<Func<T, object>> getId, params T[] items);

        void Remove(T team);

        void Add(T team);

        T Find(object id);
    }
}
