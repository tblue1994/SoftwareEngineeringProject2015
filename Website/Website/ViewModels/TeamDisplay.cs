using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Models;

namespace Website.ViewModels
{
    public class TeamDisplay
    {
        public readonly string TeamName;
        public readonly MembershipStatus Status;
        public readonly long Id;

        public TeamDisplay(string teamName, MembershipStatus status, long Id)
        {
            this.TeamName = teamName;
            this.Status = status;
            this.Id = Id;
        }
    }
}