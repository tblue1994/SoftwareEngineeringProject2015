using System;
using System.Collections.Generic;
using System.Linq;
using WindowsPhoneApp.Models;

namespace Website.Models
{
    public class Report : IFeedable
    {
        public long Id { get; set; }

        public string AccountId { get; set; }

        public int Steps { get; set; }

        public double Distance { get; set; }

        public double Duration { get; set; }

        public DateTime Date { get; set; }

        public DateTime FeedDate()
        {
            return Date;
        }
    }
}