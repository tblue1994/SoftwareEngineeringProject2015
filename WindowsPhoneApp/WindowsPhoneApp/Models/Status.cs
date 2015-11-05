using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsPhoneApp.Models
{
    public class Status : IFeedable
    {
        public long Id { get; set; }


        public string AccountId { get; set; }


        public long MoodId { get; set; }


        public DateTime Date { get; set; }


        public DateTime FeedDate()
        {
            return Date;
        }
    }
}