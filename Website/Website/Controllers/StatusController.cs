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
    public class StatusController : ApiController
    {
        IStatusLogic Logic;

        public StatusController(IStatusLogic logic)
        {
            Logic = logic;
        }


        [HttpPut]
        [ResponseType(typeof(Status))]
        public IHttpActionResult Put(Status item)
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
                else
                {
                    throw;
                }
            }
        }

        // POST api/Activity
        [HttpPost]
        [ResponseType(typeof(Status))]
        public IHttpActionResult Post(Status item)
        {
            Status Created = Logic.Create(item);
            return Ok(Created);
        }

        // DELETE api/Activity/5
        [ResponseType(typeof(Status))]
        public IHttpActionResult Delete(long id)
        {
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
        [ResponseType(typeof(Status))]
        public IHttpActionResult Get(long id)
        {
            Status item = Logic.Get(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpGet]
        [ResponseType(typeof(List<Status>))]
        public IHttpActionResult GetByAccount(string id)
        {
            IQueryable<Status> items = Logic.GetByAccount(id);
            if (items == null)
            {
                return NotFound();
            }

            return Ok(items.ToList());
        }

        [HttpGet]
        [ResponseType(typeof(List<Status>))]
        public IHttpActionResult GetByTeam(long id)
        {
            IQueryable<Status> items = Logic.GetByTeam(id);
            if (items == null)
            {
                return NotFound();
            }

            return Ok(items.ToList());
        }
    }
}
