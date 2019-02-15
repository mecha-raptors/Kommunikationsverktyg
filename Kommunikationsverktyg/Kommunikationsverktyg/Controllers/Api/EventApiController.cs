using Kommunikationsverktyg.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
            else if (existingVote != null && existingVote.VoteFor == true)
            {
                return;
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
            else if (existingVote != null && existingVote.VoteFor == false)
            {
                return;
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

        [Route("registerEventModel")]
        [HttpPost]
        public void RegisterEventModel(List<string> dateIdList)
        {
            var db = new ApplicationDbContext();

            var userId = User.Identity.GetUserId();
            var user = db.Users.FirstOrDefault(u => u.Id == userId);

            var dateId = int.Parse(dateIdList[0]);
            var date = db.Dates.FirstOrDefault(d => d.DateId == dateId);

            var eR = db.RequestedEvents.Find(date.EventConnection.EventId);

            var newEvent = new EventModel
            {
                Title = eR.Title,
                Description = eR.Description,
                Location = eR.Location,
                Start = eR.TimeSuggestions[0].StartTime,
                End = eR.TimeSuggestions[0].EndTime,
                EventCreator = eR.EventCreator
            };

            if (db.Votes.Any(v => v.Date.EventConnection.EventId == eR.EventId))
            {
                var votes = db.Votes.Where(v => v.Date.EventConnection.EventId == eR.EventId);

                db.Votes.RemoveRange(votes);
            }

            if (db.Dates.Any(e => e.EventConnection.EventId == eR.EventId))
            {
                var dates = db.Dates.Where(d => d.EventConnection.EventId == eR.EventId);

                db.Dates.RemoveRange(dates);
            }

            var requestsToRemove = new List<RequestedEventModel>();

            var users = db.Users.ToList();

            foreach (var u in users)
            {
                if (u.EventRequests != null)
                {
                    foreach (var e in u.EventRequests)
                    {
                        if (e.EventId == eR.EventId)
                        {
                            requestsToRemove.Add(e);
                        }
                    }
                }
            }

            if (requestsToRemove.Count > 0)
            {
                foreach (var u in users)
                {
                    foreach (var e in u.EventRequests)
                    {
                        foreach (var r in requestsToRemove)
                        {
                            if (r == e)
                            {
                                u.EventRequests.Remove(e);
                            }
                        }
                    }
                    db.Users.AddOrUpdate(u);
                }
            }
            

            db.RequestedEvents.Remove(eR);

            db.EventModels.Add(newEvent);

            db.SaveChanges();

            Console.Write("Hej det funka");
        }

        [Route("cancelRequestedEvent")]
        [HttpPost]
        public void CancelRequestedEvent(List<string> dateIdList)
        {
            var db = new ApplicationDbContext();

            var dateId = int.Parse(dateIdList[0]);
            var date = db.Dates.FirstOrDefault(d => d.DateId == dateId);

            var eR = db.RequestedEvents.Find(date.EventConnection.EventId);

            if (db.Votes.Any(v => v.Date.EventConnection.EventId == eR.EventId))
            {
                var votes = db.Votes.Where(v => v.Date.EventConnection.EventId == eR.EventId);

                db.Votes.RemoveRange(votes);
            }

            if (db.Dates.Any(e => e.EventConnection.EventId == eR.EventId))
            {
                var dates = db.Dates.Where(d => d.EventConnection.EventId == eR.EventId);

                db.Dates.RemoveRange(dates);
            }

            var requestsToRemove = new List<RequestedEventModel>();

            var users = db.Users.ToList();

            foreach (var u in users)
            {
                if (u.EventRequests != null)
                {
                    foreach (var e in u.EventRequests)
                    {
                        if (e.EventId == eR.EventId)
                        {
                            requestsToRemove.Add(e);
                        }
                    }
                }
            }

            if (requestsToRemove.Count > 0)
            {
                foreach (var u in users)
                {
                    foreach (var e in u.EventRequests)
                    {
                        foreach (var r in requestsToRemove)
                        {
                            if (r == e)
                            {
                                u.EventRequests.Remove(e);
                            }
                        }
                    }
                    db.Users.AddOrUpdate(u);
                }
            }


            db.RequestedEvents.Remove(eR);

            db.SaveChanges();

            Console.Write("Hej det funka");
        }
    }
}