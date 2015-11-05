using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public enum GoalStatus { Completed, Challenged, Current, Failed }

    public enum TargetTimeline { SingleActivity, Daily, Weekly, Monthly, Cumulative }

    public class Goal : IIdentified
    {
        public long Id { get; set; }

        public TargetTimeline Timeline { get; set; }

        public string Description
        {
            get
            {
                string ActivityType = "";
                if (Target.ActivityType == TargetActivityType.General)
                {
                    ActivityType = "Moving";
                }
                else
                {
                    ActivityType = Target.ActivityType.ToString();
                }

                if (Target.Type == TargetType.Distance)
                {
                    return ActivityType + " " + Target.TargetNumber + " Miles";
                }
                else if (Target.Type == TargetType.Duration)
                {
                    return ActivityType + " for " + Target.TargetNumber + " Hours";
                }
                else
                {
                    return ActivityType + " " + Target.TargetNumber + " " + Target.Type;
                }
            }
        }

        [Required]
        public DateTime BeginDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public long TargetId { get; set; }

        [Required]
        public double Progress { get; set; }
        
        [Required]
        public string AccountId { get; set; }

        [Required]
        public GoalStatus Status { get; set; }

        public virtual Target Target { get; set; }
    }
}