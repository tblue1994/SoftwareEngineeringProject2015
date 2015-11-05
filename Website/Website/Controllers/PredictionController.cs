using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Timers;
using System.Web.Http;
using System.Web.Http.Description;
using Website.BusinessLogic;
using Website.Models;
using Website.Repositories;

namespace Website.Controllers
{
    public class PredictionController : ApiController
    {
        private IAccountLogic accounts;
        private PredictionLogic logic;

        public PredictionController(IAccountLogic accounts, PredictionLogic logic)
        {
            this.accounts = accounts;
            this.logic = logic;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        public IHttpActionResult Test(string id, Activity activity)
        {
            Account account = accounts.Get(id);

            var data = RandomForestModel.wrapRow(0, "", "", DateTime.Now,
                (double)account.Height * 0.0254,
                (double)account.Weight * 0.4536,
                activity.Duration.TotalSeconds,
                activity.Distance,
                activity.Steps,
                "");
            ActivityType result = ActivityType.Walking;

            string name = logic.ApplyTree(data);
            if (name == "R")
            {
                result = ActivityType.Running;
            }
            else if (name == "W")
            {
                result = ActivityType.Walking;
            }
            else if (name == "J")
            {
                result = ActivityType.Jogging;
            }
            else if (name == "B")
            {
                result = ActivityType.Biking;
            }

            return Ok(result);
        }

        [HttpGet]
        public IHttpActionResult RecreateTree()
        {
            logic.RecreateTree();
            return Ok();
        }
    }
}