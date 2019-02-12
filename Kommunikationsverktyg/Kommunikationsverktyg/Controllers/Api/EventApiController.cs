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
            
            //If user has already voted...
            var existingVote = date.Votes.FirstOrDefault(v => v.Voter == user);
            if (existingVote != null && existingVote.VoteFor == false)
            {
                existingVote.VoteFor = true;
            }
            else
            {
                var vote = new VoteModel
                {
                    VoteFor = true,
                    Voter = user,
                    Date = date
                };
                db.Votes.Add(vote);
            }
            
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

            var existingVote = date.Votes.FirstOrDefault(v => v.Voter == user);
            if (existingVote != null && existingVote.VoteFor == true)
            {
                existingVote.VoteFor = false;
            }
            else
            {
                var vote = new VoteModel
                {
                    VoteFor = false,
                    Voter = user,
                    Date = date
                };
                db.Votes.Add(vote);
            }

            db.SaveChanges();
        }
    }
}