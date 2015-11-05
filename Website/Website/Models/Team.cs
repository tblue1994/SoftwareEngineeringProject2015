using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class Team : IIdentified
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool Deleted { get; set; }

        public virtual ICollection<Membership> Memberships { get; set; }
    }
}