using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Website.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class Account : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string PreferredName { get; set; }
        [Required]
        public int Zip { get; set; }
        public long? FacebookId { get; set; }

        public long? TwitterId { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }
        public bool? Sex { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required]
        public int Height { get; set; }

        public string PictureUrl { get; set; }

        public virtual ICollection<Membership> Memberships { get; set; }

        public virtual ICollection<Goal> Goals { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<Workout> Workouts { get; set; }

        public virtual ICollection<Attainment> Attainments { get; set; }
        public virtual ICollection<Food> FoodEaten { get; set; }
        public virtual ICollection<Status> Statuses { get; set; }
        public virtual ICollection<Report> Reports { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Account> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}