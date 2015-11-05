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
    public class TeamTest
    {
        [TestMethod]
        public void TeamPostGet()
        {
            Team team = new Team
            {
                Access = true,
                Name = "Work together!"
            };

            TeamController controller = new TeamController(new MockDatabase());

            controller.Create(1, team);

            var result = controller.Get(1) as OkNegotiatedContentResult<Team>;
            Assert.IsNotNull(result);

            Assert.AreEqual("Work together!", result.Content.Name);
            Assert.AreEqual(true, result.Content.Access);
        }

        [TestMethod]
        public void TeamPostPutGet()
        {
            Team team = new Team
            {
                Access = true,
                Name = "Work together!"
            };

            TeamController controller = new TeamController(new MockDatabase());

            team = (controller.Create(1, team) as OkNegotiatedContentResult<Team>).Content;

            team.Access = false;
            team.Name = "Nah";

            var result = controller.Get(team.Id) as OkNegotiatedContentResult<Team>;
            Assert.IsNotNull(result);

            Assert.AreEqual("Nah", result.Content.Name);
            Assert.AreEqual(false, result.Content.Access);
        }
    }
}
