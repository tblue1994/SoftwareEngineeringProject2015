using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsPhoneApp.Models
{

    public enum TargetTimeline { SingleActivity, Daily, Weekly, Monthly, Cumulative }

    public static class TargetTimelineExtensions
    {
        public static string AsPostfix(this TargetTimeline type)
        {
            switch (type)
            {
                case TargetTimeline.Cumulative: return "ever";
                case TargetTimeline.Daily: return "in a day";
                case TargetTimeline.Monthly: return "in a month";
                case TargetTimeline.SingleActivity: return "at once";
                case TargetTimeline.Weekly: return "in a week";
            }
            throw new NotImplementedException();
        }

        public static TimeSpan Span(this TargetTimeline type)
        {
            switch (type)
            {
                case TargetTimeline.Cumulative: return TimeSpan.MaxValue;
                case TargetTimeline.Daily: return TimeSpan.FromDays(1);
                case TargetTimeline.Monthly: return TimeSpan.FromDays(30);
                case TargetTimeline.SingleActivity: return TimeSpan.FromDays(600);
                case TargetTimeline.Weekly: return TimeSpan.FromDays(7);
            }
            throw new NotImplementedException();
        }
    }
    public enum GoalStatus { Completed, Challenged, Current, Failed }
    public class Goal : IFeedable
    {
        public long Id { get; set; }

        public string Description { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public long TargetId { get; set; }

        public double Progress { get; set; }

        public string AccountId { get; set; }

        public GoalStatus Status { get; set; }

        public TargetTimeline Timeline { get; set; }

        public virtual Target Target { get; set; }

        public DateTime FeedDate()
        {
            return BeginDate;
        }
    }

    public static class GoalExtensions
    {
        public static string FullDescription(this Goal goal)
        {
            return goal.Target.ActivityType.AsVerb() + " " + goal.Target.TargetNumber + " " + goal.Target.Type.AsUnit() + " " + goal.Timeline.AsPostfix();
        }
    }
}