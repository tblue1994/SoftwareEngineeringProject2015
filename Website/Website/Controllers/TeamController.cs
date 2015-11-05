using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Website.BusinessLogic;
using Website.Models;
using Microsoft.Practices.Unity;
using Website.Repositories;

namespace Website.Controllers
{
    public class TeamController : ApiController
    {
        private ITeamLogic Logic;
        private IMembershipLogic MemLogic;

        public TeamController(ITeamLogic logic, IMembershipLogic mem)
        {
            Logic = logic;
            MemLogic = mem;
        }

        [HttpGet]
        [ResponseType(typeof(Team))]
        public IHttpActionResult Get(long id)
        {
            Team team = Logic.Get(id);
            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

        [HttpGet]
        [ResponseType(typeof(List<Team>))]
        public IHttpActionResult Search(string key)
        {
            return Ok(Logic.Search(key));
        }

        [HttpPut]
        public IHttpActionResult Put(Team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Logic.Update(team);
                return Ok(team);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(team.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpPost]
        [ResponseType(typeof(Team))]
        public IHttpActionResult Post(string id, Team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Logic.Create(team);
            MemLogic.Create(new Membership
                {
                    AccountId = id,
                    Status = MembershipStatus.Admin,
                    TeamId = team.Id,
                    Date = DateTime.UtcNow
                });
            return Ok(team);
        }

        [HttpDelete]
        [ResponseType(typeof(Team))]
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

        [HttpGet]
        public IHttpActionResult GetByAccountId(string id)
        {
            List<Team> teams = new List<Team>();
            List<Membership> mems = MemLogic.GetByAccount(id).Where(e => e.Status == MembershipStatus.Member || e.Status == MembershipStatus.Admin).ToList();

            foreach (Membership m in mems)
            {
                teams.Add(Logic.Get(m.TeamId));
            }
            return Ok(teams);
        }

        [HttpGet]
        public IHttpActionResult GetByAccountIdInvited(string id)
        {
            List<Team> teams = new List<Team>();
            List<Membership> mems = MemLogic.GetByAccount(id).Where(e => e.Status == MembershipStatus.Invited).ToList();

            foreach (Membership m in mems)
            {
                teams.Add(Logic.Get(m.TeamId));
            }
            return Ok(teams);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Logic.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeamExists(long id)
        {
            return Logic.Exists(id);
        }
    }
}