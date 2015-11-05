using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsPhoneApp.Models
{
    public enum Measurement { Ounce, FluidOunce, Cup, Serving }

	public static class MeasurementExtensions
	{
		public static string InSentence(this Measurement self, bool plural)
		{
			string result = "";
			switch(self)
			{
				case Measurement.Cup: result = "cup"; break;
				case Measurement.FluidOunce: result = "fluid ounce"; break;
				case Measurement.Ounce: result = "ounce"; break;
				case Measurement.Serving: result = "serving"; break;
			}
			if (plural) result += "s";
			return result;
		}
	}

    public class Food : IFeedable
    {
        public long Id { get; set; }

        public string AccountId { get; set; }


        public string FoodName { get; set; }


        public double Amount { get; set; }


        public Measurement Measurement { get; set; }


        public DateTime Date { get; set; }

        public DateTime FeedDate()
        {
            return Date;
        }
    }
}