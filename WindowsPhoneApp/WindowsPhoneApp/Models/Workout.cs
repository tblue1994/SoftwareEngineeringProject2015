using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsPhoneApp.Models
{

    public class Workout
    {
        public long Id { get; set; }


        public string AccountId { get; set; }


        public DateTime Date { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }

    }
}