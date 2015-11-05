using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsPhoneApp.Models
{
    public class Exercise
    {
        public long Id { get; set; }

        
        public long WorkoutId { get; set; }

        
        public string Description { get; set; }

        
        public int Sets { get; set; }

        
        public int Repetitions { get; set; }

        public int Weight { get; set; }

    }
}