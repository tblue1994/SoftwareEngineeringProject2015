using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsPhoneApp.Models
{
    public class Team
    {
        public long Id { get; set; }


        public string Name { get; set; }


        public bool Deleted { get; set; }

        public virtual ICollection<Membership> Memberships { get; set; }
    }
}