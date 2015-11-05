using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Website.BusinessLogic;
using Website.Models;
using Website.Repositories;
using Moq;

namespace Website.Tests
{
    [TestClass]
    public class LogicTests
    {
        [TestMethod]
        public void AccountLogic()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            Mock<IAccountRepository> repo = new Mock<IAccountRepository>();
            Mock<ITeamRepository> teamRepo = new Mock<ITeamRepository>();
            Mock<IAttainmentRepository> attainmentRepo = new Mock<IAttainmentRepository>();
            Mock<IMembershipRepository> membershipRepo = new Mock<IMembershipRepository>();

            AccountLogic logic = new AccountLogic(uow.Object, repo.Object, teamRepo.Object, attainmentRepo.Object, membershipRepo.Object);

            var account = new Account();
            var sequence = new MockSequence();
            repo.InSequence(sequence).Setup(r => r.Create(account));
            repo.InSequence(sequence).Setup(r => r.Update(account));
            repo.InSequence(sequence).Setup(r => r.Get(account.Id));
            repo.InSequence(sequence).Setup(r => r.Delete(account.Id));
            logic.Create(account);
            logic.Update(account);
            logic.Get(account.Id);
            logic.Delete(account.Id);
        }

        [TestMethod]
        public void ActivityLogic()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            Mock<IActivityRepository> repo = new Mock<IActivityRepository>();
            Mock<ITeamLogic> teamLogic = new Mock<ITeamLogic>();

            ActivityLogic logic = new ActivityLogic(uow.Object, repo.Object, teamLogic.Object);

            var activity = new Activity();
            var sequence = new MockSequence();
            repo.InSequence(sequence).Setup(r => r.Create(activity));
            repo.InSequence(sequence).Setup(r => r.Update(activity));
            repo.InSequence(sequence).Setup(r => r.Get(activity.Id));
            repo.InSequence(sequence).Setup(r => r.Delete(activity.Id));
            logic.Create(activity);
            logic.Update(activity);
            logic.Get(activity.Id);
            logic.Delete(activity.Id);
        }

        [TestMethod]
        public void AttainmentLogic()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            Mock<IAttainmentRepository> repo = new Mock<IAttainmentRepository>();
            Mock<IBadgeRepository> badgeRepo = new Mock<IBadgeRepository>();
            Mock<IActivityRepository> activityRepo = new Mock<IActivityRepository>();
            Mock<ITargetRepository> targetRepo = new Mock<ITargetRepository>();
            Mock<ITeamLogic> teamLogic = new Mock<ITeamLogic>();

            AttainmentLogic logic = new AttainmentLogic(uow.Object, repo.Object, badgeRepo.Object, activityRepo.Object, targetRepo.Object, teamLogic.Object);

            var attainment = new Attainment();
            var sequence = new MockSequence();
            repo.InSequence(sequence).Setup(r => r.Create(attainment));
            repo.InSequence(sequence).Setup(r => r.Update(attainment));
            repo.InSequence(sequence).Setup(r => r.Get(attainment.Id));
            repo.InSequence(sequence).Setup(r => r.Delete(attainment.Id));
            logic.Create(attainment);
            logic.Update(attainment);
            logic.Get(attainment.Id);
            logic.Delete(attainment.Id);
        }

        [TestMethod]
        public void BadgeLogic()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            Mock<IBadgeRepository> repo = new Mock<IBadgeRepository>();

            BadgeLogic logic = new BadgeLogic(uow.Object, repo.Object);

            var badge = new Badge();
            var sequence = new MockSequence();
            repo.InSequence(sequence).Setup(r => r.Create(badge));
            repo.InSequence(sequence).Setup(r => r.Update(badge));
            repo.InSequence(sequence).Setup(r => r.Get(badge.Id));
            repo.InSequence(sequence).Setup(r => r.Delete(badge.Id));
            logic.Create(badge);
            logic.Update(badge);
            logic.Get(badge.Id);
            logic.Delete(badge.Id);
        }

        [TestMethod]
        public void FoodLogic()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            Mock<IFoodRepository> repo = new Mock<IFoodRepository>();
            Mock<ITeamLogic> teamLogic = new Mock<ITeamLogic>();

            FoodLogic logic = new FoodLogic(uow.Object, repo.Object, teamLogic.Object);

            var food = new Food();
            var sequence = new MockSequence();
            repo.InSequence(sequence).Setup(r => r.Create(food));
            repo.InSequence(sequence).Setup(r => r.Update(food));
            repo.InSequence(sequence).Setup(r => r.Get(food.Id));
            repo.InSequence(sequence).Setup(r => r.Delete(food.Id));
            logic.Create(food);
            logic.Update(food);
            logic.Get(food.Id);
            logic.Delete(food.Id);
        }

        [TestMethod]
        public void GoalLogic()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            Mock<IGoalRepository> repo = new Mock<IGoalRepository>();
            Mock<ITargetRepository> targetRepo = new Mock<ITargetRepository>();
            Mock<IActivityRepository> activityRepo = new Mock<IActivityRepository>();

            GoalLogic logic = new GoalLogic(uow.Object, repo.Object, targetRepo.Object, activityRepo.Object);

            var goal = new Goal();
            var sequence = new MockSequence();
            repo.InSequence(sequence).Setup(r => r.Create(goal));
            repo.InSequence(sequence).Setup(r => r.Update(goal));
            repo.InSequence(sequence).Setup(r => r.Get(goal.Id));
            repo.InSequence(sequence).Setup(r => r.Delete(goal.Id));
            logic.Create(goal);
            logic.Update(goal);
            logic.Get(goal.Id);
            logic.Delete(goal.Id);
        }

        [TestMethod]
        public void MembershipLogic()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            Mock<IMembershipRepository> repo = new Mock<IMembershipRepository>();

            MembershipLogic logic = new MembershipLogic(uow.Object, repo.Object);

            var membership = new Membership();
            var sequence = new MockSequence();
            repo.InSequence(sequence).Setup(r => r.Create(membership));
            repo.InSequence(sequence).Setup(r => r.Update(membership));
            repo.InSequence(sequence).Setup(r => r.Get(membership.Id));
            repo.InSequence(sequence).Setup(r => r.Delete(membership.Id));
            logic.Create(membership);
            logic.Update(membership);
            logic.Get(membership.Id);
            logic.Delete(membership.Id);
        }

        [TestMethod]
        public void MoodLogic()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            Mock<IMoodRepository> repo = new Mock<IMoodRepository>();

            MoodLogic logic = new MoodLogic(uow.Object, repo.Object);

            var mood = new Mood();
            var sequence = new MockSequence();
            repo.InSequence(sequence).Setup(r => r.Create(mood));
            repo.InSequence(sequence).Setup(r => r.Update(mood));
            repo.InSequence(sequence).Setup(r => r.Get(mood.Id));
            repo.InSequence(sequence).Setup(r => r.Delete(mood.Id));
            logic.Create(mood);
            logic.Update(mood);
            logic.Get(mood.Id);
            logic.Delete(mood.Id);
        }

        [TestMethod]
        public void PathLogic()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            Mock<IPathRepository> repo = new Mock<IPathRepository>();

            PathLogic logic = new PathLogic(uow.Object, repo.Object);

            var path = new Path();
            var sequence = new MockSequence();
            repo.InSequence(sequence).Setup(r => r.Create(path));
            repo.InSequence(sequence).Setup(r => r.Update(path));
            repo.InSequence(sequence).Setup(r => r.Get(path.Id));
            repo.InSequence(sequence).Setup(r => r.Delete(path.Id));
            logic.Create(path);
            logic.Update(path);
            logic.Get(path.Id);
            logic.Delete(path.Id);
        }

        [TestMethod]
        public void ReportLogic()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            Mock<IReportRepository> repo = new Mock<IReportRepository>();
            Mock<IActivityRepository> activityRepo = new Mock<IActivityRepository>();
            Mock<IAccountRepository> accountRepo = new Mock<IAccountRepository>();

            ReportLogic logic = new ReportLogic(uow.Object, repo.Object, activityRepo.Object, accountRepo.Object);

            var report = new Report();
            var sequence = new MockSequence();
            repo.InSequence(sequence).Setup(r => r.Create(report));
            repo.InSequence(sequence).Setup(r => r.Update(report));
            repo.InSequence(sequence).Setup(r => r.Get(report.Id));
            repo.InSequence(sequence).Setup(r => r.Delete(report.Id));
            logic.Create();
            logic.Update(report);
            logic.Get(report.Id);
            logic.Delete(report.Id);
        }

        [TestMethod]
        public void StatusLogic()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            Mock<IStatusRepository> repo = new Mock<IStatusRepository>();
            Mock<ITeamLogic> teamLogic = new Mock<ITeamLogic>();

            StatusLogic logic = new StatusLogic(uow.Object, repo.Object, teamLogic.Object);

            var status = new Status();
            var sequence = new MockSequence();
            repo.InSequence(sequence).Setup(r => r.Create(status));
            repo.InSequence(sequence).Setup(r => r.Update(status));
            repo.InSequence(sequence).Setup(r => r.Get(status.Id));
            repo.InSequence(sequence).Setup(r => r.Delete(status.Id));
            logic.Create(status);
            logic.Update(status);
            logic.Get(status.Id);
            logic.Delete(status.Id);
        }

        [TestMethod]
        public void TargetLogic()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            Mock<ITargetRepository> repo = new Mock<ITargetRepository>();

            TargetLogic logic = new TargetLogic(uow.Object, repo.Object);

            var target = new Target();
            var sequence = new MockSequence();
            repo.InSequence(sequence).Setup(r => r.Create(target));
            repo.InSequence(sequence).Setup(r => r.Update(target));
            repo.InSequence(sequence).Setup(r => r.Get(target.Id));
            repo.InSequence(sequence).Setup(r => r.Delete(target.Id));
            logic.Create(target);
            logic.Update(target);
            logic.Get(target.Id);
            logic.Delete(target.Id);
        }

        [TestMethod]
        public void TeamLogic()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            Mock<ITeamRepository> repo = new Mock<ITeamRepository>();

            TeamLogic logic = new TeamLogic(uow.Object, repo.Object);

            var team = new Team();
            var sequence = new MockSequence();
            repo.InSequence(sequence).Setup(r => r.Create(team));
            repo.InSequence(sequence).Setup(r => r.Update(team));
            repo.InSequence(sequence).Setup(r => r.Get(team.Id));
            logic.Create(team);
            logic.Update(team);
            logic.Get(team.Id);
        }
    }
}
