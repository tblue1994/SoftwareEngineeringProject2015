using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsPhoneApp.Models
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

        public static bool CanModerate(this MembershipStatus self)
        {
            return self == MembershipStatus.Admin;
        }
    }

    public class Membership : IFeedable
    {

        public long Id { get; set; }

        public string AccountId { get; set; }

        public DateTime Date { get; set; }

        public long TeamId { get; set; }



        public MembershipStatus Status { get; set; }


        public DateTime FeedDate()
        {
            return Date;
        }
    }
}