using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsPhoneApp.Models
{
    public class Attainment : IFeedable
    {
        public long Id { get; set; }


        public string AccountId { get; set; }


        public long BadgeId { get; set; }


        public DateTime DateEarned { get; set; }

        public DateTime FeedDate()
        {
            return DateEarned;
        }
    }
}