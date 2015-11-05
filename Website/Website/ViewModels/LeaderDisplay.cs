using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.ViewModels
{
    public class LeaderDisplay
    {
        public readonly string AccountName;
        public readonly double Value;

        public LeaderDisplay(string accName, double value)
        {
            this.AccountName = accName;
            this.Value = value;
        }
    }
}