using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class Exercise : IIdentified
    {
        public long Id { get; set; }

        [Required]
        public long WorkoutId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Sets { get; set; }

        [Required]
        public int Repetitions { get; set; }

        public int Weight { get; set; }

    }
}