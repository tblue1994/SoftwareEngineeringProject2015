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
    public class FoodController : ApiController
    {
        IFoodLogic Logic;

        public FoodController(IFoodLogic logic)
        {
            Logic = logic;
        }


        [HttpPut]
        [ResponseType(typeof(Food))]
        public IHttpActionResult Put(Food item)
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
        [ResponseType(typeof(Food))]
        public IHttpActionResult Post(Food item)
        {
            Food Created = Logic.Create(item);
            return Ok(Created);
        }

        // DELETE api/Activity/5
        [ResponseType(typeof(Food))]
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
        [ResponseType(typeof(Food))]
        public IHttpActionResult Get(long id)
        {
            Food item = Logic.Get(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpGet]
        [ResponseType(typeof(Food))]
        public IHttpActionResult GetByAccount(string id)
        {
            IQueryable<Food> items = Logic.GetByAccount(id);
            if (items == null)
            {
                return NotFound();
            }

            return Ok(items.ToList());
        }

        [HttpGet]
        [ResponseType(typeof(Food))]
        public IHttpActionResult GetByTeam(long id)
        {
            IQueryable<Food> items = Logic.GetByTeam(id);
            if (items == null)
            {
                return NotFound();
            }

            return Ok(items.ToList());
        }
    }
}