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
    public class TargetController : ApiController
    {
        ITargetLogic Logic;

        public TargetController(ITargetLogic al)
        {
            Logic = al;
        }


        // PUT api/Activity/5
        public IHttpActionResult Put(Target item)
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

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Activity
        [HttpPost]
        [ResponseType(typeof(Target))]
        public IHttpActionResult Post(Target item)
        {
            Target Created = Logic.Create(item);
            return Ok(Created);
        }

        // DELETE api/Activity/5
        [ResponseType(typeof(Target))]
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
        [ResponseType(typeof(Target))]
        public IHttpActionResult Get(long id)
        {
            Target item = Logic.Get(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }
    }
}
