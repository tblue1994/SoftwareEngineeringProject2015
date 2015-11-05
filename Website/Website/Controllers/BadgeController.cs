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
    public class BadgeController : ApiController
    {
        IBadgeLogic Logic;
        IAttainmentLogic aLogic;

        public BadgeController(IBadgeLogic al, IAttainmentLogic alogic)
        {
            Logic = al;
            aLogic = alogic;
        }


        // PUT api/Activity/5
        public IHttpActionResult Put(Badge item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(Logic.Update(item));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Logic.Exists(item.Id))
                {
                    return NotFound();
                }

            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Activity
        [HttpPost]
        [ResponseType(typeof(Badge))]
        public IHttpActionResult Post(Badge item)
        {
            Badge Created = Logic.Create(item);
            return Ok(Created);
        }

        // DELETE api/Activity/5
        [ResponseType(typeof(Badge))]
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
        [ResponseType(typeof(Badge))]
        public IHttpActionResult Get(long id)
        {
            Badge item = Logic.Get(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpGet]
        [ResponseType(typeof(Badge))]
        public IHttpActionResult GetAll()
        {
            IQueryable<Badge> item = Logic.GetAll();
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpGet]
        [ResponseType(typeof(Badge))]
        public IHttpActionResult GetUnearnedBadges(string id)
        {
            return Ok(aLogic.UnearnedBadges(id));
        }

        [HttpGet]
        [ResponseType(typeof(Badge))]
        public IHttpActionResult GetEarnedBadges(string id)
        {
            List<Attainment> items = aLogic.GetByAccount(id).ToList();
            List<Badge> earned = new List<Badge>();
            foreach (Attainment a in items)
            {
                earned.Add(Logic.Get(a.BadgeId));
            }
            return Ok(earned);
        }
    }
}