using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Website.Models;

namespace WebsiteTest
{
    public class MockAccess<T> : IDbAccess<T>, IEnumerable<T> where T : IIdentified
    {
        private int nextId = 1;
        private Dictionary<int, T> items = new Dictionary<int, T>();

        public T Find(int id)
        {
            return items[id];
        }

        public void Remove(T item)
        {
            items.Remove(item.Id);
        }

        public void Add(T item)
        {
            item.Id = nextId;
            items.Add(nextId, item);
            nextId += 1;
        }


        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return items.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return items.Values.GetEnumerator();
        }

        void IDbAccess<T>.AddOrUpdate(Expression<Func<T, object>> getId, params T[] items)
        {
            throw new NotImplementedException();
        }

        Type IQueryable.ElementType
        {
            get { return typeof(T); }
        }

        Expression IQueryable.Expression
        {
            get { return items.Values.AsQueryable().Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return items.Values.AsQueryable().Provider; }
        }

        public T this[int id]
        {
            get
            {
                return items[id];
            }
            set
            {
                items[id] = value;
            }
        }
    }
}
