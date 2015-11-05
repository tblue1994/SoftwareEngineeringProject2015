using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.ViewModels
{
    public class TeamDashboardViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string BarCSV { get; set; }
        public string LineCSV { get; set; }
        public string PieCSV { get; set; }
        public List<ScatterDisplay> Scatter { get; set; }
        public List<LeaderDisplay> Achievement { get; set; }
        public List<LeaderDisplay> Distance { get; set; }
    }
}