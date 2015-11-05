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
    public class PathController : ApiController
    {
        IPathLogic Logic;

        public PathController(IPathLogic logic)
        {
            Logic = logic;
        }


        // PUT api/Activity/5
        public IHttpActionResult Put(Path item)
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
        [ResponseType(typeof(Path))]
        public IHttpActionResult Post(Path item)
        {
            Path Created = null;
            try
            {
                Created = Logic.Create(item);
            }
            catch (Exception e)
            {

            }

            // The binary serializer is gross, so we skip it.
            return Ok(new { Data = Created.Data.Select(a => (int)a).ToArray(), ActivityId = Created.ActivityId, Id = Created.Id });
        }

        // DELETE api/Activity/5
        [ResponseType(typeof(Activity))]
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
        [ResponseType(typeof(Path))]
        public IHttpActionResult Get(long id)
        {
            Path item = Logic.Get(id);
            if (item == null)
            {
                return NotFound();
            }

            // The binary serializer is gross, so we skip it.
            return Ok(new { Data = item.Data.Select(a => (int)a).ToArray(), ActivityId = item.ActivityId, Id = item.Id });
        }

        [HttpGet]
        [ResponseType(typeof(Path))]
        public IHttpActionResult GetByActivity(long id)
        {
            Path item = Logic.GetByActivity(id);
            if (item == null)
            {
                return NotFound();
            }

            // The binary serializer is gross, so we skip it.
            return Ok(new { Data = item.Data.Select(a => (int)a).ToArray(), ActivityId = item.ActivityId, Id = item.Id });
        }
    }
}