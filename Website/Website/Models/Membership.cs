using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public enum MembershipStatus { Admin, Banned, Member, Invited, Left }

    public static class MembershipStatusExtensions
    {
        public static bool IsMember(this MembershipStatus self)
        {
            return self == MembershipStatus.Admin ||
                self == MembershipStatus.Member;
        }

        public static bool IsBanned(this MembershipStatus self)
        {
            return self == MembershipStatus.Banned;
        }
        public static bool IsInvited(this MembershipStatus self)
        {
            return self == MembershipStatus.Invited;
        }

        public static bool CanModerate(this MembershipStatus self)
        {
            return self == MembershipStatus.Admin;
        }
    }

    public class Membership : IIdentified
    {

        public long Id { get; set; }
        [Required]
        public string AccountId { get; set; }

        [Required]
        public long TeamId { get; set; }


        [Required]
        public MembershipStatus Status { get; set; }

        [Required]
        public DateTime Date { get; set; }

    }
}