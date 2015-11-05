using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Website.BusinessLogic;
using Website.Models;
using Website.ViewModels;

namespace Website.Controllers
{
    public class AccountInfoController : Controller
    {
        public IAccountLogic logic;
        public ITeamLogic tLogic;
        public IActivityLogic ActivityLogic;
        public IBadgeLogic BadgeLogic;
        public IAttainmentLogic AttainmentLogic;

        public AccountInfoController(IAccountLogic l, ITeamLogic t, IActivityLogic a, IAttainmentLogic attainment, IBadgeLogic badge)
        {
            logic = l;
            tLogic = t;
            ActivityLogic = a;
            BadgeLogic = badge;
            AttainmentLogic = attainment;
        }
        
        // GET: /AccountInfo/
        public ActionResult AccountInformation(string id)
        {
            Response.Cookies.Remove("UserId");
//
            Account a = logic.Get(id);
            List<Membership> mems = a.Memberships.ToList();
            List<TeamDisplay> teamDisplay = new List<TeamDisplay>();
            foreach (Membership m in mems)
            {
                if (m.Status.IsMember())
                {
                    Team team = tLogic.Get(m.TeamId);
                    TeamDisplay td = new TeamDisplay(team.Name, m.Status, team.Id);
                    teamDisplay.Add(td);
                }
            }
            AccountInformationViewModel aivm = new AccountInformationViewModel();
            aivm.Birthdate = a.Birthdate;
            aivm.EarnedBadges = EarnedBadges(a);
            aivm.FullName = a.FullName;
            aivm.Height = a.Height;
            aivm.PictureUrl = a.PictureUrl;
            aivm.PreferredName = a.PreferredName;
            aivm.Sex = a.Sex;
            aivm.Teams = teamDisplay;
            aivm.UnearnedBadges = UnearnedBadges(a);
            aivm.Weight = a.Weight;
            aivm.Zip = a.Zip;
            return View(aivm);
        }
        public ActionResult ViewAllUsers()
        {
            IQueryable<Account> accounts = logic.GetAll();
            return View(accounts);
        }

		public ActionResult Dashboard(string id)
		{
            if (id == null) id = User.Identity.GetUserId();
			Account user = logic.Get(id);
			PersonalDashboardViewModel model = GenerateViewModel(user);
			return View(model);
		}

        public PersonalDashboardViewModel GenerateViewModel(Account a)
        {
            PersonalDashboardViewModel pdvm = new PersonalDashboardViewModel();
            pdvm.Id = a.Id;
            pdvm.FullName = a.FullName;
            pdvm.PreferredName = a.PreferredName;
            pdvm.GoalCSV = GoalsCSV(a);
            string[] TwoDay = TwoDayActivityCSV(a);
            pdvm.TwoDayCSV = TwoDay[0];
            pdvm.ReportCSV = ReportCSV(a);
            string[] pie = TotalActivityCSV(a);
            pdvm.TotalCSV = pie[0];
            pdvm.BarchartInsight = TwoDay[1];
            pdvm.Closest = GoalInsight(a);
            pdvm.LinechartInsight = ReportInsight(a);
            pdvm.PiechartInsight = pie[1];
            pdvm.Prediction = MovingAverageDistance(a);
			pdvm.CanShare = a.Id == User.Identity.GetUserId();
            return pdvm;
        }
        
        
        
        private string GoalsCSV(Account a)
        {
            string goalCSV = "name,Remaining,Progress \r\n";
            
            foreach (Goal g in a.Goals)
            {
                if (g.Status == GoalStatus.Current)
                {
                    if (g.Target.Type == TargetType.Distance)
                    {
                        goalCSV += g.Description + "," + (g.Target.TargetNumber - g.Progress) + "," + g.Progress + "\r\n";
                    }
                    if (g.Target.Type == TargetType.Duration)
                    {
                        goalCSV += g.Description + "," + (g.Target.TargetNumber - g.Progress) + "," + g.Progress + "\r\n";
                    }
                    if (g.Target.Type == TargetType.Steps)
                    {
                        goalCSV += g.Description + "," + (g.Target.TargetNumber - g.Progress) + "," + g.Progress + "\r\n";
                    }
                }
            }
            return goalCSV;
        }

        private Goal GoalInsight(Account a)
        {
            return a.Goals.Where(u => u.Status == GoalStatus.Current).OrderByDescending(g => (g.Progress / g.Target.TargetNumber)).FirstOrDefault();
        }

        private string[] TwoDayActivityCSV(Account a)
        {
            DateTime TodayMidnight = DateTime.Today.ToUniversalTime();
            DateTime TomorrowMidnight= DateTime.Today.AddDays(1).ToUniversalTime();
            DateTime YesterdayMidnight = DateTime.Today.AddDays(-1).ToUniversalTime();
            List<Activity> Today = ActivityLogic.GetByDate(a.Id,TodayMidnight,TomorrowMidnight);
            List<Activity> Yesterday = ActivityLogic.GetByDate(a.Id, YesterdayMidnight, TodayMidnight);
            int[] YesterdayTotals = GenerateStepTotals(Yesterday);
            int[] TodayTotals = GenerateStepTotals(Today);
            string[] data = new string[2];
            data[0] = "Day,Walking,Jogging,Running,Biking \r\n"+
                "Yesterday," + YesterdayTotals[0].ToString() + "," + YesterdayTotals[1].ToString() + "," + YesterdayTotals[2].ToString() + "," + YesterdayTotals[3].ToString() + "\r\n" +
                "Today," + TodayTotals[0].ToString() + "," + TodayTotals[1].ToString() + "," + TodayTotals[2].ToString() + "," + TodayTotals[3].ToString() + "\r\n";
            data[1] = TwoDayInsights(TodayTotals, YesterdayTotals);
            return data;
        }
        private string TwoDayInsights(int[] today, int[] yesterday)
        {
            if (today[0] > yesterday[0])
            {
                return "You did more walking today than you did yesterday!";
            }
            else if (today[1] > yesterday[1])
            {
                return "You did more jogging today than you did yesterday!";
            }
            else if (today[2] > yesterday[2])
            {
                return "You did more running today than you did yesterday!";
            }
            else if (today[3] > yesterday[3])
            {
                return "You did more biking today than you did yesterday!";
            }
            else
            {
                return "You have not beaten your step totals from yesterday. Keep trying!";
            }

            
        }
        
        private int[] GenerateStepTotals(List<Activity> activities)
        {
            int Running = 0;
            int Jogging = 0;
            int Walking = 0;
            int Biking = 0;
            foreach (Activity a in activities)
            {
                switch (a.Type)
                {
                    case ActivityType.Biking:
                        Biking+=a.Steps;
                        break;
                    case ActivityType.Walking:
                        Walking+=a.Steps;
                        break;
                    case ActivityType.Jogging:
                        Jogging+= a.Steps;
                        break;
                    default:
                        Running+= a.Steps;
                        break;
                }
            }
            int[] totals = new int[] { Walking,Jogging,Running,Biking};
            return totals;
        }

        private string ReportCSV(Account a)
        {
            string Csv = "Date,Distance,Duration\r\n";
            DateTime last= new DateTime();
            foreach (Report r in a.Reports.OrderBy(e=> e.Date))
            {
                Csv += r.Date.ToShortDateString() + ","  + r.Distance + "," + r.Duration + "\r\n";
                last = r.Date;
            }
            
            return Csv;
        }

        private string ReportInsight(Account a)
        {
            double distance = 0;
            double duration = 0;
            foreach (Report r in a.Reports)
            {
                distance += r.Distance;
                duration += r.Duration;
            }

            if (distance + duration == 0)
            {
                return "You do not have any daily reports :(";
            }
            return "Your average all activity miles per hour is "+ Math.Round((distance/duration),2) + "mph";
        }
        private double MovingAverageDistance(Account a)
        {
            double TotalDistance = 0;
            foreach (Report r in a.Reports.OrderByDescending(e => e.Date).Take(5))
            {
                TotalDistance += r.Distance;
            }
            return Math.Max(Math.Round((TotalDistance / 5), 2),5);
        }
        private string[] TotalActivityCSV(Account a)
        {
            string[] text = new string[2];
            string[] generated = GenerateDurationTotals(a.Activities.ToList());
            text[0] = "Type,% of Total Activities \r\n" + generated[0];
            text[1] = generated[1];
            return text;
        }

        private string[] GenerateDurationTotals(List<Activity> activities)
        {
            TimeSpan Running = new TimeSpan();
            TimeSpan Jogging = new TimeSpan();
            TimeSpan Walking = new TimeSpan();
            TimeSpan Biking = new TimeSpan();
            foreach (Activity a in activities)
            {
                switch (a.Type)
                {
                    case ActivityType.Biking:
                        Biking += a.Duration;
                        break;
                    case ActivityType.Walking:
                        Walking += a.Duration;
                        break;
                    case ActivityType.Jogging:
                        Jogging += a.Duration;
                        break;
                    default:
                        Running += a.Duration;
                        break;
                }
            }
            string[] generated = new string[2];
            generated[0]= "Walking" + "," + Walking.TotalHours + "\r\n" +
                "Jogging" + "," + Jogging.TotalHours + "\r\n" +
                "Running" + "," + Running.TotalHours + "\r\n" +
                "Biking" + "," + Biking.TotalHours;
            if ((Walking.TotalHours + Jogging.TotalHours + Running.TotalHours + Biking.TotalHours) == 0)
            {
                generated[1] = "You don't have any tracked activity :(";
            }
            else if (Walking > Jogging && Walking > Running && Walking > Biking)
            {
                generated[1] = "Most of your tracked time is spent Walking!";
            }
            else if (Jogging > Running && Jogging > Biking)
            {
                generated[1] = "Most of your tracked time is spent Jogging!";
            }
            else if (Running > Biking)
            {
                generated[1] = "Most of your tracked time is spent Running!";
            }
            else
            {
                generated[1] = "Most of your tracked time is spent Biking!";
            }
            return generated;
        }
        private List<BadgeDisplay> EarnedBadges(Account account)
        {
            List<Attainment> items = AttainmentLogic.GetByAccount(account.Id).ToList();
            List<BadgeDisplay> earned = new List<BadgeDisplay>();
            foreach (Attainment a in items)
            {
                earned.Add(new BadgeDisplay(BadgeLogic.Get(a.BadgeId).Title, a.DateEarned.ToShortDateString()));
            }
            return earned;
        }
        private List<BadgeDisplay> UnearnedBadges(Account account)
        {
            List<Badge> items = AttainmentLogic.UnearnedBadges(account.Id).ToList();
            List<BadgeDisplay> UnearnedEarned = new List<BadgeDisplay>();
            foreach (Badge b in items)
            {
                UnearnedEarned.Add(new BadgeDisplay(b.Title, "--/--/----"));
            }
            return UnearnedEarned;
        }
    }
}