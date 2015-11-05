using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public enum TargetType { Steps, Distance, Duration }

    public enum TargetActivityType {Walking, Jogging, Running, Biking, General }

    public class Target
    {
        public long Id { get; set; }

        [Required]
        public TargetType Type { get; set; }

        [Required]
        public double TargetNumber { get; set; }
        
        [Required]
        public TargetActivityType ActivityType { get; set; } 
    }
}