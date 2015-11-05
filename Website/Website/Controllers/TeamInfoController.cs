using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Website.BusinessLogic;
using Website.Models;
using Website.ViewModels;

namespace Website.Controllers
{
    public class TeamInfoController : Controller
    {
        public ITeamLogic logic;
        public IAccountLogic aLogic;
        public IMembershipLogic MembershipLogic;
        public TeamInfoController(ITeamLogic t, IAccountLogic a, IMembershipLogic m)
        {
            logic = t;
            aLogic = a;
            MembershipLogic = m;
        }
        
        public ActionResult TeamInformation(long id)
        {
            Team team = logic.Get(id);
            List<Membership> mems = team.Memberships.ToList();
            List<Account> Members = new List<Account>();
            foreach (Membership m in mems)
            {
                if (m.Status.IsMember())
                {
                    Account account = aLogic.Get(m.AccountId);
                    Members.Add(account);
                }

            }
            ViewBag.Members = Members;
            return View(team);
        }

        public ActionResult TeamDashboard(long id)
        {
            Team team = logic.Get(id);
            List<Account> Members = GetMembers(team);
            string[] csvs = MemberActivityCSV(Members);
            TeamDashboardViewModel tdvm = new TeamDashboardViewModel();
            tdvm.Id = id;
            tdvm.Name = team.Name;
            tdvm.BarCSV = csvs[0];
            tdvm.PieCSV = csvs[1];
            tdvm.LineCSV = WeeklyReportCSV(Members);
            tdvm.Scatter = ScatterCSV(Members);
            tdvm.Distance = LeaderboardDistance(id);
            tdvm.Achievement = LeaderboardAttainments(id);
            return View(tdvm);
        }

        public ActionResult ViewAllTeams()
        {            
            List<Team> teams = logic.GetAll().ToList();
            return View(teams);
        }

        private string[] MemberActivityCSV(List<Account> members)
        {
            int[] days = new int[7];
            int[] type = new int[4];
            foreach (Account a in members)
            {
                foreach (Activity act in a.Activities)
                {
                    days[(int)act.EndTime.DayOfWeek]++;
                    type[(int)act.Type]++;
                }
            }
            string[] csv = new string[2];
            csv[0] = "Day of week, # Of Activities \r\n";
            for (int i = 0; i < 7; i++)
            {
                csv[0] += ((DayOfWeek)i).ToString() + "," + days[i] + "\r\n";
            }
            csv[1] = "Type, # Of Activities \r\n";
            for (int i = 0; i < 4; i++)
            {
                csv[1] += ((ActivityType)i).ToString() + "," + type[i] + "\r\n";
            }
            return csv;
            
        }
        private List<Account> GetMembers(Team t)
        {
            List<Membership> mems = MembershipLogic.GetByTeam(t.Id).ToList();
            List<Account> Members = new List<Account>();
            foreach (Membership m in mems)
            {
                if (m.Status.IsMember())
                {
                    Members.Add(aLogic.Get(m.AccountId));
                }
            }
            return Members;
        }

        private string WeeklyReportCSV(List<Account> members)
        {
            string csv = "Date";
            foreach (Account a in members)
            {
                csv += "," + a.FullName;
            }
            csv += "\r\n";

            for (int i = 6; i > -1; i--)
            {
                string DateString = DateTime.UtcNow.AddDays(-i).ToShortDateString();
                foreach (Account a in members)
                {
                    DateString +=",";
                    DateTime begin = DateTime.UtcNow.AddDays(-(i+1));
                    DateTime end = DateTime.UtcNow.AddDays(-i);
                    Report r = a.Reports.FirstOrDefault(q => q.Date>begin && q.Date<end);
                    if (r == null)
                    {

                    }
                    else
                    {
                        DateString += r.Steps;
                    }
                    
                    
                }
                DateString += "\r\n";
                csv += DateString;
            }

            return csv;
        }
        private List<ScatterDisplay> ScatterCSV(List<Account> members)
        {
            List<ScatterDisplay> scatter = new List<ScatterDisplay>();
            foreach (Account a in members)
            {
                double distance = 0;
                double duration = 0;
                foreach (Report r in a.Reports)
                {
                    distance += r.Distance;
                    duration += r.Duration;
                }
                if (a.Reports.Count == 0)
                {
                    scatter.Add(new ScatterDisplay { Name = a.FullName, Distance = 0, Duration = 0 });
                }
                else
                {
                    scatter.Add(new ScatterDisplay { Name = a.FullName, Distance = Math.Round(distance / a.Reports.Count,2), Duration = Math.Round(duration / a.Reports.Count,2) });
                }
            }
            
            return scatter;
        }

        private List<LeaderDisplay> LeaderboardDistance(long id)
        {
            //List<Account> members = new List<Account>();
            List<LeaderDisplay> leaders = new List<LeaderDisplay>();
            List<Membership> mems = MembershipLogic.GetByTeam(id).Where(e => e.Status == MembershipStatus.Admin || e.Status == MembershipStatus.Member).ToList();
            foreach (Membership m in mems)
            {
                Account a = aLogic.Get(m.AccountId);
                double total = 0;
                foreach (Activity act in a.Activities)
                {
                    total += act.Distance;
                }
                LeaderDisplay ld = new LeaderDisplay(a.FullName, total);
                leaders.Add(ld);
            }
            leaders = leaders.OrderByDescending(e => e.Value).Take(5).ToList();
            return leaders;
        }

        private List<LeaderDisplay> LeaderboardAttainments(long id)
        {
            //List<Account> members = new List<Account>();
            List<LeaderDisplay> leaders = new List<LeaderDisplay>();
            List<Membership> mems = MembershipLogic.GetByTeam(id).Where(e => e.Status == MembershipStatus.Admin || e.Status == MembershipStatus.Member).ToList();
            foreach (Membership m in mems)
            {
                Account a = aLogic.Get(m.AccountId);
                LeaderDisplay ld = new LeaderDisplay(a.FullName, a.Attainments.Count());
                leaders.Add(ld);
            }
            leaders = leaders.OrderByDescending(e => e.Value).Take(5).ToList();
            return leaders;

        }

    }
}
