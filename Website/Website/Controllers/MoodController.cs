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
    public class MoodController : ApiController
    {
        IMoodLogic Logic;

        public MoodController(IMoodLogic al)
        {
            Logic = al;
        }


        // PUT api/Activity/5
        public IHttpActionResult Put(Mood item)
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
        [ResponseType(typeof(Mood))]
        public IHttpActionResult Post(Mood item)
        {
            Mood Created = Logic.Create(item);
            return Ok(Created);
        }

        // DELETE api/Activity/5
        [ResponseType(typeof(Mood))]
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
        [ResponseType(typeof(Mood))]
        public IHttpActionResult Get(long id)
        {
            Mood item = Logic.Get(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpGet]
        [ResponseType(typeof(Mood))]
        public IHttpActionResult GetAll()
        {
            IQueryable<Mood> item = Logic.GetAll();
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }
    }
    
}
