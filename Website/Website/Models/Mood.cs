using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class Mood : IIdentified
    {
        public long Id { get; set; }

        [Required]
        public string Description { get; set; }
    }
}