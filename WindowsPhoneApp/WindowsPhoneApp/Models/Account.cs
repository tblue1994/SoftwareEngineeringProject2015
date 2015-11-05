using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace WindowsPhoneApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class Account
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public string PreferredName { get; set; }

        public int Zip { get; set; }
        public long? FacebookId { get; set; }

        public long? TwitterId { get; set; }

        public DateTime Birthdate { get; set; }
        public bool? Sex { get; set; }

        public int Weight { get; set; }

        public int Height { get; set; }

        public string PictureUrl { get; set; }

        public virtual ICollection<Membership> Memberships { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<Workout> Workouts { get; set; }

        public virtual ICollection<Attainment> Attainments { get; set; }
        public virtual ICollection<Food> FoodEaten { get; set; }

        public Account EliminateNull()
        {
            Memberships = Memberships ?? new List<Membership>();
            Activities = Activities ?? new List<Activity>();
            Workouts = Workouts ?? new List<Workout>();
            Attainments = Attainments ?? new List<Attainment>();
            FoodEaten = FoodEaten ?? new List<Food>();
            return this;
        }
    }
}