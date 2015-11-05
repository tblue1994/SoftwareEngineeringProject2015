using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsPhoneApp.Models
{
    public class Badge
    {
        public long Id { get; set; }


        public string Title { get; set; }


        public String Description { get; set; }


        public long TargetId { get; set; }

        public virtual Target Target { get; set; }
    }
}