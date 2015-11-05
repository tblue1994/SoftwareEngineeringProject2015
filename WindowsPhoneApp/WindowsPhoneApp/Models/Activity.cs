using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsPhoneApp.Models
{
    public enum ActivityType { Walking, Jogging, Running, Biking }
    public class Activity : IFeedable
    {
        public long Id { get; set; }


        public string AccountId { get; set; }


        public ActivityType Type { get; set; }


        public String Description { get; set; }


        public int Steps { get; set; }

        public double Distance { get; set; }

        public DateTime BeginTime { get; set; }


        public DateTime EndTime { get; set; }


        public DateTime FeedDate()
        {
            return EndTime;
        }

		public string Describe(string userName)
		{
			string activity = null;
			switch(Type)
			{
				case ActivityType.Walking: activity = "walked"; break;
				case ActivityType.Jogging: activity = "jogged"; break;
				case ActivityType.Running: activity = "ran"; break;
				case ActivityType.Biking: activity = "biked"; break;
			}

			return userName + " " + activity + " " + Math.Round(UserState.UseOldUnits ? Distance / 3 : Distance, 2) + 
                (UserState.UseOldUnits ? " leagues" : " miles.");
		}
	}
}