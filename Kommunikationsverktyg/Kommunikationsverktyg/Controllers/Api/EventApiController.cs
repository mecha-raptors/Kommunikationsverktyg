using Kommunikationsverktyg.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace Kommunikationsverktyg.Controllers.Api
{
    [RoutePrefix("api/event")]
    public class EventApiController : ApiController
    {
        [Route("voteForTime")]
        [HttpPost]
        public void VoteForTime(List<string> dateIdList)
        {
            var db = new ApplicationDbContext();
            var userId = User.Identity.GetUserId();
            var user = db.Users.FirstOrDefault(u => u.Id == userId);
            var dateId = int.Parse(dateIdList[0]);
            var date = db.Dates.FirstOrDefault(d => d.DateId == dateId);

            if(date.VotersAgainst.Contains(user))
            {
                date.VotersAgainst.Remove(user);
            }

            date.Voters.Add(user);
            db.SaveChanges();
        }

        [Route("voteAgainstTime")]
        [HttpPost]
        public void VoteAgainstTime(List<string> dateIdList)
        {
            var db = new ApplicationDbContext();
            var userId = User.Identity.GetUserId();
            var user = db.Users.FirstOrDefault(u => u.Id == userId);
            var dateId = int.Parse(dateIdList[0]);
            var date = db.Dates.FirstOrDefault(d => d.DateId == dateId);

            if(date.Voters.Contains(user))
            {
                date.Voters.Remove(user);
            }

            date.VotersAgainst.Add(user);

            db.SaveChanges();
        }
    }
}