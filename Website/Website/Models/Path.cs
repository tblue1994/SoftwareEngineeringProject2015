using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class Path : IIdentified
    {
        public long Id { get; set; }

        [Required]
        public long ActivityId { get; set; }

        [Required]
        public byte[] Data { get; set; }

    }
}