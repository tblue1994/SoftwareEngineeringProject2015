using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Website.Repositories;
using Moq;
using Website.Models;
using System.Data.Entity;

namespace Website.Tests
{
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        public void AccountRepository()
        {
            Mock<IWebsiteContext> context = new Mock<IWebsiteContext>();
            Mock<IDbSetFactory> factory = new Mock<IDbSetFactory>();
            Mock<DbSet<Account>> dbSet = new Mock<DbSet<Account>>();

            factory.Setup(m => m.CreateDbSet<Account>()).Returns(dbSet.Object);

            AccountRepository repo = new AccountRepository(context.Object, factory.Object);

            var account = new Account
                {
                    Id = "SDF",
                    FullName = "Trevor Slawnyk",
                    PreferredName = "Trevor",
                    Zip = 68456,
                    FacebookId = 4929447011515,
                    Birthdate = new DateTime(1994, 6, 22),
                    Weight = 250,
                    Height = 73,
                    Sex = false
                };
            account.UserName = "asdf";

            var sequence = new MockSequence();
            dbSet.InSequence(sequence).Setup(e => e.Add(account));
            dbSet.InSequence(sequence).Setup(e => e.Find(account.Id));
            dbSet.InSequence(sequence).Setup(e => e.Find(account.Id));
            dbSet.InSequence(sequence).Setup(e => e.Find(account.Id));
            repo.Create(account);
            repo.Get(account.Id);
            repo.Update(account);
            repo.Delete(account.Id);
        }

        [TestMethod]
        public void ActivityRepository()
        {
            Mock<IDbSetFactory> factory = new Mock<IDbSetFactory>();
            Mock<DbSet<Activity>> dbSet = new Mock<DbSet<Activity>>();

            factory.Setup(m => m.CreateDbSet<Activity>()).Returns(dbSet.Object);

            ActivityRepository repo = new ActivityRepository(factory.Object);

            var activity = new Activity();

            var sequence = new MockSequence();
            dbSet.InSequence(sequence).Setup(e => e.Add(activity));
            dbSet.InSequence(sequence).Setup(e => e.Find(activity.Id));
            dbSet.InSequence(sequence).Setup(e => e.Find(activity.Id));
            dbSet.InSequence(sequence).Setup(e => e.Find(activity.Id));
            repo.Create(activity);
            repo.Get(activity.Id);
            repo.Update(activity);
            repo.Delete(activity.Id);
        }

        [TestMethod]
        public void AttainmentRepository()
        {
            Mock<IDbSetFactory> factory = new Mock<IDbSetFactory>();
            Mock<DbSet<Attainment>> dbSet = new Mock<DbSet<Attainment>>();

            factory.Setup(m => m.CreateDbSet<Attainment>()).Returns(dbSet.Object);

            AttainmentRepository repo = new AttainmentRepository(factory.Object);

            var attainment = new Attainment();

            var sequence = new MockSequence();
            dbSet.InSequence(sequence).Setup(e => e.Add(attainment));
            dbSet.InSequence(sequence).Setup(e => e.Find(attainment.Id));
            dbSet.InSequence(sequence).Setup(e => e.Find(attainment.Id));
            dbSet.InSequence(sequence).Setup(e => e.Find(attainment.Id));
            repo.Create(attainment);
            repo.Get(attainment.Id);
            repo.Update(attainment);
            repo.Delete(attainment.Id);
        }

        [TestMethod]
        public void BadgeRepository()
        {
            Mock<IDbSetFactory> factory = new Mock<IDbSetFactory>();
            Mock<DbSet<Badge>> dbSet = new Mock<DbSet<Badge>>();

            factory.Setup(m => m.CreateDbSet<Badge>()).Returns(dbSet.Object);

            BadgeRepository repo = new BadgeRepository(factory.Object);

            var badge = new Badge();

            var sequence = new MockSequence();
            dbSet.InSequence(sequence).Setup(e => e.Add(badge));
            dbSet.InSequence(sequence).Setup(e => e.Find(badge.Id));
            dbSet.InSequence(sequence).Setup(e => e.Find(badge.Id));
            dbSet.InSequence(sequence).Setup(e => e.Find(badge.Id));
            repo.Create(badge);
            repo.Get(badge.Id);
            repo.Update(badge);
            repo.Delete(badge.Id);
        }

        [TestMethod]
        public void FoodRepository()
        {
            Mock<IDbSetFactory> factory = new Mock<IDbSetFactory>();
            Mock<DbSet<Food>> dbSet = new Mock<DbSet<Food>>();

            factory.Setup(m => m.CreateDbSet<Food>()).Returns(dbSet.Object);

            FoodRepository repo = new FoodRepository(factory.Object);

            var Food = new Food();

            var sequence = new MockSequence();
            dbSet.InSequence(sequence).Setup(e => e.Add(Food));
            dbSet.InSequence(sequence).Setup(e => e.Find(Food.Id));
            dbSet.InSequence(sequence).Setup(e => e.Find(Food.Id));
            dbSet.InSequence(sequence).Setup(e => e.Find(Food.Id));
            repo.Create(Food);
            repo.Get(Food.Id);
            repo.Update(Food);
            repo.Delete(Food.Id);
        }

        [TestMethod]
        public void GoalRepository()
        {
            Mock<IDbSetFactory> factory = new Mock<IDbSetFactory>();
            Mock<DbSet<Goal>> dbSet = new Mock<DbSet<Goal>>();

            factory.Setup(m => m.CreateDbSet<Goal>()).Returns(dbSet.Object);

            GoalRepository repo = new GoalRepository(factory.Object);

            var Goal = new Goal();

            var sequence = new MockSequence();
            dbSet.InSequence(sequence).Setup(e => e.Add(Goal));
            dbSet.InSequence(sequence).Setup(e => e.Find(Goal.Id));
            dbSet.InSequence(sequence).Setup(e => e.Find(Goal.Id));
            dbSet.InSequence(sequence).Setup(e => e.Find(Goal.Id));
            repo.Create(Goal);
            repo.Get(Goal.Id);
            repo.Update(Goal);
            repo.Delete(Goal.Id);
        }

        [TestMethod]
        public void MembershipRepository()
        {
            Mock<IDbSetFactory> factory = new Mock<IDbSetFactory>();
            Mock<DbSet<Membership>> dbSet = new Mock<DbSet<Membership>>();

            factory.Setup(m => m.CreateDbSet<Membership>()).Returns(dbSet.Object);

            MembershipRepository repo = new MembershipRepository(factory.Object);

            var Membership = new Membership();

            var sequence = new MockSequence();
            dbSet.InSequence(sequence).Setup(e => e.Add(Membership));
            dbSet.InSequence(sequence).Setup(e => e.Find(Membership.Id));
            dbSet.InSequence(sequence).Setup(e => e.Find(Membership.Id));
            dbSet.InSequence(sequence).Setup(e => e.Find(Membership.Id));
            repo.Create(Membership);
            repo.Get(Membership.Id);
            repo.Update(Membership);
            repo.Delete(Membership.Id);
        }

        [TestMethod]
        public void MoodRepository()
        {
            Mock<IDbSetFactory> factory = new Mock<IDbSetFactory>();
            Mock<DbSet<Mood>> dbSet = new Mock<DbSet<Mood>>();

            factory.Setup(m => m.CreateDbSet<Mood>()).Returns(dbSet.Object);

            MoodRepository repo = new MoodRepository(factory.Object);

            var Mood = new Mood();

            var sequence = new MockSequence();
            dbSet.InSequence(sequence).Setup(e => e.Add(Mood));
            dbSet.InSequence(sequence).Setup(e => e.Find(Mood.Id));
            dbSet.InSequence(sequence).Setup(e => e.Find(Mood.Id));
            dbSet.InSequence(sequence).Setup(e => e.Find(Mood.Id));
            repo.Create(Mood);
            repo.Get(Mood.Id);
            repo.Update(Mood);
            repo.Delete(Mood.Id);
        }

        [TestMethod]
        public void PathRepository()
        {
            Mock<IDbSetFactory> factory = new Mock<IDbSetFactory>();
            Mock<DbSet<Path>> dbSet = new Mock<DbSet<Path>>();

            factory.Setup(m => m.CreateDbSet<Path>()).Returns(dbSet.Object);

            PathRepository repo = new PathRepository(factory.Object);

            var Path = new Path();

            var sequence = new MockSequence();
            dbSet.InSequence(sequence).Setup(e => e.Add(Path));
            dbSet.InSequence(sequence).Setup(e => e.Find(Path.Id));
            dbSet.InSequence(sequence).Setup(e => e.Find(Path.Id));
            dbSet.InSequence(sequence).Setup(e => e.Find(Path.Id));
            repo.Create(Path);
            repo.Get(Path.Id);
            repo.Update(Path);
            repo.Delete(Path.Id);
        }

        [TestMethod]
        public void ReportRepository()
        {
            Mock<IDbSetFactory> factory = new Mock<IDbSetFactory>();
            Mock<DbSet<Report>> dbSet = new Mock<DbSet<Report>>();

            factory.Setup(m => m.CreateDbSet<Report>()).Returns(dbSet.Object);

            ReportRepository repo = new ReportRepository(factory.Object);

            var Report = new Report();

            var sequence = new MockSequence();
            dbSet.InSequence(sequence).Setup(e => e.Add(Report));
            dbSet.InSequence(sequence).Setup(e => e.Find(Report.Id));
            dbSet.InSequence(sequence).Setup(e => e.Find(Report.Id));
            dbSet.InSequence(sequence).Setup(e => e.Find(Report.Id));
            repo.Create(Report);
            repo.Get(Report.Id);
            repo.Update(Report);
            repo.Delete(Report.Id);
        }

        [TestMethod]
        public void StatusRepository()
        {
            Mock<IDbSetFactory> factory = new Mock<IDbSetFactory>();
            Mock<DbSet<Status>> dbSet = new Mock<DbSet<Status>>();

            factory.Setup(m => m.CreateDbSet<Status>()).Returns(dbSet.Object);

            StatusRepository repo = new StatusRepository(factory.Object);

            var Status = new Status();

            var sequence = new MockSequence();
            dbSet.InSequence(sequence).Setup(e => e.Add(Status));
            dbSet.InSequence(sequence).Setup(e => e.Find(Status.Id));
            dbSet.InSequence(sequence).Setup(e => e.Find(Status.Id));
            dbSet.InSequence(sequence).Setup(e => e.Find(Status.Id));
            repo.Create(Status);
            repo.Get(Status.Id);
            repo.Update(Status);
            repo.Delete(Status.Id);
        }

        [TestMethod]
        public void TargetRepository()
        {
            Mock<IDbSetFactory> factory = new Mock<IDbSetFactory>();
            Mock<DbSet<Target>> dbSet = new Mock<DbSet<Target>>();

            factory.Setup(m => m.CreateDbSet<Target>()).Returns(dbSet.Object);

            TargetRepository repo = new TargetRepository(factory.Object);

            var Target = new Target();

            var sequence = new MockSequence();
            dbSet.InSequence(sequence).Setup(e => e.Add(Target));
            dbSet.InSequence(sequence).Setup(e => e.Find(Target.Id));
            dbSet.InSequence(sequence).Setup(e => e.Find(Target.Id));
            dbSet.InSequence(sequence).Setup(e => e.Find(Target.Id));
            repo.Create(Target);
            repo.Get(Target.Id);
            repo.Update(Target);
            repo.Delete(Target.Id);
        }

        [TestMethod]
        public void TeamRepository()
        {
            Mock<IDbSetFactory> factory = new Mock<IDbSetFactory>();
            Mock<DbSet<Team>> dbSet = new Mock<DbSet<Team>>();

            factory.Setup(m => m.CreateDbSet<Team>()).Returns(dbSet.Object);

            TeamRepository repo = new TeamRepository(factory.Object);

            var Team = new Team();

            var sequence = new MockSequence();
            dbSet.InSequence(sequence).Setup(e => e.Add(Team));
            dbSet.InSequence(sequence).Setup(e => e.Find(Team.Id));
            dbSet.InSequence(sequence).Setup(e => e.Find(Team.Id));
            dbSet.InSequence(sequence).Setup(e => e.Find(Team.Id));
            repo.Create(Team);
            repo.Get(Team.Id);
            repo.Update(Team);
            repo.Delete(Team.Id);
        }
    }
}
