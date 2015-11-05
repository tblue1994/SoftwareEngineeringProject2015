using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Website.BusinessLogic;
using Website.Models;

namespace Website.Controllers
{
    public class ReportController : ApiController
    {
        IReportLogic Logic;

        public ReportController(IReportLogic logic)
        {
            Logic = logic;
        }


        [HttpPut]
        [ResponseType(typeof(Report))]
        public IHttpActionResult Put(Report item)
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

        // DELETE api/Activity/5
        [ResponseType(typeof(Report))]
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
        [ResponseType(typeof(Report))]
        public IHttpActionResult Get(long id)
        {
            Report item = Logic.Get(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpGet]
        [ResponseType(typeof(Report))]
        public IHttpActionResult GetByAccount(string id)
        {
            IQueryable<Report> items = Logic.GetByAccount(id);
            if (items == null)
            {
                return NotFound();
            }

            return Ok(items.ToList());
        }
        
        [HttpPost]
        [ResponseType(typeof(Report))]
        public IHttpActionResult Post()
        {
            Logic.Create();
            return Ok();
        }

        [HttpGet]
        [ResponseType(typeof(Report))]
        public IHttpActionResult GetMiniReportByAccount(string id)
        {
            return Ok(Logic.MiniReport(id));
        }
    }
}