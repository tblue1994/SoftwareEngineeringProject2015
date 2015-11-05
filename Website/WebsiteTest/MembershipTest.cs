using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Website.Controllers;
using Website.Models;

namespace WebsiteTest
{
    [TestClass]
    public class MembershipTest
    {
        private void BasicDb(MockDatabase mockDb)
        {
            mockDb.Accounts.Add(new Account()
            {
                Id = 1,
                FullName = "Trevor Slawnyk",
                PreferredName = "Trevor",
                Zip = 68456,
                FacebookId = 1758682040,
                Birthdate = new DateTime(1994, 6, 22),
                Weight = 250,
                Height = 73,
                Sex = false
            });
            mockDb.Accounts.Add(new Account()
                {
                    Id = 2,
                    FullName = "Hanna Rogoz",
                    PreferredName = "Hanna Banana",
                    Zip = 60517,
                    FacebookId = 1590114489,
                    Birthdate = new DateTime(1995, 8, 4),
                    Weight = 170,
                    Height = 69,
                    Sex = true
                });
            mockDb.Teams.Add(new Team
                {
                    Id = 1,
                    Name = "Cowbell",
                    Access = true
                });
            mockDb.Memberships.Add(new Membership
                {
                    Id = 1,
                    AccountId = 1,
                    TeamId = 1,
                    Status = MembershipStatus.Admin
                });
        }

        private void DoubleDb(MockDatabase db)
        {
            BasicDb(db);
            db.Memberships.Add(new Membership
                {
                    Id = 2,
                    AccountId = 2,
                    TeamId = 1,
                    Status = MembershipStatus.Member
                });
        }

        [TestMethod]
        public void MembershipPostGetAccept()
        {
            var mockDb = new MockDatabase();
            BasicDb(mockDb);

            MembershipController controller = new MembershipController(mockDb);


            var membership = (controller.Accept(teamId: 1, userId: 2) as OkNegotiatedContentResult<Membership>).Content;
            var result = controller.Get(membership.Id) as OkNegotiatedContentResult<Membership>;
            Assert.IsNotNull(result);

            Assert.AreEqual(1, result.Content.TeamId);
            Assert.AreEqual(2, result.Content.AccountId);
            Assert.AreEqual(MembershipStatus.Member, result.Content.Status);
        }

        [TestMethod]
        public void MembershipBan()
        {
            var mockDb = new MockDatabase();
            DoubleDb(mockDb);

            MembershipController controller = new MembershipController(mockDb);

            controller.BanMember(teamId: 1, toId: 2, fromId: 1);

            var result = controller.Get(2) as OkNegotiatedContentResult<Membership>;
            Assert.IsNotNull(result);

            Assert.AreEqual(1, result.Content.TeamId);
            Assert.AreEqual(2, result.Content.AccountId);
            Assert.AreEqual(MembershipStatus.Banned, result.Content.Status);
        }

        [TestMethod]
        public void MembershipGiveAndTakeModerator()
        {
            var mockDb = new MockDatabase();
            DoubleDb(mockDb);

            MembershipController controller = new MembershipController(mockDb);

            controller.GiveModeratorStatus(teamId: 1, toId: 2, fromId: 1);

            var result = controller.Get(2) as OkNegotiatedContentResult<Membership>;
            Assert.IsNotNull(result);

            Assert.AreEqual(1, result.Content.TeamId);
            Assert.AreEqual(2, result.Content.AccountId);
            Assert.AreEqual(MembershipStatus.Moderator, result.Content.Status);

            controller.RemoveModeratorStatus(teamId: 1, toId: 2, fromId: 1);

            result = controller.Get(2) as OkNegotiatedContentResult<Membership>;
            Assert.IsNotNull(result);

            Assert.AreEqual(1, result.Content.TeamId);
            Assert.AreEqual(2, result.Content.AccountId);
            Assert.AreEqual(MembershipStatus.Member, result.Content.Status);
        }

        [TestMethod]
        public void MembershipByTeam()
        {
            var mockDb = new MockDatabase();
            DoubleDb(mockDb);

            MembershipController controller = new MembershipController(mockDb);

            var result = controller.ByTeam(1) as OkNegotiatedContentResult<List<Membership>>;
            Assert.IsNotNull(result);

            Assert.IsTrue(result.Content.Select(a => a.AccountId).Contains(1));
            Assert.IsTrue(result.Content.Select(a => a.AccountId).Contains(2));
        }


        [TestMethod]
        public void MembershipByAccount()
        {
            var mockDb = new MockDatabase();
            DoubleDb(mockDb);

            MembershipController controller = new MembershipController(mockDb);

            var result = controller.ByAccount(1) as OkNegotiatedContentResult<List<Membership>>;
            Assert.IsNotNull(result);

            Assert.IsTrue(result.Content.Select(t => t.TeamId).Contains(1));
        }
    }
}
