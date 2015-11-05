using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Website.Models;
using WindowsPhoneApp.Models;

namespace WindowsPhoneApp.Common
{
    interface IApi
    {
        Task<Account> GetAccount(string id);

        Task<Account> AccountFacebook(long id);

        Task<Account> AccountTwitter(long id);

        Task<Account> CreateAccount(Account account);

        Task<List<Team>> TeamsByAccount(string accountId);

        Task<List<Badge>> BadgesByAccount(string accountId);

        Task<Badge> GetBadge(long id);

        Task<Team> GetTeam(long teamId);

        Task<List<Account>> ByTeam(long teamId);

        Task<Account> EditAccount(Account account);

        Task<Team> EditTeam(Team team);

        void JoinTeam(string userId, long teamId);

        void LeaveTeam(string userId, long teamId);

        Task<Team> CreateTeam(Team team, string userId);

        Task<bool> IsAdmin(string userId, long teamId);

        Task<List<Badge>> AllBadges();

        Task<List<Attainment>> AttainmentsByAccount(string userId);

        Task<Tuple<Activity, List<Goal>, List<Attainment>>> SendActivity(Activity activity);

        Task<Activity> GetActivity(long id);

        Task<List<Badge>> EarnedBadges(string userId);
        Task<List<Badge>> UnearnedBadges(string userId);

        Task<List<Mood>> GetMoods();

        Task<Status> CreateStatus(Status status);

        Task<List<Status>> GetStatuses(string userId);

        Task<string> MoodDescription(long moodId);

        //Task<List<Account>> GetFriends(string accountId);

        Task<List<Account>> FriendsWithBadge(string accountId, long badgeId);

        Task Invite(string fromId, string toId, long teamId);

        Task<List<Team>> TeamsInvitedTo(string userId);

        Task<List<Account>> SearchAccounts(string[] words);

        Task<List<Account>> AccountsInvitedTo(long teamId);

        Task<List<Activity>> TeamActivities(long teamId);

        Task<List<Food>> TeamFoods(long teamId);

        Task<List<Attainment>> TeamAttainments(long teamId);

        Task<List<Status>> TeamStatuses(long teamId);

        Task<List<Goal>> GetGoals(string p);

        Task<Target> PostTarget(Target target);

        Task<Goal> PostGoal(Goal goal);

        Task DeleteAccount(string userId);

        Task<List<Tuple<Account, double>>> LeaderboardDistance(long teamId);

        Task<List<Tuple<Account, double>>> LeaderboardAttainments(long teamId);

        Task<Food> PostFood(Food food);

        Task<Activity> UpdateActivity(Activity activity);

        Task<Path> GetPath(long activityId);

        Task<Path> PostPath(Path path);

        Task<ActivityType> Predict(Activity activity);

        Task<List<Report>> GetReports(string id);

        Task GiveAdmin(string p1, string p2, long teamId);

        Task<List<Team>> SearchTeams(string[] words);

        Task DeleteGoal(long goalId);

        Task<Report> MiniReport(string id);

        Task<List<Membership>> GetMemberships(string id);
    }
}
