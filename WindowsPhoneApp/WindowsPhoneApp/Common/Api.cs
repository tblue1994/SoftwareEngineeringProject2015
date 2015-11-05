using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Website.Models;
using WindowsPhoneApp.Models;

namespace WindowsPhoneApp.Common
{
    class Api : IApi
    {
        private const string baseUrl = "http://se4.azurewebsites.net/";

        private static HttpClient client = new HttpClient();

        private Api()
        {
            client.BaseAddress = new Uri(baseUrl);
        }

        private static IApi self = new Api();
        public static IApi Do
        {
            get
            {
                return self;
            }
        }

        public async Task<Account> GetAccount(string id)
        {
            var response = await client.GetAsync("api/Account/Get/" + id);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Account>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task<Account> AccountFacebook(long id)
        {
            var response = await client.GetAsync("api/Account/Facebook/" + id);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Account>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task<Account> AccountTwitter(long id)
        {
            var response = await client.GetAsync("api/Account/Twitter/" + id);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Account>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task<Account> CreateAccount(Account account)
        {
            if (account.PictureUrl == null) account.PictureUrl = "<facebook>";
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Account/Post");
            request.Content = new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Account>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Team>> TeamsByAccount(string accountId)
        {
            var response = await client.GetAsync("api/Team/GetByAccountId/" + accountId);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Team>>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Badge>> BadgesByAccount(string accountId)
        {
            var response = await client.GetAsync("api/Attainment/GetByAccount/" + accountId);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return await JsonConvert.DeserializeObject<List<Attainment>>(content).SelectAwait(m => GetBadge(m.BadgeId));
            }
            else
            {
                return null;
            }
        }

        public async Task<Team> GetTeam(long teamId)
        {
            var response = await client.GetAsync("api/Team/Get/" + teamId);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Team>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Account>> ByTeam(long teamId)
        {
            var response = await client.GetAsync("api/Account/GetByTeamId/" + teamId);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Account>>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task<Account> EditAccount(Account account)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "api/Account/Put");
            request.Content = new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Account>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task<Team> EditTeam(Team team)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "api/Team/Put");
            request.Content = new StringContent(JsonConvert.SerializeObject(team), Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Team>(content);
            }
            else
            {
                return null;
            }
        }

        public async void JoinTeam(string userId, long teamId)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "api/Membership/Accept/" + teamId + "/" + userId);
            var response = await client.SendAsync(request);
        }

        public async void LeaveTeam(string userId, long teamId)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "api/Membership/Leave/" + teamId + "/" + userId);
            var response = await client.SendAsync(request);
        }

        public async Task<Team> CreateTeam(Team team, string userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Team/Post/" + userId);
            request.Content = new StringContent(JsonConvert.SerializeObject(team), Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Team>(content);
            }
            else
            {
                return null;
            }
        }


        public async Task<Badge> GetBadge(long id)
        {
            var response = await client.GetAsync("api/Badge/Get/" + id);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Badge>(content);
            }
            else
            {
                return null;
            }
        }


        public async Task<bool> IsAdmin(string userId, long teamId)
        {
            var response = await client.GetAsync("api/Membership/GetByAccountAndTeam/" + teamId + "/" + userId);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Membership>(content).Status == MembershipStatus.Admin;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Attainment>> AttainmentsByAccount(string userId)
        {
            var response = await client.GetAsync("api/Attainment/GetByAccount/" + userId);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Attainment>>(content);
            }
            else
            {
                return null;
            }
        }


        public async Task<Tuple<Activity, List<Goal>, List<Attainment>>> SendActivity(Activity activity)
        {
            if (activity.Description == null) activity.Description = "<placeholder>";

            var request = new HttpRequestMessage(HttpMethod.Post, "api/Activity/Post");
            request.Content = new StringContent(JsonConvert.SerializeObject(activity), Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Tuple<Activity, List<Goal>, List<Attainment>>>(content);
            }
            else
            {
                return null;
            }
        }


        public async Task<Activity> GetActivity(long id)
        {
            var response = await client.GetAsync("api/Activity/Get/" + id);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Activity>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Badge>> AllBadges()
        {
            var response = await client.GetAsync("api/Badge/GetAll");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Badge>>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Badge>> EarnedBadges(string userId)
        {
            var response = await client.GetAsync("api/Badge/GetEarnedBadges/" + userId);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Badge>>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Badge>> UnearnedBadges(string userId)
        {
            var response = await client.GetAsync("api/Badge/GetUnearnedBadges/" + userId);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Badge>>(content);
            }
            else
            {
                return null;
            }
        }


        public async Task<List<Mood>> GetMoods()
        {
            var response = await client.GetAsync("api/Mood/GetAll");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Mood>>(content);
            }
            else
            {
                return null;
            }
        }


        public async Task<Status> CreateStatus(Status status)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Status/Post");
            request.Content = new StringContent(JsonConvert.SerializeObject(status), Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Status>(content);
            }
            else
            {
                return null;
            }
        }


        public async Task<List<Status>> GetStatuses(string userId)
        {
            var response = await client.GetAsync("api/Status/GetByAccount/" + userId);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Status>>(content);
            }
            else
            {
                return null;
            }
        }


        public async Task<string> MoodDescription(long moodId)
        {
            var response = await client.GetAsync("api/Mood/Get/" + moodId);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Mood>(content).Description;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Account>> FriendsWithBadge(string accountId, long badgeId)
        {
            var response = await client.GetAsync("api/Account/GetTeammateWithBadges/" + badgeId + "/" + accountId);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Account>>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task Invite(string fromId, string toId, long teamId)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "api/Membership/Invite/" + teamId + "/" + toId + "/" + fromId);
            var response = await client.SendAsync(request);
        }

        public async Task<List<Team>> TeamsInvitedTo(string userId)
        {
            var response = await client.GetAsync("api/Membership/ByAccount/" + userId);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return await JsonConvert.DeserializeObject<List<Membership>>(content)
                    .Where(m => m.Status == MembershipStatus.Invited)
                    .SelectAwait(m => GetTeam(m.TeamId));
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Account>> SearchAccounts(string[] words)
        {
            var response = await client.GetAsync("api/Account/Search/" + string.Join(" ", words));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Account>>(content);
            }
            else
            {
                return null;
            }
        }


        public async Task<List<Account>> AccountsInvitedTo(long teamId)
        {
            var response = await client.GetAsync("api/Account/GetAccountsInvitedToTeam/" + teamId);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Account>>(content);
            }
            else
            {
                return null;
            }
        }


        public async Task<List<Activity>> TeamActivities(long teamId)
        {
            var response = await client.GetAsync("api/Activity/GetByTeam/" + teamId);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Activity>>(content);
            }
            else
            {
                return null;
            }
        }


        public async Task<List<Food>> TeamFoods(long teamId)
        {
            var response = await client.GetAsync("api/Food/GetByTeam/" + teamId);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Food>>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Attainment>> TeamAttainments(long teamId)
        {
            var response = await client.GetAsync("api/Attainment/GetByTeam/" + teamId);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Attainment>>(content);
            }
            else
            {
                return null;
            }
        }


        public async Task<List<Status>> TeamStatuses(long teamId)
        {
            var response = await client.GetAsync("api/Status/GetByTeam/" + teamId);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Status>>(content);
            }
            else
            {
                return null;
            }
        }


        public async Task<List<Goal>> GetGoals(string userId)
        {
            var response = await client.GetAsync("api/Goal/GetByAccount/" + userId);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Goal>>(content);
            }
            else
            {
                return null;
            }
        }


        public async Task<Target> PostTarget(Target target)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Target/Post");
            request.Content = new StringContent(JsonConvert.SerializeObject(target), Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Target>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task<Goal> PostGoal(Goal goal)
        {
            if (goal.Description == null) goal.Description = "<temp>";
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Goal/Post");
            request.Content = new StringContent(JsonConvert.SerializeObject(goal), Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Goal>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task DeleteAccount(string userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "api/Account/Delete/" + userId);
            await client.SendAsync(request);
        }

        public async Task<List<Tuple<Account, double>>> LeaderboardDistance(long teamId)
        {
            var response = await client.GetAsync("api/Account/LeaderboardDistance/" + teamId);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Tuple<Account, double>>>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Tuple<Account, double>>> LeaderboardAttainments(long teamId)
        {
            var response = await client.GetAsync("api/Account/LeaderboardAttainments/" + teamId);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Tuple<Account, double>>>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task<Food> PostFood(Food food)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Food/Post");
            request.Content = new StringContent(JsonConvert.SerializeObject(food), Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Food>(content);
            }
            else
            {
                return null;
            }
        }


        public async Task<Activity> UpdateActivity(Activity activity)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "api/Activity/Put");
            request.Content = new StringContent(JsonConvert.SerializeObject(activity), Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Activity>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task<Path> GetPath(long activityId)
        {
            var response = await client.GetAsync("api/Path/GetByActivity/" + activityId);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Path>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task<Path> PostPath(Path path)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Path/Post");
            request.Content = new StringContent(JsonConvert.SerializeObject(new { Data = path.Data.Select(a => (int)a).ToArray(), ActivityId = path.ActivityId }), Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Path>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task<ActivityType> Predict(Activity activity)
        {
            if (activity.Description == null) activity.Description = "<temporary>";
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Prediction/Test/" + UserState.CurrentId);
            request.Content = new StringContent(JsonConvert.SerializeObject(activity), Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ActivityType>(content);
            }
            else
            {
                return ActivityType.Walking;
            }
        }


        public async Task<List<Report>> GetReports(string id)
        {
            var response = await client.GetAsync("api/Report/GetByAccount/" + id);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Report>>(content);
            }
            else
            {
                return null;
            }
        }


        public async Task GiveAdmin(string fromId, string toId, long teamId)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "api/Membership/GiveAdminStatus/" + teamId + "/" + toId + "/" + fromId);
            var response = await client.SendAsync(request);
        }


        public async Task<List<Team>> SearchTeams(string[] words)
        {
            var response = await client.GetAsync("api/Team/Search/" + string.Join(" ", words));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Team>>(content);
            }
            else
            {
                return null;
            }
        }


        public async Task DeleteGoal(long goalId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "api/Goal/Delete/" + goalId);
            var response = await client.SendAsync(request);
        }


        public async Task<Report> MiniReport(string id)
        {
            var response = await client.GetAsync("api/Report/GetMiniReportByAccount/" + id);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Report>(content);
            }
            else
            {
                return null;
            }
        }


        public async Task<List<Membership>> GetMemberships(string id)
        {
            var response = await client.GetAsync("api/Membership/ByAccount/" + id);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Membership>>(content);
            }
            else
            {
                return null;
            }
        }
    }
}
