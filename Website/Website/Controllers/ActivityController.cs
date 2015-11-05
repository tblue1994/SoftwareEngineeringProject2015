using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Website.BusinessLogic;
using Website.Models;
using Website.Repositories;

namespace Website.Controllers
{
    public class ActivityController : ApiController
    {
        IActivityLogic AL;
        IAttainmentLogic attLogic;
        IGoalLogic gLogic;

        public ActivityController(IActivityLogic al, IAttainmentLogic l, IGoalLogic g)
        {
            AL = al;
            attLogic = l;
            gLogic = g;
        }


        // PUT api/Activity/5
        public IHttpActionResult Put(Activity activity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(AL.Update(activity));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AL.ActivityExists(activity.Id))
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
        [ResponseType(typeof(Activity))]
        public IHttpActionResult Post(Activity activity)
        {
            Activity Created = AL.Create(activity);

            List<Goal> goals = gLogic.UpdateCurrentGoals(Created.Id);
            List<Attainment> att = attLogic.GetNewAttainments(Created.Id);

            // Have to break up the tuple because the serializer on the server
            // differs from the deserializer on the phone.
            return Ok(new { Item1 = Created, Item2 = goals, Item3 = att });
        }

        // DELETE api/Activity/5
        [ResponseType(typeof(Activity))]
        public IHttpActionResult Delete(long id)
        {
            //AL.Delete(id);
            if (AL.Delete(id))
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
                AL.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        [ResponseType(typeof(Activity))]
        public IHttpActionResult Get(long id)
        {
            Activity activity = AL.Get(id);
            if (activity == null)
            {
                return NotFound();
            }

            return Ok(activity);
        }

        [HttpGet]
        [ResponseType(typeof(List<Activity>))]
        public IHttpActionResult GetByAccount(string id)
        {
            IQueryable<Activity> items = AL.GetByAccount(id);
            if (items == null)
            {
                return NotFound();
            }

            return Ok(items.ToList());
        }

        [HttpGet]
        [ResponseType(typeof(List<Activity>))]
        public IHttpActionResult GetByTeam(long id)
        {
            IQueryable<Activity> items = AL.GetByTeam(id);
            if (items == null)
            {
                return NotFound();
            }

            return Ok(items.ToList());
        }
    }
}