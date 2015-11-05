using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsPhoneApp.Models
{
    public enum TargetType { Steps, Distance, Duration }

    public static class TargetTypeExtensions
    {
        public static string AsUnit(this TargetType type)
        {
            switch (type)
            {
                case TargetType.Distance: return "miles";
                case TargetType.Duration: return "hours";
                case TargetType.Steps: return "steps";
            }
            throw new NotImplementedException();
        }
    }

    public enum TargetActivityType { Running, Walking, Jogging, Biking, General }

    public static class TargetActivityTypeExtensions
    {
        public static string AsVerb(this TargetActivityType type)
        {
            switch (type)
            {
                case TargetActivityType.General: return "Exercise";
                case TargetActivityType.Biking: return "Bike";
                case TargetActivityType.Jogging: return "Jog";
                case TargetActivityType.Running: return "Run";
                case TargetActivityType.Walking: return "Walk";
            }
            throw new NotImplementedException();
        }
    }

    public class Target
    {
        public long Id { get; set; }


        public TargetType Type { get; set; }


        public double TargetNumber { get; set; }


        public TargetActivityType ActivityType { get; set; }
    }
}