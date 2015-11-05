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
using Website.Models;
using Website.Repositories;
using Microsoft.Practices.Unity;
using Website.BusinessLogic;

namespace Website.Controllers
{
    public class MembershipController : ApiController
    {
        private IMembershipLogic Logic;
        private ITeamLogic tLogic;
        public MembershipController(IMembershipLogic logic, ITeamLogic tl)
        {
            Logic = logic;
            tLogic = tl;
        }

        [HttpGet]
        [ResponseType(typeof(Membership))]
        public IHttpActionResult Get(long id)
        {
            Membership membership = Logic.Get(id);
            if (membership == null)
            {
                return NotFound();
            }

            return Ok(membership);
        }

        [HttpGet]
        public IHttpActionResult ByAccount(string id)
        {
            IQueryable<Membership> membership = Logic.GetByAccount(id);

            if (membership == null)
            {
                return NotFound();
            }

            return Ok(membership.ToList());
        }

        [HttpGet]
        public IHttpActionResult ByTeam(long id)
        {
            IQueryable<Membership> membership = Logic.GetByTeam(id);

            if (membership == null)
            {
                return NotFound();
            }

            return Ok(membership.ToList());
        }

        [HttpPut]
        [ResponseType(typeof(Membership))]
        public IHttpActionResult Invite(long teamId, string toId, string fromId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Membership Inviter = Logic.GetByTeamAndAccount(teamId, fromId);
            Membership Invitee = Logic.GetByTeamAndAccount(teamId, toId);

            if (Inviter != null)
            {
                if (Invitee == null)
                {
                    return Ok(Logic.Create(new Membership { AccountId = toId, TeamId = teamId, Status = MembershipStatus.Invited, Date = DateTime.UtcNow }));
                }
                else if (Invitee.Status == MembershipStatus.Left)
                {
                    Invitee.Status = MembershipStatus.Invited;
                    return Ok(Logic.Update(Invitee));
                }
                else
                {
                    return BadRequest("The invitee is already in the team, invited or banned.");
                }
            }
            else
            {
                return BadRequest("The inviter doesn't exist");
            }
        }

        [HttpPut]
        public IHttpActionResult Accept(long teamId, string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Membership membership = Logic.GetByTeamAndAccount(teamId, userId);

            if (membership == null)
            {
                return Ok(Logic.Create(new Membership { AccountId = userId, TeamId = teamId, Status = MembershipStatus.Member, Date = DateTime.UtcNow }));
            }
            else if (membership.Status == MembershipStatus.Invited || membership.Status == MembershipStatus.Left)
            {
                membership.Status = MembershipStatus.Member;
                return Ok(Logic.Update(membership));

            }
            else
            {
                return BadRequest("The account is already in the team.");
            }
        }

        [HttpPut]
        // DELETE api/Membership/5
        [ResponseType(typeof(Membership))]
        public IHttpActionResult Leave(long teamId, string userId)
        {
            Membership membership = Logic.GetByTeamAndAccount(teamId, userId);

            if (membership == null)
            {
                return NotFound();
            }
            IQueryable<Membership> memberships = Logic.GetByTeam(teamId);

            /*if (membership.Status == MembershipStatus.Admin &&
                memberships.Where(m => m.Status == MembershipStatus.Member || m.Status == MembershipStatus.Admin).Count() > 1)
            {
                return BadRequest("You can't leave");
            }*/

            Logic.Leave(membership.Id);

            if (memberships.Count() == 1)
            {
                tLogic.Delete(teamId);
                return Ok("Team has also been deleted");
            }

            return Ok(membership);
        }

        [HttpDelete]
        // DELETE api/Membership/5
        [ResponseType(typeof(Membership))]
        public IHttpActionResult Decline(long teamId, string userId)
        {
            Membership membership = Logic.GetByTeamAndAccount(teamId, userId);

            if (membership == null)
            {
                return NotFound();
            }

            Logic.Delete(membership.Id);

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult GiveAdminStatus(long teamId, string fromId, string toId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Membership fromMembership = Logic.GetByTeamAndAccount(teamId, fromId);
            Membership toMembership = Logic.GetByTeamAndAccount(teamId, toId);

            if (fromMembership.Status != MembershipStatus.Admin)
            {
                return BadRequest("The member trying to give admin status is not an admin.");
            }
            if (!toMembership.Status.IsMember())
            {
                return BadRequest("The member receiving admin status is not a member of the team.");
            }

            fromMembership.Status = MembershipStatus.Member;
            toMembership.Status = MembershipStatus.Admin;
            Logic.Update(fromMembership);
            Logic.Update(toMembership);

            return Ok("Promotion Successful");
        }

        [HttpPut]
        public IHttpActionResult Ban(long teamId, string fromId, string toId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Membership authMem = Logic.GetByTeamAndAccount(teamId, fromId);
            Membership toMembership = Logic.GetByTeamAndAccount(teamId, toId);

            if (!authMem.Status.CanModerate())
            {
                return BadRequest("The member trying to ban is not an admin.");
            }
            if (!toMembership.Status.IsMember())
            {
                return BadRequest("The person being banned is not a member of the team.");
            }

            toMembership.Status = MembershipStatus.Banned;
            Logic.Update(toMembership);

            return Ok(toMembership);
        }

        [HttpPut]
        public IHttpActionResult Unban(long teamId, string fromId, string toId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Membership authMem = Logic.GetByTeamAndAccount(teamId, fromId);
            Membership toMembership = Logic.GetByTeamAndAccount(teamId, toId);

            if (!authMem.Status.CanModerate())
            {
                return BadRequest("The member trying to ban is not an admin.");
            }
            if (!toMembership.Status.IsBanned())
            {
                return BadRequest("The person is not banned.");
            }

            toMembership.Status = MembershipStatus.Invited;
            Logic.Update(toMembership);

            return Ok(toMembership);
        }

        [HttpGet]
        public IHttpActionResult GetByAccountAndTeam(long teamId, string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Membership membership = Logic.GetByTeamAndAccount(teamId, userId);

            if (membership == null)
            {
                return NotFound();
            }
            return Ok(membership);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Logic.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MembershipExists(long id)
        {
            return Logic.Exists(id);
        }
    }
}