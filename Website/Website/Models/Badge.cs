using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public enum BadgeTimeline { SingleActivity, Daily, Weekly, Monthly, Cumulative }
    
    public class Badge : IIdentified
    {
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public String Description { get; set; }

        [Required]
        public BadgeTimeline Timeline { get; set; }

        [Required]
        public long TargetId { get; set; }

        public virtual Target Target { get; set; }
    }
}