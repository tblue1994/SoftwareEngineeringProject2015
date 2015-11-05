namespace Website.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.Linq;
    using Website.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Website.Models.WebsiteContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Website.Models.WebsiteContext";
        }

        protected override void Seed(WebsiteContext context)
        {
            var userManager = new UserManager<Account>(new UserStore<Account>(context));

            var accounts = new List<Account>
            {
             new Account()
             {
                 FullName = "Trevor Slawnyk",
                 PreferredName = "Trevor",
                 Zip = 68456,
                 FacebookId = 4929447011515,
                 TwitterId = 2565509796,
                 Birthdate = new DateTime(1994, 6, 22),
                 Weight = 250,
                 Height = 73,
                 Sex = false
             },
             new Account()
             {
                 FullName = "Hanna Rogoz",
                 PreferredName = "Hanna",
                 Zip = 60517,
                 FacebookId = 1590114489,
                 Birthdate = new DateTime(1995, 8, 4),
                 Weight = 170,
                 Height = 69,
                 Sex = true
             } ,
             new Account()
             {
                 FullName = "Wyatt Goodin",
                 PreferredName = "Wyatt",
                 Zip = 80303,
                 FacebookId = 802746313125281,
                 Birthdate = new DateTime(1995, 5, 9),
                 Weight = 170,
                 Height = 73,
                 Sex = false
             },
             new Account()
             {
                 FullName = "Heitor Castro",
                 PreferredName = "Heitor",
                 Zip = 68508,
                 FacebookId = 10152669987526046,
                 Birthdate = new DateTime(1995, 2, 9),
                 Weight = 145,
                 Height = 66,
                 Sex = false
             },
             new Account()
             {
                 FullName = "Charlie Brown",
                 PreferredName = "Chuck",
                 Zip = 78657,
                 FacebookId = 1587488569585,
                 Birthdate = new DateTime(1999, 1, 1),
                 Weight = 100,
                 Height = 36,
                 Sex = false
             },
             new Account()
             {
                 FullName = "Ian Cottingham",
                 PreferredName = "Ian",
                 Zip = 68508,
                 FacebookId = 1458796589,
                 Birthdate = new DateTime(1980, 2, 9),
                 Weight = 190,
                 Height = 72,
                 Sex = false
             },
             new Account()
             {
                 FullName = "Taylor Mcgyver",
                 PreferredName = "Taylor",
                 Zip = 68508,
                 FacebookId = 245879658,
                 Birthdate = new DateTime(1988, 9, 9),
                 Weight = 145,
                 Height = 66,
             }
            };
            accounts.ForEach(s =>
            {
                s.UserName = s.PreferredName;
                userManager.Create(s);
            });
            context.SaveChanges();

            var teams = new List<Team>
            {
                new Team()
                {
                    Id=1,
                    Name = "Cowbell",
                    Deleted = false
                },
                new Team()
                {
                    Id=2,
                    Name = "Not Cowbell",
                    Deleted = false
                },
                new Team()
                {
                    Id=3,
                    Name = "THIS TEAM HAS BANNED MCGYVER",
                    Deleted = false
                }
            };
            teams.ForEach(s => context.Teams.AddOrUpdate(p => p.Id, s));
            context.SaveChanges();

            var memberships = new List<Membership>
            {
                new Membership { 
                    Id = 1,
                    AccountId = context.Users.Single(s => s.FacebookId == 4929447011515).Id, 
                    TeamId = 1, 
                    Status = MembershipStatus.Member,
                    Date= DateTime.Now
                },
                 new Membership { 
                    Id = 2,
                    AccountId = context.Users.Single(s => s.FacebookId == 1590114489).Id, 
                    TeamId = 1, 
                    Status = MembershipStatus.Member,
                    Date= DateTime.Now
                },                            
                 new Membership { 
                    Id = 3,
                    AccountId = context.Users.Single(s => s.FacebookId == 802746313125281).Id, 
                    TeamId = 1, 
                    Status = MembershipStatus.Admin,
                    Date= DateTime.Now
                },                            
                 new Membership { 
                    Id = 4,
                    AccountId = context.Users.Single(s => s.FacebookId == 10152669987526046).Id, 
                    TeamId = 1, 
                    Status = MembershipStatus.Member,
                    Date= DateTime.Now
                },
                 new Membership {
                    Id = 5,
                    AccountId = context.Users.Single(s => s.FacebookId == 1587488569585).Id, 
                    TeamId = 2, 
                    Status = MembershipStatus.Member,
                    Date= DateTime.Now
                },
                 new Membership { 
                    Id = 6,
                    AccountId = context.Users.Single(s => s.FacebookId == 1458796589).Id, 
                    TeamId = 2, 
                    Status = MembershipStatus.Admin,
                    Date= DateTime.Now
                },
                 new Membership { 
                    Id = 7,
                    AccountId = context.Users.Single(s => s.FacebookId == 1458796589).Id, 
                    TeamId = 3, 
                    Status = MembershipStatus.Admin,
                    Date= DateTime.Now
                },
                 new Membership { 
                    Id = 8,
                    AccountId = context.Users.Single(s => s.FacebookId == 245879658).Id, 
                    TeamId = 3, 
                    Status = MembershipStatus.Banned,
                    Date= DateTime.Now
                },
                new Membership { 
                    Id = 9,
                    AccountId = context.Users.Single(s => s.FacebookId == 245879658).Id, 
                    TeamId = 2, 
                    Status = MembershipStatus.Invited,
                    Date= DateTime.Now
                }
            };

            foreach (Membership e in memberships)
            {
                var MembershipInDataBase = context.Memberships.Where(
                    s =>
                         s.AccountId == e.AccountId &&
                         s.TeamId == e.TeamId).SingleOrDefault();
                if (MembershipInDataBase == null)
                {
                    context.Memberships.Add(e);
                }
            }
            context.SaveChanges();

            var Targets = new List<Target>
            {
                new Target { 
                    Id = 1,
                    Type = TargetType.Distance,
                    TargetNumber = 26.2,
                    ActivityType = TargetActivityType.Running

                },
                new Target { 
                    Id = 2,
                    Type = TargetType.Steps,
                    TargetNumber = 10000,
                    ActivityType = TargetActivityType.General
                },
                new Target { 
                    Id = 3,
                    Type = TargetType.Distance,
                    TargetNumber = 15248,
                    ActivityType = TargetActivityType.Running
                },
                new Target { 
                    Id = 4,
                    Type = TargetType.Duration,
                    TargetNumber = 3,
                    ActivityType = TargetActivityType.Biking
                },
                new Target { 
                    Id = 5,
                    Type = TargetType.Steps,
                    TargetNumber = 37280000,
                    ActivityType = TargetActivityType.Walking
                },
                new Target { 
                    Id = 6,
                    Type = TargetType.Distance,
                    TargetNumber = 3.14,
                    ActivityType = TargetActivityType.Running
                },
                new Target { 
                    Id = 7,
                    Type = TargetType.Duration,
                    TargetNumber = 50,
                    ActivityType = TargetActivityType.Running
                },
                 new Target { 
                    Id = 8,
                    Type = TargetType.Steps,
                    TargetNumber = 5000,
                    ActivityType = TargetActivityType.Walking
                },
                new Target { 
                    Id = 9,
                    Type = TargetType.Distance,
                    TargetNumber = 13,
                    ActivityType = TargetActivityType.Running
                },
                 new Target { 
                    Id = 10,
                    Type = TargetType.Steps,
                    TargetNumber = 50000,
                    ActivityType = TargetActivityType.Walking
                },
                  new Target { 
                    Id = 11,
                    Type = TargetType.Distance,
                    TargetNumber = 3.1,
                    ActivityType = TargetActivityType.Jogging
                },
                 new Target { 
                    Id = 12,
                    Type = TargetType.Distance,
                    TargetNumber = 30,
                    ActivityType = TargetActivityType.Biking
                },
                new Target { 
                    Id = 13,
                    Type = TargetType.Steps,
                    TargetNumber = 10000,
                    ActivityType = TargetActivityType.Walking
                },
                new Target { 
                    Id = 14,
                    Type = TargetType.Duration,
                    TargetNumber = 1,
                    ActivityType = TargetActivityType.Jogging
                },
                 new Target { 
                    Id = 15,
                    Type = TargetType.Duration,
                    TargetNumber = 1,
                    ActivityType = TargetActivityType.Biking
                },
                 new Target { 
                    Id = 16,
                    Type = TargetType.Distance,
                    TargetNumber = 1000,
                    ActivityType = TargetActivityType.Running
                },
                 new Target { 
                    Id = 17,
                    Type = TargetType.Distance,
                    TargetNumber = 500,
                    ActivityType = TargetActivityType.Biking
                },
                new Target { 
                    Id = 18,
                    Type = TargetType.Duration,
                    TargetNumber = .5,
                    ActivityType = TargetActivityType.Running
                },
                new Target { 
                    Id = 19,
                    Type = TargetType.Steps,
                    TargetNumber = 15000,
                    ActivityType = TargetActivityType.Jogging
                },
                new Target { 
                    Id = 20,
                    Type = TargetType.Duration,
                    TargetNumber = 2,
                    ActivityType = TargetActivityType.Jogging
                },
                 new Target { 
                    Id = 21,
                    Type = TargetType.Distance,
                    TargetNumber = 15,
                    ActivityType = TargetActivityType.Running
                },
                new Target { 
                    Id = 22,
                    Type = TargetType.Duration,
                    TargetNumber = 4,
                    ActivityType = TargetActivityType.Biking
                },
                  new Target { 
                    Id = 23,
                    Type = TargetType.Distance,
                    TargetNumber = 100,
                    ActivityType = TargetActivityType.Jogging
                },
                 new Target { 
                    Id = 24,
                    Type = TargetType.Distance,
                    TargetNumber = 2776,
                    ActivityType = TargetActivityType.Biking
                },
                 new Target { 
                    Id = 25,
                    Type = TargetType.Steps,
                    TargetNumber = 1000000,
                    ActivityType = TargetActivityType.Walking
                },
                 new Target { 
                    Id = 26,
                    Type = TargetType.Steps,
                    TargetNumber = 10000,
                    ActivityType = TargetActivityType.Walking
                },
                 new Target { 
                    Id = 27,
                    Type = TargetType.Distance,
                    TargetNumber = 10000,
                    ActivityType = TargetActivityType.Running
                },
                 new Target { 
                    Id = 28,
                    Type = TargetType.Duration,
                    TargetNumber = 10000,
                    ActivityType = TargetActivityType.Jogging
                },
                 new Target { 
                    Id = 29,
                    Type = TargetType.Steps,
                    TargetNumber = 10000,
                    ActivityType = TargetActivityType.Walking
                }

            };

            Targets.ForEach(s => context.Targets.AddOrUpdate(p => p.Id, s));
            context.SaveChanges();

            var goals = new List<Goal>
            {
                new Goal()
                {
                    Id=1,
                    BeginDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(7),
                    TargetId=1,
                    AccountId=context.Users.Single(s => s.FacebookId == 802746313125281).Id,
                    Status = GoalStatus.Current,
                    Progress=0

                },
                new Goal()
                {
                    Id=2,
                    BeginDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(7),
                    TargetId=2,
                    AccountId=context.Users.Single(s => s.FacebookId == 802746313125281).Id,
                    Status = GoalStatus.Current,
                    Progress=0
                },
                new Goal()
                {
                    Id=3,
                    BeginDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(7),
                    TargetId=3,
                    AccountId=context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Status = GoalStatus.Current,
                    Progress=1500
                },
                new Goal()
                {
                    Id=3,
                    BeginDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(7),
                    TargetId=26,
                    AccountId=context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Status = GoalStatus.Current,
                    Progress=2222
                },
                new Goal()
                {
                    Id=3,
                    BeginDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(7),
                    TargetId=27,
                    AccountId=context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Status = GoalStatus.Current,
                    Progress=5000
                },
                new Goal()
                {
                    Id=3,
                    BeginDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(7),
                    TargetId=28,
                    AccountId=context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Status = GoalStatus.Current,
                    Progress=5677
                },
                new Goal()
                {
                    Id=3,
                    BeginDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(7),
                    TargetId=29,
                    AccountId=context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Status = GoalStatus.Current,
                    Progress=2345
                }
            };
            goals.ForEach(s => context.Goals.AddOrUpdate(p => p.Id, s));
            context.SaveChanges();

            var badges = new List<Badge>
            {
                new Badge { 
                    Id = 1,
                    Description = "You've run 26.2 miles", 
                    TargetId = 1,    
                    Title = "Boston Ready!",
                    Timeline=BadgeTimeline.SingleActivity
                },
                new Badge { 
                    Id = 2,
                    Title = "Walk it out!",
                    Description = "You've taken 10,000 steps today", 
                    TargetId = 2,
                    Timeline=BadgeTimeline.Daily   
                },
                new Badge { 
                    Id = 3,
                    Title = "Run Forest, run!",
                    Description = "Your run lasted more than 5 miles", 
                    TargetId = 3,
                    Timeline=BadgeTimeline.SingleActivity   
                },
                new Badge { 
                    Id = 4,
                    Title = "Wheels on fire!",
                    Description = "You biked for more than 3 hours", 
                    TargetId = 4,
                    Timeline=BadgeTimeline.SingleActivity
                },
                 new Badge { 
                    Id = 5,
                    Title = "Grab your passport!",
                    Description = "You've walked the length of the Great Wall of China", 
                    TargetId = 5,
                    Timeline=BadgeTimeline.Cumulative 
                },
                new Badge { 
                    Id = 6,
                    Title = "It's pie time!",
                    Description = "Your run lasted more than 3.14 miles!", 
                    TargetId = 6,
                    Timeline=BadgeTimeline.SingleActivity  
                },
                new Badge { 
                    Id = 7,
                    Title = "Over the hill!",
                    Description = "Congrats, you've logged more than 50 running hours!", 
                    TargetId = 7,
                    Timeline=BadgeTimeline.Cumulative  
                },
                new Badge { 
                    Id = 8,
                    Title = "Rev it up!",
                    Description = "Who needs a car when they can walk 5000 steps like you?", 
                    TargetId = 8,
                    Timeline=BadgeTimeline.Daily  
                },
                new Badge { 
                    Id = 9,
                    Title = "Half marathon horizon!",
                    Description = "Way to finish a 13 mile run!", 
                    TargetId = 9,
                    Timeline=BadgeTimeline.Daily 
                },
                new Badge { 
                    Id = 10,
                    Title = "Hot shot!",
                    Description = "Congrats on 50,000 steps this week!", 
                    TargetId = 10,
                    Timeline=BadgeTimeline.Weekly
                },
                  new Badge { 
                    Id = 11,
                    Title = "5k in the distance!",
                    Description = "Nice job jogging 13.1 miles", 
                    TargetId = 11,
                    Timeline=BadgeTimeline.SingleActivity
                },
                 new Badge { 
                    Id = 12,
                    Title = "Speed Demon",
                    Description = "Biked 30 miles today, nice job", 
                    TargetId = 12,
                    Timeline=BadgeTimeline.Daily
                },
                new Badge { 
                    Id = 13,
                    Title = "You're on fire!",
                    Description = "You've taken more than 10,000 steps today!", 
                    TargetId = 13,
                    Timeline=BadgeTimeline.Daily
                },
                 new Badge { 
                    Id = 14,
                    Title = "Jog it out!",
                    Description = "You've jogged more than an hour today, nice work!", 
                    TargetId = 14,
                    Timeline=BadgeTimeline.Daily
                },
                new Badge { 
                    Id = 15,
                    Title = "Certified Cyclist",
                    Description = "You biked more than an hour today!", 
                    TargetId = 15,
                    Timeline=BadgeTimeline.Daily
                },
                 new Badge { 
                    Id = 16,
                    Title = "Inspiration Station",
                    Description = "You just logged your 1000th running mile!", 
                    TargetId = 16,
                    Timeline=BadgeTimeline.Cumulative
                },
                new Badge { 
                    Id = 17,
                    Title = "Pedal to the Metal",
                    Description = "You've just logged 500 biking miles", 
                    TargetId = 17,
                    Timeline=BadgeTimeline.Cumulative
                },
                new Badge { 
                    Id = 18,
                    Title = "Up up and away!",
                    Description = "Nice job on a 30 minute run", 
                    TargetId = 18,
                    Timeline=BadgeTimeline.SingleActivity
                },
                new Badge { 
                    Id = 19,
                    Title = "Step Master!",
                    Description = "You took over 15,000 steps during your jog", 
                    TargetId = 19,
                    Timeline=BadgeTimeline.SingleActivity
                },
                new Badge { 
                    Id = 20,
                    Title = "Glisten up!",
                    Description = "Way to do work on your 2 hour jog", 
                    TargetId = 20,
                    Timeline=BadgeTimeline.SingleActivity
                },
                 new Badge { 
                    Id = 21,
                    Title = "Usain Bolt worthy!",
                    Description = "You just ran over 15 miles", 
                    TargetId = 21,
                    Timeline=BadgeTimeline.SingleActivity
                },
                 new Badge { 
                    Id = 22,
                    Title = "Lance ArmSTRONG!",
                    Description = "Impressive 4 hour bike ride", 
                    TargetId = 22,
                    Timeline=BadgeTimeline.SingleActivity
                },
                new Badge { 
                    Id = 23,
                    Title = "Speed Racer",
                    Description = "You've logged 100 miles jogged this month", 
                    TargetId = 23,
                    Timeline=BadgeTimeline.Monthly
                },
                   new Badge { 
                    Id = 24,
                    Title = "Tour de France Ready!",
                    Description = "You've logged more than 2776 miles biking", 
                    TargetId = 24,
                    Timeline=BadgeTimeline.Cumulative
                },
                new Badge { 
                    Id = 25,
                    Title = "Trooper Alert",
                    Description = "You've taken more than 1000000 walking steps", 
                    TargetId = 25,
                    Timeline=BadgeTimeline.Cumulative
                }
            };

            badges.ForEach(s => context.Badges.AddOrUpdate(p => p.Id, s));
            context.SaveChanges();

            var attainments = new List<Attainment>
            {
                new Attainment { 
                    Id = 1,
                    BadgeId = 1,
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    DateEarned = DateTime.UtcNow    
                },
                 new Attainment { 
                    Id = 2,
                    BadgeId = 2,
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    DateEarned = DateTime.UtcNow     
                }, 
                new Attainment { 
                    Id = 3,
                    BadgeId = 1,
                    AccountId= context.Users.Single(s => s.FacebookId == 802746313125281).Id,
                    DateEarned = DateTime.UtcNow     
                },
                 new Attainment { 
                    Id = 4,
                    BadgeId = 2,
                    AccountId= context.Users.Single(s => s.FacebookId == 802746313125281).Id,
                    DateEarned = DateTime.UtcNow     
                }
                 
            };

            attainments.ForEach(s => context.Attainments.AddOrUpdate(p => p.Id, s));
            context.SaveChanges();

            var activities = new List<Activity>
            {
                new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Walking,
                    Description="6 day ago Walk",
                    Steps= 40000,
                    Distance = 4,
                    BeginTime= DateTime.UtcNow.AddMinutes(-80).AddDays(-6),
                    EndTime = DateTime.UtcNow.AddDays(-6)    
                },
                new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Jogging,
                    Description="6 day ago Jog",
                    Steps= 1090,
                    Distance = 1,
                    BeginTime= DateTime.UtcNow.AddMinutes(-10).AddDays(-6),
                    EndTime = DateTime.UtcNow.AddDays(-6)
                },
                new Activity {
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Running,
                    Description="6 day ago Run",
                    Steps= 4000,
                    Distance = 3.5,
                    BeginTime= DateTime.UtcNow.AddMinutes(-30).AddDays(-6),
                    EndTime = DateTime.UtcNow.AddDays(-6)    
                },
                new Activity {
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Biking,
                    Description="6 day ago Bike",
                    Steps= 0,
                    Distance =5.5,
                    BeginTime= DateTime.UtcNow.AddMinutes(-30).AddDays(-6),
                    EndTime = DateTime.UtcNow.AddDays(-6)   
                },
                new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Walking,
                    Description="5 day ago Walk",
                    Steps= 27000,
                    Distance = 2.75,
                    BeginTime= DateTime.UtcNow.AddMinutes(-40).AddDays(-5),
                    EndTime = DateTime.UtcNow.AddDays(-5)    
                },
                new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Jogging,
                    Description="5 day ago Jog",
                    Steps= 5000,
                    Distance = 2.7,
                    BeginTime= DateTime.UtcNow.AddMinutes(-33).AddDays(-5),
                    EndTime = DateTime.UtcNow.AddDays(-5)
                },
                new Activity {
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Running,
                    Description="5 day ago Run",
                    Steps= 8000,
                    Distance = 6,
                    BeginTime= DateTime.UtcNow.AddHours(-1).AddDays(-5),
                    EndTime = DateTime.UtcNow.AddDays(-5)    
                },
                new Activity {
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Biking,
                    Description="5 day ago Bike",
                    Steps= 0,
                    Distance =10,
                    BeginTime= DateTime.UtcNow.AddHours(-1).AddDays(-5),
                    EndTime = DateTime.UtcNow.AddDays(-5)   
                },
                new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Walking,
                    Description="4 day ago Walk",
                    Steps= 30000,
                    Distance = 3,
                    BeginTime= DateTime.UtcNow.AddMinutes(-63).AddDays(-4),
                    EndTime = DateTime.UtcNow.AddDays(-4)    
                },
                new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Jogging,
                    Description="4 day ago Jog",
                    Steps= 4000,
                    Distance = 2,
                    BeginTime= DateTime.UtcNow.AddMinutes(-25).AddDays(-4),
                    EndTime = DateTime.UtcNow.AddDays(-4)
                },
                new Activity {
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Running,
                    Description="4 day ago Run",
                    Steps= 6000,
                    Distance = 4.6,
                    BeginTime= DateTime.UtcNow.AddMinutes(-48).AddDays(-4),
                    EndTime = DateTime.UtcNow.AddDays(-4)    
                },
                new Activity {
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Biking,
                    Description="4 day ago Bike",
                    Steps= 0,
                    Distance =42,
                    BeginTime= DateTime.UtcNow.AddHours(-5).AddDays(-4),
                    EndTime = DateTime.UtcNow.AddDays(-4)   
                },
                new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Walking,
                    Description="3 day ago Walk",
                    Steps= 25000,
                    Distance = 2.6,
                    BeginTime= DateTime.UtcNow.AddMinutes(-48).AddDays(-3),
                    EndTime = DateTime.UtcNow.AddDays(-3)    
                },
                new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Jogging,
                    Description="3 day ago Jog",
                    Steps= 2000,
                    Distance = 1,
                    BeginTime= DateTime.UtcNow.AddMinutes(-12).AddDays(-3),
                    EndTime = DateTime.UtcNow.AddDays(-3)    
                },
                new Activity {
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Running,
                    Description="3 day ago Run",
                    Steps= 1050,
                    Distance = .75,
                    BeginTime= DateTime.UtcNow.AddMinutes(-8).AddDays(-3),
                    EndTime = DateTime.UtcNow.AddDays(-3)    
                },
                new Activity {
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Biking,
                    Description="3 day ago Bike",
                    Steps= 0,
                    Distance =19,
                    BeginTime= DateTime.UtcNow.AddHours(-2).AddDays(-3),
                    EndTime = DateTime.UtcNow.AddDays(-3)   
                },
                new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Walking,
                    Description="2 day ago Walk",
                    Steps= 10000,
                    Distance = .33,
                    BeginTime= DateTime.UtcNow.AddMinutes(-20).AddDays(-2),
                    EndTime = DateTime.UtcNow.AddDays(-2)    
                },
                new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Jogging,
                    Description="2 day ago Jog",
                    Steps= 2500,
                    Distance = 1.5,
                    BeginTime= DateTime.UtcNow.AddMinutes(-15).AddDays(-2),
                    EndTime = DateTime.UtcNow.AddDays(-2)    
                },
                new Activity {
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Running,
                    Description="2 day ago Run",
                    Steps= 1500,
                    Distance = 1.25,
                    BeginTime= DateTime.UtcNow.AddMinutes(-10).AddDays(-2),
                    EndTime = DateTime.UtcNow.AddDays(-2)    
                },
                new Activity {
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Biking,
                    Description="2 day ago Bike",
                    Steps= 0,
                    Distance = 4.5,
                    BeginTime= DateTime.UtcNow.AddMinutes(-30).AddDays(-2),
                    EndTime = DateTime.UtcNow.AddDays(-2)   
                },
                new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Walking,
                    Description="Yesterday Walk",
                    Steps= 26000,
                    Distance = 20,
                    BeginTime= DateTime.UtcNow.AddHours(-2).AddDays(-1),
                    EndTime = DateTime.UtcNow.AddDays(-1)    
                },
                new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Jogging,
                    Description="Yesterday Jog",
                    Steps= 1500,
                    Distance = 1,
                    BeginTime= DateTime.UtcNow.AddHours(-1).AddDays(-1),
                    EndTime = DateTime.UtcNow.AddDays(-1)    
                },
                new Activity {
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Running,
                    Description="Yesterday Run",
                    Steps= 5500,
                    Distance = 3.4,
                    BeginTime= DateTime.UtcNow.AddHours(-1).AddDays(-1),
                    EndTime = DateTime.UtcNow.AddDays(-1)    
                },
                new Activity {
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Biking,
                    Description="Yesterday Bike",
                    Steps= 18000,
                    Distance = 9.3,
                    BeginTime= DateTime.UtcNow.AddHours(-4).AddDays(-1),
                    EndTime = DateTime.UtcNow.AddDays(-1)   
                },
                new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Walking,
                    Description="Today Walk",
                    Steps= 16000,
                    Distance = 10,
                    BeginTime= DateTime.UtcNow.AddHours(-2),
                    EndTime = DateTime.UtcNow    
                },
                new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Jogging,
                    Description="Today Jog",
                    Steps= 3000,
                    Distance = 2,
                    BeginTime= DateTime.UtcNow.AddHours(-1),
                    EndTime = DateTime.UtcNow    
                },
                new Activity {
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Running,
                    Description="Today Run",
                    Steps= 5000,
                    Distance = 3,
                    BeginTime= DateTime.UtcNow.AddHours(-1),
                    EndTime = DateTime.UtcNow    
                },
                new Activity {
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Type=ActivityType.Biking,
                    Description="Today Bike",
                    Steps= 20000,
                    Distance = 10,
                    BeginTime= DateTime.UtcNow.AddHours(-5),
                    EndTime = DateTime.UtcNow    
                },
                 new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 10152669987526046).Id,
                    Type=ActivityType.Biking,
                    Description="Heitor Day 1 Biking",
                    Steps= 0,
                    Distance = 27,
                    BeginTime= DateTime.UtcNow.AddDays(-4).AddHours(-3),
                    EndTime = DateTime.UtcNow.AddDays(-4)
                },
                 new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 10152669987526046).Id,
                    Type=ActivityType.Running,
                    Description="Heitor Day 1 Run",
                    Steps= 2500,
                    Distance = 2.1,
                    BeginTime= DateTime.UtcNow.AddDays(-4).AddMinutes(-18),
                    EndTime = DateTime.UtcNow.AddDays(-4)
                },
                 new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 10152669987526046).Id,
                    Type=ActivityType.Walking,
                    Description="Heitor Day 2 Walk",
                    Steps= 6000,
                    Distance = 3,
                    BeginTime= DateTime.UtcNow.AddDays(-3).AddHours(-1),
                    EndTime = DateTime.UtcNow.AddDays(-3)
                },
                 new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 10152669987526046).Id,
                    Type=ActivityType.Jogging,
                    Description="Heitor Day 2 jog",
                    Steps= 5000,
                    Distance = 3,
                    BeginTime= DateTime.UtcNow.AddDays(-3).AddHours(-.5),
                    EndTime = DateTime.UtcNow.AddDays(-3)
                },
                 new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 10152669987526046).Id,
                    Type=ActivityType.Biking,
                    Description="Heitor Day 2 Bike",
                    Steps= 0,
                    Distance = 4.5,
                    BeginTime= DateTime.UtcNow.AddDays(-3).AddHours(-.5),
                    EndTime = DateTime.UtcNow.AddDays(-3)
                },
                 new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 10152669987526046).Id,
                    Type=ActivityType.Walking,
                    Description="Heitor Day 3 Walk",
                    Steps= 10000,
                    Distance = 5,
                    BeginTime= DateTime.UtcNow.AddDays(-2).AddHours(-1.66),
                    EndTime = DateTime.UtcNow.AddDays(-2)
                },
                 new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 10152669987526046).Id,
                    Type=ActivityType.Running,
                    Description="Heitor Day 3 run",
                    Steps= 1200,
                    Distance = 1.1,
                    BeginTime= DateTime.UtcNow.AddDays(-2).AddMinutes(-10),
                    EndTime = DateTime.UtcNow.AddDays(-2)
                },
                 new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 10152669987526046).Id,
                    Type=ActivityType.Walking,
                    Description="Heitor Day 4 walk",
                    Steps= 12000,
                    Distance =6,
                    BeginTime= DateTime.UtcNow.AddDays(-1).AddMinutes(-85),
                    EndTime = DateTime.UtcNow.AddDays(-1)
                },
                 new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 10152669987526046).Id,
                    Type=ActivityType.Biking,
                    Description="Heitor Day 4 biking",
                    Steps= 0,
                    Distance =31,
                    BeginTime= DateTime.UtcNow.AddDays(-1).AddMinutes(-200),
                    EndTime = DateTime.UtcNow.AddDays(-1)
                },
                 new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 10152669987526046).Id,
                    Type=ActivityType.Running,
                    Description="Heitor Day 5 walk",
                    Steps= 7500,
                    Distance =6,
                    BeginTime= DateTime.UtcNow.AddMinutes(-57),
                    EndTime = DateTime.UtcNow
                },
                 new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 10152669987526046).Id,
                    Type=ActivityType.Jogging,
                    Description="Heitor Day 5 biking",
                    Steps= 4000,
                    Distance =2.2,
                    BeginTime= DateTime.UtcNow.AddMinutes(-30),
                    EndTime = DateTime.UtcNow
                },
                 new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 10152669987526046).Id,
                    Type=ActivityType.Jogging,
                    Description="Heitor Day 5 biking",
                    Steps= 4000,
                    Distance =2.2,
                    BeginTime= DateTime.UtcNow.AddMinutes(-30),
                    EndTime = DateTime.UtcNow
                },
                 new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 1590114489).Id,
                    Type=ActivityType.Running,
                    Description="Hanna Day 1 run",
                    Steps= 24000,
                    Distance = 20,
                    BeginTime= DateTime.UtcNow.AddDays(-2).AddHours(-3.5),
                    EndTime = DateTime.UtcNow.AddDays(-2)
                },
                 new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 1590114489).Id,
                    Type=ActivityType.Walking,
                    Description="hanna Day 2 walk",
                    Steps= 10000,
                    Distance = 5,
                    BeginTime= DateTime.UtcNow.AddDays(-1).AddHours(-1.66),
                    EndTime = DateTime.UtcNow.AddDays(-1)
                },
                 new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 1590114489).Id,
                    Type=ActivityType.Jogging,
                    Description="hanna Day 2 jog",
                    Steps= 5000,
                    Distance = 3,
                    BeginTime= DateTime.UtcNow.AddDays(-1).AddHours(-.5),
                    EndTime = DateTime.UtcNow.AddDays(-1)
                },
                 new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 1590114489).Id,
                    Type=ActivityType.Biking,
                    Description="hanna Day 2 bike",
                    Steps= 0,
                    Distance = 11,
                    BeginTime= DateTime.UtcNow.AddDays(-1).AddHours(-1),
                    EndTime = DateTime.UtcNow.AddDays(-1)
                },
                 new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 1590114489).Id,
                    Type=ActivityType.Running,
                    Description="Hanna Day 3 run",
                    Steps= 10000,
                    Distance = 8,
                    BeginTime= DateTime.UtcNow.AddHours(-1.25),
                    EndTime = DateTime.UtcNow
                },
                 new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 1590114489).Id,
                    Type=ActivityType.Walking,
                    Description="hanna Day 3 Walk",
                    Steps= 5000,
                    Distance = 2.75,
                    BeginTime= DateTime.UtcNow.AddHours(-1),
                    EndTime = DateTime.UtcNow
                },
                 new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 1590114489).Id,
                    Type=ActivityType.Biking,
                    Description="hanna Day 3 bike",
                    Steps= 0,
                    Distance = 21,
                    BeginTime= DateTime.UtcNow.AddMinutes(-120),
                    EndTime = DateTime.UtcNow
                },
                 new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 802746313125281).Id,
                    Type=ActivityType.Biking,
                    Description="wyatt Day 1 bike",
                    Steps= 0,
                    Distance = 50,
                    BeginTime= DateTime.UtcNow.AddDays(-2).AddHours(-5),
                    EndTime = DateTime.UtcNow.AddDays(-2)
                },
                 new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 802746313125281).Id,
                    Type=ActivityType.Walking,
                    Description="wyatt Day 1 walk",
                    Steps= 2000,
                    Distance = 1,
                    BeginTime= DateTime.UtcNow.AddDays(-2).AddHours(-.33),
                    EndTime = DateTime.UtcNow.AddDays(-2)
                },
                 new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 802746313125281).Id,
                    Type=ActivityType.Walking,
                    Description="wyatt Day 2 walk",
                    Steps= 11000,
                    Distance = 6,
                    BeginTime= DateTime.UtcNow.AddDays(-1).AddHours(-1.5),
                    EndTime = DateTime.UtcNow.AddDays(-1)
                },
                 new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 802746313125281).Id,
                    Type=ActivityType.Running,
                    Description="wyatt Day 2 run",
                    Steps= 12000,
                    Distance = 10,
                    BeginTime= DateTime.UtcNow.AddDays(-1).AddHours(-1.75),
                    EndTime = DateTime.UtcNow.AddDays(-1)
                },
                 new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 802746313125281).Id,
                    Type=ActivityType.Jogging,
                    Description="wyatt Day 2 jog",
                    Steps= 5000,
                    Distance = 3,
                    BeginTime= DateTime.UtcNow.AddDays(-1).AddHours(-.5),
                    EndTime = DateTime.UtcNow.AddDays(-1)
                },
                 new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 802746313125281).Id,
                    Type=ActivityType.Running,
                    Description="wyatt Day 3 run",
                    Steps= 8000,
                    Distance = 7,
                    BeginTime= DateTime.UtcNow.AddHours(-1),
                    EndTime = DateTime.UtcNow
                },
                 new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 802746313125281).Id,
                    Type=ActivityType.Jogging,
                    Description="wyatt Day 3 jog",
                    Steps= 2000,
                    Distance = 1.5,
                    BeginTime= DateTime.UtcNow.AddHours(-.25),
                    EndTime = DateTime.UtcNow
                },
                 new Activity { 
                    AccountId= context.Users.Single(s => s.FacebookId == 802746313125281).Id,
                    Type=ActivityType.Biking,
                    Description="wyatt Day 3 bike",
                    Steps= 0,
                    Distance = 18,
                    BeginTime= DateTime.UtcNow.AddMinutes(-150),
                    EndTime = DateTime.UtcNow
                }
            };

            activities.ForEach(s => context.Activities.AddOrUpdate(p => p.Id, s));
            context.SaveChanges();

            var moods = new List<Mood>
            {
                new Mood{
                    Id = 1,
                    Description = "Determined"
                },
                new Mood{
                    Id = 2,
                    Description = "Worn out"
                },
                new Mood{
                    Id = 3,
                    Description = "Accomplished"
                },
                new Mood{
                    Id = 4,
                    Description = "Satisfied"
                },
                new Mood{
                    Id = 5,
                    Description = "Inspired"
                },
                new Mood{
                    Id = 6,
                    Description = "Tired"
                },
                new Mood{
                    Id = 7,
                    Description = "Refreshed"
                },
                new Mood{
                    Id = 8,
                    Description = "Happy"
                },
                new Mood{
                    Id = 9,
                    Description = "Sick"
                },
                 new Mood{
                    Id = 10,
                    Description = "Energized"
                }
            };

            moods.ForEach(s => context.Moods.AddOrUpdate(p => p.Id, s));
            context.SaveChanges();

            var status = new List<Status>
            {
                new Status { 
                    Id = 2,
                    MoodId = 2,
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Date = new DateTime(2015, 3, 10)    
                }, 
                new Status { 
                    Id = 3,
                    MoodId = 10,
                    AccountId= context.Users.Single(s => s.FacebookId == 802746313125281).Id,
                    Date = new DateTime(2015, 3, 14)    
                }
            };

            status.ForEach(s => context.Statuses.AddOrUpdate(p => p.Id, s));
            context.SaveChanges();

            var reports = new List<Report>
            {
                new Report { 
                    Steps = 45090,
                    Distance=14,
                    Duration=2.5,
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Date = DateTime.Today.AddDays(-6) 
                },
                new Report { 
                    Steps = 40000,
                    Distance=21.45,
                    Duration=3.2167,
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Date = DateTime.Today.AddDays(-5) 
                },
                new Report { 
                    Steps = 40000,
                    Distance=51.6,
                    Duration=7.267,
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Date = DateTime.Today.AddDays(-4) 
                },
                new Report { 
                    Steps = 28050,
                    Distance=23.35,
                    Duration=3.133,
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Date = DateTime.Today.AddDays(-3) 
                },
                new Report { 
                    Steps = 14000,
                    Distance=8.25,
                    Duration=1.25,
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Date = DateTime.Today.AddDays(-2) 
                },
                new Report { 
                    Steps = 51000,
                    Distance=33.7,
                    Duration=8,
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Date = DateTime.Today.AddDays(-1)    
                }, 
                new Report { 
                    Steps = 44000,
                    Distance=25,
                    Duration=9,
                    AccountId= context.Users.Single(s => s.FacebookId == 4929447011515).Id,
                    Date = DateTime.Today    
                },
                new Report { 
                    Steps = 2500,
                    Distance=29.1,
                    Duration=3.3,
                    AccountId= context.Users.Single(s => s.FacebookId == 10152669987526046).Id,
                    Date = DateTime.Today.AddDays(-4) 
                },
                new Report { 
                    Steps = 11000,
                    Distance=10.5,
                    Duration=2,
                    AccountId= context.Users.Single(s => s.FacebookId == 10152669987526046).Id,
                    Date = DateTime.Today.AddDays(-3) 
                },
                new Report { 
                    Steps = 11200,
                    Distance=6.1,
                    Duration=1.833,
                    AccountId= context.Users.Single(s => s.FacebookId == 10152669987526046).Id,
                    Date = DateTime.Today.AddDays(-2) 
                },
                new Report { 
                    Steps = 12000,
                    Distance=37,
                    Duration=4.55,
                    AccountId= context.Users.Single(s => s.FacebookId == 10152669987526046).Id,
                    Date = DateTime.Today.AddDays(-1)    
                }, 
                new Report { 
                    Steps = 11500,
                    Distance=8.2,
                    Duration=1.45,
                    AccountId= context.Users.Single(s => s.FacebookId == 10152669987526046).Id,
                    Date = DateTime.Today    
                },
                new Report { 
                    Steps = 24000,
                    Distance=20,
                    Duration=3.5,
                    AccountId= context.Users.Single(s => s.FacebookId == 1590114489).Id,
                    Date = DateTime.Today.AddDays(-2) 
                },
                new Report { 
                    Steps = 15000,
                    Distance=19,
                    Duration=3.167,
                    AccountId= context.Users.Single(s => s.FacebookId == 1590114489).Id,
                    Date = DateTime.Today.AddDays(-1)    
                }, 
                new Report { 
                    Steps = 15000,
                    Distance=31.75,
                    Duration=4.25,
                    AccountId= context.Users.Single(s => s.FacebookId == 1590114489).Id,
                    Date = DateTime.Today    
                },
                new Report { 
                    Steps = 2000,
                    Distance=51,
                    Duration=5.33,
                    AccountId= context.Users.Single(s => s.FacebookId == 802746313125281).Id,
                    Date = DateTime.Today.AddDays(-2) 
                },
                new Report { 
                    Steps = 28000,
                    Distance=19,
                    Duration=3.75,
                    AccountId= context.Users.Single(s => s.FacebookId == 802746313125281).Id,
                    Date = DateTime.Today.AddDays(-1)    
                }, 
                new Report { 
                    Steps = 10000,
                    Distance=26.5,
                    Duration=3.75,
                    AccountId= context.Users.Single(s => s.FacebookId == 802746313125281).Id,
                    Date = DateTime.Today    
                }
            };

            reports.ForEach(s => context.Reports.AddOrUpdate(p => p.Id, s));
            context.SaveChanges();

        }
    }
}




