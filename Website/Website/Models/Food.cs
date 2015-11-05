using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public enum Measurement { Ounce, FluidOunce, Cup, Serving }
    public class Food
    {
        public long Id { get; set; }
        
        [Required]
        public string AccountId { get; set; }

        [Required]
        public string FoodName { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public Measurement Measurement { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}