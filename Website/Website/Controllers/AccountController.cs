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
using Website.Repositories;

namespace Website.Controllers
{
    public class AccountController : ApiController
    {
        private IAccountLogic Logic;
        private IMembershipLogic mLogic;
        public AccountController(IAccountLogic logic, IMembershipLogic mlogic)
        {
            Logic = logic;
            mLogic = mlogic;
        }

        [HttpGet]
        [ResponseType(typeof(List<Account>))]
        public IHttpActionResult Search(string key)
        {
            return Ok(Logic.Search(key));
        }

        [HttpGet]
        [ResponseType(typeof(Account))]
        public IHttpActionResult Get(string id)
        {
            Account account = Logic.Get(id);
            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        [HttpGet]
        [ResponseType(typeof(Account))]
        public IHttpActionResult Facebook(long facebookId)
        {
            Account account = Logic.GetByFacebook(facebookId);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpGet]
        [ResponseType(typeof(Account))]
        public IHttpActionResult Twitter(long twitterId)
        {
            Account account = Logic.GetByTwitter(twitterId);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpPut]
        public IHttpActionResult Put(Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Logic.Update(account);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(account.Id))
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

        [HttpPost]
        [ResponseType(typeof(Account))]
        public IHttpActionResult Post(Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Logic.Create(account);

            return Ok(account);
        }

        [HttpDelete]
        [ResponseType(typeof(Account))]
        public IHttpActionResult Delete(String id)
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
        [ResponseType(typeof(Account))]
        public IHttpActionResult GetByTeamId(long id)
        {
            List<Account> members = new List<Account>();
            List<Membership> mems = mLogic.GetByTeam(id).Where(e => e.Status == MembershipStatus.Member || e.Status == MembershipStatus.Admin).ToList();
            foreach (Membership m in mems)
            {
                members.Add(Logic.Get(m.AccountId));
            }
            return Ok(members);
        }

        [HttpGet]
        [ResponseType(typeof(Account))]
        public IHttpActionResult GetBannedAccountsByTeam(long id)
        {
            List<Account> members = new List<Account>();
            List<Membership> mems = mLogic.GetByTeam(id).Where(e => e.Status == MembershipStatus.Banned).ToList();
            foreach (Membership m in mems)
            {
                members.Add(Logic.Get(m.AccountId));
            }
            return Ok(members);
        }

        [HttpGet]
        public IHttpActionResult LeaderboardDistance(long id)
        {
            //List<Account> members = new List<Account>();
            List<Tuple<Account, double>> leaders = new List<Tuple<Account, double>>();
            List<Membership> mems = mLogic.GetByTeam(id).Where(e => e.Status == MembershipStatus.Admin || e.Status == MembershipStatus.Member).ToList();
            foreach (Membership m in mems)
            {
                Account a = Logic.Get(m.AccountId);
                double total = 0;
                foreach (Activity act in a.Activities)
                {
                    total += act.Distance;
                }
                Tuple<Account, double> t = new Tuple<Account, double>(a, total);

                leaders.Add(t);
            }
            leaders = leaders.OrderByDescending(e => e.Item2).Take(5).ToList();
            // We have to convert it to a dynamic item, as the Json serializer here
            // doesn't match the windows phone deserializer for tuples.
            return Ok(leaders.Select(e => new { Item1 = e.Item1, Item2 = e.Item2 }).ToList());
        }

        [HttpGet]
        [ResponseType(typeof(Account))]
        public IHttpActionResult LeaderboardAttainments(long id)
        {
            //List<Account> members = new List<Account>();
            List<Tuple<Account, int>> leaders = new List<Tuple<Account, int>>();
            List<Membership> mems = mLogic.GetByTeam(id).Where(e => e.Status == MembershipStatus.Admin || e.Status == MembershipStatus.Member).ToList();
            foreach (Membership m in mems)
            {
                Account a = Logic.Get(m.AccountId);
                int i = a.Attainments.Count();
                Tuple<Account, int> t = new Tuple<Account, int>(a, i);

                leaders.Add(t);
            }
            leaders = leaders.OrderByDescending(e => e.Item2).Take(5).ToList();
            // We have to convert it to a dynamic item, as the Json serializer here
            // doesn't match the windows phone deserializer for tuples.
            return Ok(leaders.Select(e => new { Item1 = e.Item1, Item2 = e.Item2 }).ToList());

        }

        [HttpGet]
        [ResponseType(typeof(List<Account>))]
        public IHttpActionResult GetAccountsInvitedToTeam(long id)
        {
            List<Account> members = new List<Account>();
            List<Membership> mems = mLogic.GetByTeam(id).Where(e => e.Status == MembershipStatus.Invited).ToList();
            foreach (Membership m in mems)
            {
                members.Add(Logic.Get(m.AccountId));
            }
            return Ok(members);
        }

        [HttpGet]
        [ResponseType(typeof(List<Account>))]
        public IHttpActionResult GetTeammateWithBadges(long badgeId, string userId)
        {
            return Ok(Logic.TeammatesWithBadge(userId, badgeId));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Logic.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccountExists(string id)
        {
            return Logic.AccountExists(id);
        }
    }
}