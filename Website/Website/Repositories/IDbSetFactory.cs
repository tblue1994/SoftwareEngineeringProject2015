using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Repositories
{
    public interface IDbSetFactory : IDisposable
    {
        DbSet<T> CreateDbSet<T>() where T : class;
        void ChangeObjectState(object entity, EntityState state);
    }

}
