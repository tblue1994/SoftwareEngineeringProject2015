using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.ViewModels
{
    public class AccountInformationViewModel
    {
        public string FullName { get; set; }
        public string PreferredName { get; set; }
        public int Zip { get; set; }
        public DateTime Birthdate { get; set; }
        public bool? Sex { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public string PictureUrl { get; set; }
        public List<BadgeDisplay> EarnedBadges { get; set; }
        public List<BadgeDisplay> UnearnedBadges { get; set; }
        public List<TeamDisplay> Teams { get; set; }

    }
}