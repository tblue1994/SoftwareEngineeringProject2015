using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Models;

namespace Website.ViewModels
{
    public class PersonalDashboardViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string PreferredName { get; set; }
        public string GoalCSV { get; set; }
        public string TwoDayCSV { get; set; }
        public string ReportCSV { get; set; }
        public string TotalCSV { get; set; }
        public string BarchartInsight { get; set; }
        public Goal Closest { get; set; }
        public string PiechartInsight { get; set; }
        public string LinechartInsight { get; set; }
        public double Prediction { get; set; }
		public bool CanShare { get; set; }
    }
}