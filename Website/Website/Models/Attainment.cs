using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class Attainment : IIdentified
    {
        public long Id { get; set; }

        [Required]
        public string AccountId { get; set; }

        [Required]
        public long BadgeId { get; set; }
        
        [Required]
        public DateTime DateEarned { get; set; }
    }
}