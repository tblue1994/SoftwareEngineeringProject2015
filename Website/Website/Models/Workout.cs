using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Website.Models
{
    
    public class Workout: IIdentified
    {
        public long Id { get; set; }

        [Required]
        public string AccountId { get; set; }
        
        [Required]
        public DateTime Date { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }

    }
}