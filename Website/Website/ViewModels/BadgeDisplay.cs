using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.ViewModels
{
    public class BadgeDisplay
    {
        public readonly string BadgeName;
        public readonly string Date;

        public BadgeDisplay(string badgeName, string date)
        {
            this.BadgeName = badgeName;
            this.Date = date;
        }
    }
}