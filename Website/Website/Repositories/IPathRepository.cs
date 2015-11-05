using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Models;

namespace Website.Repositories
{
    public interface IPathRepository : IRepository<Path, long>
    {
        Path GetByActivityId(long activityId);
    }
}
