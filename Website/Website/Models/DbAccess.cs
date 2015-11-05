using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Website.Models
{
    class DbAccess<T> : IDbAccess<T> where T : class
    {
        private IDbSet<T> set;

        public DbAccess(IDbSet<T> set)
        {
            this.set = set;
        }


        public IEnumerator<T> GetEnumerator()
        {
            return set.AsQueryable().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return set.AsQueryable().GetEnumerator();
        }

        public Type ElementType
        {
            get { return set.AsQueryable().ElementType; }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return set.AsQueryable().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return set.AsQueryable().Provider; }
        }

        public void AddOrUpdate(Expression<Func<T, object>> getId, params T[] items)
        {
            set.AddOrUpdate(getId, items);
        }


        public void Remove(T team)
        {
            set.Remove(team);
        }

        public void Add(T team)
        {
            set.Add(team);
        }

        public T Find(object id)
        {
            return set.Find(id);
        }
    }
}
