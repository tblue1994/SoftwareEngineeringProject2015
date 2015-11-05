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
    public class AttainmentController : ApiController
    {
        IAttainmentLogic Logic;

        public AttainmentController(IAttainmentLogic logic)
        {
            Logic = logic;
        }


        // PUT api/Activity/5
        public IHttpActionResult Put(Attainment item)
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
        [ResponseType(typeof(Attainment))]
        public IHttpActionResult Post(Attainment item)
        {
            Attainment Created = Logic.Create(item);
            return Ok(Created);
        }

        // DELETE api/Activity/5
        [ResponseType(typeof(Attainment))]
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
        [ResponseType(typeof(Attainment))]
        public IHttpActionResult Get(long id)
        {
            Attainment item = Logic.Get(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpGet]
        [ResponseType(typeof(Attainment))]
        public IHttpActionResult GetByAccount(string id)
        {
            IQueryable<Attainment> items = Logic.GetByAccount(id);
            if (items == null)
            {
                return NotFound();
            }

            return Ok(items.ToList());
        }

        [HttpGet]
        [ResponseType(typeof(Attainment))]
        public IHttpActionResult GetByTeam(long id)
        {
            IQueryable<Attainment> items = Logic.GetByTeam(id);
            if (items == null)
            {
                return NotFound();
            }

            return Ok(items.ToList());
        }

        /*[HttpGet]
        [ResponseType(typeof(Attainment))]
        public IHttpActionResult UpdateAttainments(long activityId)
        {
            return Ok(Logic.GetNewAttainments(activityId));
        }*/
    }
}