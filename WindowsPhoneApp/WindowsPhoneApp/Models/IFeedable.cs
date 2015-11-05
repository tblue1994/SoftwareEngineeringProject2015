using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPhoneApp.Models
{
    class IFeedableComparer : IComparer<IFeedable>
    {

        public int Compare(IFeedable x, IFeedable y)
        {
            int value = -x.FeedDate().CompareTo(y.FeedDate());
            //if (value == 0) value = -1;
            return value;
        }
    }

    public interface IFeedable
    {
        DateTime FeedDate();
    }
}
