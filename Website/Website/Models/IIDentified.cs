using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Models
{
    public interface IIdentified
    {
        long Id { get; set; }
    }
}
