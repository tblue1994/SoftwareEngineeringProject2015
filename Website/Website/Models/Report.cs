using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class Report
    {
        public long Id { get; set; }

        [Required]
        public string AccountId { get; set; }

        [Required]
        public int Steps { get; set; }

        [Required]
        public double Distance { get; set; }

        [Required]
        public double Duration { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}