using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsPhoneApp.Models;

namespace WindowsPhoneApp.Common
{
    class MockApi : IApi
    {
        private Random random = new Random();
        private Dictionary<string, Account> accounts = new Dictionary<string, Account>();
        private Dictionary<long, Activity> activities = new Dictionary<long, Activity>();
        private Dictionary<long, Attainment> attainments = new Dictionary<long, Attainment>();
        private Dictionary<long, Badge> badges = new Dictionary<long, Badge>();
        private Dictionary<long, Exercise> exercises = new Dictionary<long, Exercise>();
        private Dictionary<long, Food> foods = new Dictionary<long, Food>();
        private Dictionary<long, Goal> goals = new Dictionary<long, Goal>();
        private Dictionary<long, Membership> memberships = new Dictionary<long, Membership>();
        private Dictionary<long, Path> paths = new Dictionary<long, Path>();
        private Dictionary<long, Target> targets = new Dictionary<long, Target>();
        private Dictionary<long, Team> teams = new Dictionary<long, Team>();
        private Dictionary<long, Workout> workouts = new Dictionary<long, Workout>();

        private Dictionary<long, Status> statuses = new Dictionary<long, Status>();
        private Dictionary<long, Mood> moods = new Dictionary<long, Mood>();

        public MockApi()
        {
            Seed();
        }

        private async Task Seed()
        {
            accounts.Add("wyayay", new Account()
            {
                Id = "wyayay",
                FullName = "Wyatt Goodin",
                PreferredName = "Wyatt",
                Zip = 80303,
                FacebookId = 802746313125281,
                Birthdate = new DateTime(1995, 5, 9),
                Weight = 170,
                Height = 73,
                Sex = false
            }.EliminateNull());


            accounts.Add("asdfd", new Account()
            {
                Id = "asdfd",
                FullName = "That Guy",
                PreferredName = "Guy",
                Zip = 80303,
                FacebookId = 8027413125281,
                Birthdate = new DateTime(1995, 5, 9),
                Weight = 170,
                Height = 73,
                Sex = false
            }.EliminateNull());


            accounts.Add("casdf", new Account()
            {
                Id = "casdf",
                FullName = "Cat Man",
                PreferredName = "Cat",
                Zip = 80303,
                FacebookId = 802741281,
                Birthdate = new DateTime(1995, 5, 9),
                Weight = 170,
                Height = 73,
                Sex = false
            }.EliminateNull());

            var account = (await AccountFacebook(802746313125281));
            string wyatt = account.Id;
            Account otherGuy = accounts["asdfd"];

            teams.Add(1, new Team()
                {
                    Id = 1,
                    Name = "Cowbell",
                    Deleted = false
                });
            teams.Add(2, new Team()
            {
                Id = 2,
                Name = "Better Team",
                Deleted = false
            });
            memberships.Add(1, new Membership
                {
                    Id = 1,
                    AccountId = wyatt,
                    TeamId = 1,
                    Status = MembershipStatus.Admin
                });
            memberships.Add(2, new Membership
            {
                Id = 2,
                AccountId = otherGuy.Id,
                TeamId = 1,
                Status = MembershipStatus.Member
            });

            memberships.Add(3, new Membership
            {
                Id = 3,
                AccountId = account.Id,
                TeamId = 2,
                Status = MembershipStatus.Invited
            });

            targets.Add(1, new Target
            {
                Id = 1,
                Type = TargetType.Steps,
                TargetNumber = 1800,
                ActivityType = TargetActivityType.General
            });

            goals.Add(1, new Goal
                {
                    Id = 1,
                    Description = "Test 1",
                    BeginDate = new DateTime(2015, 6, 10),
                    TargetId = 1,
                    Target = targets[1],
                    Status = GoalStatus.Completed,
                    AccountId = wyatt
                });

            badges.Add(1, new Badge
            {
                Id = 1,
                Description = "Test 1",
                TargetId = 1,
                Title = "Test 1"
            });


            badges.Add(2, new Badge
            {
                Id = 2,
                Description = "Test 2",
                TargetId = 1,
                Title = "Test 2"
            });

            attainments.Add(1, new Attainment
                {
                    Id = 1,
                    AccountId = wyatt,
                    BadgeId = 1,
                    DateEarned = DateTime.Now
                });
            account.Attainments.Add(attainments[1]);
            attainments.Add(5, new Attainment
            {
                Id = 5,
                AccountId = wyatt,
                BadgeId = 1,
                DateEarned = DateTime.Now
            });
            account.Attainments.Add(attainments[5]);
            attainments.Add(6, new Attainment
            {
                Id = 6,
                AccountId = wyatt,
                BadgeId = 1,
                DateEarned = DateTime.Now
            });
            account.Attainments.Add(attainments[6]);
            attainments.Add(7, new Attainment
            {
                Id = 7,
                AccountId = wyatt,
                BadgeId = 1,
                DateEarned = DateTime.Now
            });
            account.Attainments.Add(attainments[7]);
            attainments.Add(8, new Attainment
            {
                Id = 8,
                AccountId = wyatt,
                BadgeId = 1,
                DateEarned = DateTime.Now
            });
            account.Attainments.Add(attainments[8]);
            attainments.Add(9, new Attainment
            {
                Id = 9,
                AccountId = wyatt,
                BadgeId = 1,
                DateEarned = DateTime.Now
            });
            account.Attainments.Add(attainments[9]);

            attainments.Add(2, new Attainment
            {
                Id = 2,
                AccountId = otherGuy.Id,
                BadgeId = 1,
                DateEarned = DateTime.Now
            });
            otherGuy.Attainments.Add(attainments[2]);

            attainments.Add(3, new Attainment
            {
                Id = 3,
                AccountId = otherGuy.Id,
                BadgeId = 2,
                DateEarned = DateTime.Now
            });
            otherGuy.Attainments.Add(attainments[3]);

            activities.Add(1, new Activity
            {
                Id = 1,
                AccountId = wyatt,
                Type = ActivityType.Walking,
                Description = "TEst1",
                Steps = 1900,
                BeginTime = new DateTime(2013, 3, 10, 3, 0, 0),
                EndTime = new DateTime(2013, 3, 10, 4, 0, 0)
            });
            account.Activities.Add(activities[1]);

            foods.Add(1, new Food
            {
                Id = 1,
                AccountId = wyatt,
                Amount = 2,
                Date = new DateTime(2014, 3, 10, 3, 0, 0),
                FoodName = "Strawberry",
                Measurement = Measurement.FluidOunce
            });
            account.FoodEaten.Add(foods[1]);

            moods.Add(1, new Mood
                {
                    Id = 1,
                    Description = "Happy"
                });


            moods.Add(2, new Mood
            {
                Id = 2,
                Description = "Sad"
            });

            statuses.Add(1, new Status
                {
                    Id = 1,
                    AccountId = wyatt,
                    Date = new DateTime(2014, 3, 10, 3, 0, 10),
                    MoodId = 1
                });
        }

        public Task<Account> GetAccount(string id)
        {
            Account result;
            accounts.TryGetValue(id, out result);
            result.EliminateNull();
            return Task.FromResult(result);
        }

        public Task<Account> AccountFacebook(long id)
        {
            Account result;
            result = accounts.First(a => a.Value.FacebookId == id).Value;
            result.EliminateNull();
            return Task.FromResult(result);
        }

        public Task<Account> AccountTwitter(long id)
        {
            Account result;
            result = accounts.First(a => a.Value.TwitterId == id).Value;
            result.EliminateNull();
            return Task.FromResult(result);
        }

        public Task<Account> CreateAccount(Account account)
        {
            accounts.Add(account.Id, account);
            return Task.FromResult(account);
        }

        public Task<List<Team>> TeamsByAccount(string accountId)
        {
            return memberships.Values
                 .Where(m => m.AccountId == accountId && m.Status.IsMember())
                 .SelectAwait(m => GetTeam(m.TeamId));
        }

        public Task<List<Badge>> BadgesByAccount(string accountId)
        {
            return attainments.Values
                 .Where(m => m.AccountId == accountId)
                 .SelectAwait(m => GetBadge(m.BadgeId));
        }

        public Task<Badge> GetBadge(long id)
        {
            Badge result;
            badges.TryGetValue(id, out result);
            return Task.FromResult(result);
        }

        public Task<Team> GetTeam(long teamId)
        {
            Team result;
            teams.TryGetValue(teamId, out result);
            return Task.FromResult(result);
        }

        public Task<List<Account>> ByTeam(long teamId)
        {
            return memberships.Values
                 .Where(m => m.TeamId == teamId && m.Status.IsMember())
                 .SelectAwait(m => GetAccount(m.AccountId));
        }

        public Task<List<Account>> AccountsInvitedTo(long teamId)
        {
            return memberships.Values
                 .Where(m => m.TeamId == teamId && m.Status == MembershipStatus.Invited)
                 .SelectAwait(m => GetAccount(m.AccountId));
        }

        public async Task<Account> EditAccount(Account account)
        {
            Account current = await GetAccount(account.Id);
            if (current == null)
            {
                throw new NotImplementedException();
            }
            else
            {
                accounts[account.Id] = account;
                return account;
            }
        }

        public async Task<Team> EditTeam(Team team)
        {
            Team current = await GetTeam(team.Id);
            if (current == null)
            {
                throw new NotImplementedException();
            }
            else
            {
                teams[team.Id] = team;
                return team;
            }
        }

        public void JoinTeam(string userId, long teamId)
        {
            var existing = memberships.Values.FirstOrDefault(m => m.AccountId == userId && teamId == m.TeamId);
            if (existing != null && existing.Status != MembershipStatus.Banned)
            {
                existing.Status = MembershipStatus.Member;
                return;
            }
            var membership = new Membership
            {
                Id = random.Next(),
                AccountId = userId,
                TeamId = teamId,
                Status = MembershipStatus.Member
            };
            memberships.Add(membership.Id, membership);
        }

        public Task<List<Team>> AllTeams()
        {
            return Task.FromResult(teams.Values.ToList());
        }

        public void LeaveTeam(string userId, long teamId)
        {
            Membership membership = memberships.Values.SingleOrDefault(m => m.AccountId == userId && m.TeamId == teamId);
            if (membership == null)
            {
                throw new ArgumentException();
            }
            else
            {
                memberships.Remove(membership.Id);
            }
        }

        public Task<Team> CreateTeam(Team team, string userId)
        {
            team.Id = random.Next();
            teams.Add(team.Id, team);
            var membership = new Membership
            {
                Id = random.Next(),
                AccountId = userId,
                Status = MembershipStatus.Admin,
                TeamId = team.Id
            };
            memberships.Add(membership.Id, membership);
            return Task.FromResult(team);
        }


        public Task<bool> IsAdmin(string userId, long teamId)
        {
            var membership = memberships.Values.FirstOrDefault(m => m.AccountId == userId && m.TeamId == teamId);
            if (membership == null) return Task.FromResult(false);
            return Task.FromResult(membership.Status == MembershipStatus.Admin);
        }


        public Task<List<Badge>> AllBadges()
        {
            return Task.FromResult(badges.Values.ToList());
        }

        public Task<List<Attainment>> AttainmentsByAccount(string userId)
        {
            return Task.FromResult(attainments.Values.Where(a => a.AccountId == userId).ToList());
        }


        public Task<Tuple<Activity, List<Goal>, List<Attainment>>> SendActivity(Activity activity)
        {
            activity.Id = random.Next();
            activities.Add(activity.Id, activity);
            if (UserState.CurrentId == activity.AccountId)
            {
                UserState.ActiveAccount.Activities.Add(activity);
            }
            throw new NotImplementedException();
        }


        public Task<Activity> GetActivity(long id)
        {
            Activity result;
            result = activities.First(a => a.Value.Id == id).Value;
            return Task.FromResult(result);
        }


        public Task<List<Badge>> EarnedBadges(string userId)
        {
            return attainments.Values
                 .Where(m => m.AccountId == userId)
                 .SelectAwait(m => GetBadge(m.BadgeId));
        }

        public Task<List<Badge>> UnearnedBadges(string userId)
        {
            return Task.FromResult(badges.Values
                 .Where(m => !attainments.Values
                     .Where(a => a.AccountId == userId)
                     .Select(a => a.BadgeId).Contains(m.Id)).ToList());
        }


        public Task<List<Mood>> GetMoods()
        {
            return Task.FromResult(moods.Values.ToList());
        }


        public Task<Status> CreateStatus(Status status)
        {
            status.Id = random.Next();
            statuses.Add(status.Id, status);
            return Task.FromResult(status);
        }


        public Task<List<Status>> GetStatuses(string userId)
        {
            return Task.FromResult(statuses.Values.Where(s => s.AccountId == userId).ToList());
        }

        public Task<string> MoodDescription(long moodId)
        {
            return Task.FromResult(moods[moodId].Description);
        }

        public async Task<List<Account>> GetFriends(string accountId)
        {
            var teams = (await TeamsByAccount(accountId)).Select(t => t.Id);

            return await memberships.Values
                    .Where(m => teams.Contains(m.TeamId) && m.AccountId != accountId)
                    .SelectAwait(m => GetAccount(m.AccountId));
        }

        public async Task<List<Account>> FriendsWithBadge(string accountId, long badgeId)
        {
            var friends = await GetFriends(accountId);
            List<Account> accounts = new List<Account>();
            foreach (var friend in friends)
            {
                var badges = await BadgesByAccount(friend.Id);
                if (badges.Select(b => b.Id).Contains(badgeId))
                {
                    accounts.Add(friend);
                }
            }
            return accounts;
        }


        public Task Invite(string fromId, string toId, long teamId)
        {
            var invitation = new Membership
            {
                Id = random.Next(),
                AccountId = toId,
                TeamId = teamId,
                Status = MembershipStatus.Invited
            };
            memberships.Add(invitation.Id, invitation);
            return Task.Delay(0);
        }

        public Task<List<Team>> TeamsInvitedTo(string userId)
        {
            return memberships.Values
                 .Where(m => m.AccountId == userId && m.Status == MembershipStatus.Invited)
                 .SelectAwait(m => GetTeam(m.TeamId));
        }

        public Task<List<Account>> SearchAccounts(string[] words)
        {
            return Task.FromResult(accounts.Values.Where(a => a.FullName.Split().Any(w => words.Contains(w))).ToList());
        }

        public Task Invite(long teamId, string userId)
        {
            var membership = new Membership
            {
                TeamId = teamId,
                AccountId = userId,
                Id = random.Next(),
                Status = MembershipStatus.Invited
            };
            memberships.Add(membership.Id, membership);
            return Task.Delay(0);
        }

        public async Task<List<Activity>> TeamActivities(long teamId)
        {
            return (await (await ByTeam(teamId)).SelectAwait(a => GetAccount(a.Id))).SelectMany(a => a.Activities).ToList();
        }

        public async Task<List<Food>> TeamFoods(long teamId)
        {
            return (await (await ByTeam(teamId)).SelectAwait(a => GetAccount(a.Id))).SelectMany(a => a.FoodEaten).ToList();
        }

        public async Task<List<Attainment>> TeamAttainments(long teamId)
        {
            return (await (await ByTeam(teamId)).SelectAwait(a => GetAccount(a.Id))).SelectMany(a => a.Attainments).ToList();
        }

        public async Task<List<Status>> TeamStatuses(long teamId)
        {
            return (await (await (await ByTeam(teamId)).SelectAwait(a => GetAccount(a.Id))).SelectAwait(a => GetStatuses(a.Id))).SelectMany(x => x).ToList();
        }

        public Task<List<Goal>> GetGoals(string userId)
        {
            return Task.FromResult(goals.Values.Where(g => g.AccountId == userId).ToList());
        }

        public Task<Target> PostTarget(Target target)
        {
            target.Id = random.Next();
            targets.Add(target.Id, target);
            return Task.FromResult(target);
        }

        public Task<Goal> PostGoal(Goal goal)
        {
            goal.Id = random.Next();
            goals.Add(goal.Id, goal);
            return Task.FromResult(goal);
        }

        public Task<int> GetProgress(long goalId)
        {
            return Task.FromResult(0);
        }

        public Task DeleteAccount(string userId)
        {
            accounts.Remove(userId);
            return Task.Delay(0);
        }

        public Task<List<Tuple<Account, double>>> LeaderboardDistance(long id)
        {
            //return Task.FromResult(accounts.Values.ToList());
            throw new NotImplementedException();
        }

        public Task<List<Tuple<Account, double>>> LeaderboardAttainments(long id)
        {
            //return Task.FromResult(accounts.Values.ToList());
            throw new NotImplementedException();
        }


        public async Task<Food> PostFood(Food food)
        {
            food.Id = random.Next();
            foods.Add(food.Id, food);
            (await GetAccount(food.AccountId)).FoodEaten.Add(food);
            return food;
        }


        public Task<Activity> UpdateActivity(Activity activity)
        {
            throw new NotImplementedException();
        }


        public Task<Path> GetPath(long activityId)
        {
            throw new NotImplementedException();
        }


        public Task<Path> PostPath(Path path)
        {
            throw new NotImplementedException();
        }


        public Task<ActivityType> Predict(Activity activity)
        {
            throw new NotImplementedException();
        }


        public Task<List<Website.Models.Report>> GetReports(string id)
        {
            throw new NotImplementedException();
        }


        public Task GiveAdmin(string p1, string p2, long teamId)
        {
            throw new NotImplementedException();
        }


        public Task<List<Team>> SearchTeams(string[] words)
        {
            throw new NotImplementedException();
        }


        public Task DeleteGoal(long goalId)
        {
            throw new NotImplementedException();
        }


        public Task<Website.Models.Report> MiniReport(string id)
        {
            throw new NotImplementedException();
        }


        public Task<List<Membership>> GetMemberships(string id)
        {
            throw new NotImplementedException();
        }
    }
}
