using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Website.BusinessLogic;
using Website.Models;

namespace Website.Controllers
{
    public class GoalController : ApiController
    {
        IGoalLogic Logic;
        IAccountLogic AccountLogic;

        public GoalController(IGoalLogic al, IAccountLogic a)
        {
            Logic = al;
            AccountLogic = a;
        }


        // PUT api/Activity/5
        public IHttpActionResult Put(Goal goal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(Logic.Update(goal));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Logic.Exists(goal.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Activity
        [HttpPost]
        [ResponseType(typeof(Goal))]
        public IHttpActionResult Post(Goal goal)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            Goal Created = Logic.Create(goal);
            return Ok(Created);
        }

        // DELETE api/Activity/5
        [ResponseType(typeof(Goal))]
        public IHttpActionResult Delete(long id)
        {
            //AL.Delete(id);
            if (Logic.Delete(id))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Logic.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        [ResponseType(typeof(Goal))]
        public IHttpActionResult Get(long id)
        {
            Goal goal = Logic.Get(id);
            if (goal == null)
            {
                return NotFound();
            }

            return Ok(goal);
        }

        /*[HttpPut]
        [ResponseType(typeof(Goal))]
        public IHttpActionResult UpdateGoals(long activityId)
        {
            return Ok(Logic.UpdateCurrentGoals(activityId));
        }*/

        [HttpPut]
        [ResponseType(typeof(Goal))]
        public IHttpActionResult FailGoals()
        {
            List<Account> accounts = AccountLogic.GetAll().ToList();
            foreach (Account a in accounts)
            {
                Logic.FailLateGoals(a.Id);
            }
            return Ok();
        }

        [HttpGet]
        [ResponseType(typeof(Attainment))]
        public IHttpActionResult GetByAccount(string id)
        {
            IQueryable<Goal> items = Logic.GetByAccount(id);
            if (items == null)
            {
                return NotFound();
            }

            return Ok(items.ToList());
        }
    }
}
