using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Website.Models;

namespace Website.Repositories
{
    public class Repository<T, k> : IRepository<T, k> where T : class
    {

        public DbSet<T> Set;
        public IDbSetFactory factory;
        public Repository(IDbSetFactory setFactory)
        {
            this.Set = setFactory.CreateDbSet<T>();
            factory = setFactory;
        }

        public virtual T Create(T item)
        {
            return Set.Add(item);
        }

        public bool Delete(k key)
        {
            T t = Set.Find(key);
            if (t == null)
            {
                return false;
            }

            factory.ChangeObjectState(t, EntityState.Deleted);
            return true;
        }

        public T Update(T item)
        {
            factory.ChangeObjectState(item, EntityState.Modified);
            return item;
        }
        public T Get(k key)
        {
            return Set.Find(key);
        }

        public IQueryable<T> GetAll()
        {
            return Set;
        }
    }
}