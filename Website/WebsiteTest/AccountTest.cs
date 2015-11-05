using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Website.Controllers;
using System.Threading;
using Website.Models;
using System.Web.Http.Results;

namespace WebsiteTest
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void AccountPostGet()
        {
            AccountController controller = new AccountController(new MockDatabase());
            controller.Create(new Account
            {
                FacebookId = 123,
                FullName = "Hey Guys",
                PreferredName = "Hey",
                Height = 100,
                Weight = 120,
                Birthdate = DateTime.Now,
                Zip = 80303,
                Sex = false
            });

            var account = controller.Get(1) as OkNegotiatedContentResult<Account>;
            var faccount = controller.Facebook(123) as OkNegotiatedContentResult<Account>;

            Assert.IsNotNull(account);
            Assert.IsNotNull(faccount);

            Assert.AreEqual(account.Content.FullName, "Hey Guys");

            Assert.AreEqual(account.Content.Height, 100);
            Assert.AreEqual(account.Content.Weight, 120);
        }

        [TestMethod]
        public void AccountPostPutGet()
        {
            AccountController controller = new AccountController(new MockDatabase());
            var original = controller.Create(new Account
            {
                FacebookId = 123,
                FullName = "Hey Guys",
                PreferredName = "Hey",
                Height = 100,
                Weight = 120,
                Birthdate = DateTime.Now,
                Zip = 80303,
                Sex = false
            }) as OkNegotiatedContentResult<Account>;

            controller.Modify(new Account
            {
                Id = original.Content.Id,
                FacebookId = 123,
                FullName = "Hey s",
                PreferredName = "Hasey",
                Height = 1001,
                Weight = 1203,
                Birthdate = DateTime.Now,
                Zip = 80303,
                Sex = false
            });

            var account = controller.Get(1) as OkNegotiatedContentResult<Account>;
            var faccount = controller.Facebook(123) as OkNegotiatedContentResult<Account>;

            Assert.IsNotNull(account);
            Assert.IsNotNull(faccount);

            Assert.AreEqual(account.Content.FullName, "Hey s");

            Assert.AreEqual(account.Content.Height, 1001);
            Assert.AreEqual(account.Content.Weight, 1203);
        }
    }
}
