using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public enum ActivityType { Walking, Jogging, Running, Biking }
    public class Activity : IIdentified
    {
        public long Id { get; set; }

        [Required]
        public string AccountId { get; set; }

        [Required]
        public ActivityType Type { get; set; }

        [Required]
        public String Description { get; set; }

        [Required]
        public int Steps { get; set; }

        [Required]
        public double Distance { get; set; }

        [Required]
        public DateTime BeginTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public TimeSpan Duration { get { return EndTime - BeginTime; } }
    }
}