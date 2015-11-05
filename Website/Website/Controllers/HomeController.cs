using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.Cookies.AllKeys.Contains("UserId"))
            {
                var userId = Request.Cookies.Get("UserId");
                return RedirectToAction("AccountInformation", "AccountInfo", new { id = int.Parse(userId.Value) });
            }
            return View();
        }

        public ActionResult NoUserData()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Response.Cookies.Get("UserId").Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}
