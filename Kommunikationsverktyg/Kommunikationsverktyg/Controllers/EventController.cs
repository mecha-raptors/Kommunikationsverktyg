using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using Kommunikationsverktyg.Models;
using Kommunikationsverktyg.Models.ViewModels;
using Kommunikationsverktyg.Repository;
using Microsoft.AspNet.Identity;

namespace Kommunikationsverktyg.Controllers
{
    public class EventController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddEvent(RequestedEventViewModel em)
        {
           
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (em.TimeSuggestions.Any(i => i.StartTime >= i.EndTime))
                {
                    ModelState.AddModelError("", "Startiden kan inte vara efter sluttiden");
                }
                if (!ModelState.IsValid)
                {
                    em.InvitableUsers = new Dictionary<string, string>();
                    em.Invitees = new List<string>();
                    foreach (var u in db.Users)
                    {
                        if (u.Id != User.Identity.GetUserId())
                        {
                            em.InvitableUsers.Add(u.Id, u.Firstname + " " + u.Lastname);
                        }
                    }
                    return View("../Home/Index", em);
                }

                if (em.Invitees != null)
                {
                    var newEvent = new RequestedEventModel
                    {
                        Title = em.Title,
                        Description = em.Description,
                        TimeSuggestions = new List<DateModel>(),
                        Invitees = new List<ApplicationUser>()

                    };

                    newEvent.TimeSuggestions = em.TimeSuggestions;

                    var emailNotification = new EmailNotification();
                    foreach (string s in em.Invitees)
                    {
                        newEvent.Invitees.Add(db.Users.Find(s));

                        var invitee = db.Users.FirstOrDefault(u => u.Id == s);
                        emailNotification.SendEventInvitationEmail(invitee.Email);
                    }
                    db.RequestedEvents.Add(newEvent);
                    db.SaveChanges();
                } else
                {
                    var newEvent = new EventModel
                    {
                        Title = em.Title,
                        Description = em.Description,
                        Start = em.TimeSuggestions[0].StartTime,
                        End = em.TimeSuggestions[0].EndTime
                    };

                    db.EventModels.Add(newEvent);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DownloadIcal(string downloadFileName)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var allEvents = db.EventModels.ToList();
            var calendar = new Ical.Net.Calendar();

            foreach (var e in allEvents)
            {
                calendar.Events.Add(new CalendarEvent
                {
                    Class = "PUBLIC",
                    Summary = e.Title,
                    Created = new CalDateTime(DateTime.Now),
                    Description = e.Description,
                    Start = new CalDateTime(Convert.ToDateTime(e.Start)),
                    End = new CalDateTime(Convert.ToDateTime(e.End)),
                    Sequence = 0,
                    Uid = Guid.NewGuid().ToString(),
                    Location = "Ett Rum"
                });
            }

            var serializer = new CalendarSerializer(new SerializationContext());
            var serializedCalendar = serializer.SerializeToString(calendar);
            var bytesCalendar = Encoding.UTF8.GetBytes(serializedCalendar);
            MemoryStream ms = new MemoryStream(bytesCalendar);

            return File(ms, "text/calendar", downloadFileName);
        }
    }
}