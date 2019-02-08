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
                var newEvent = new RequestedEventModel
                {
                    Title = em.Title,
                    Description = em.Description,
                    TimeSuggestions = new List<DateModel>(),
                    Invitees = new List<ApplicationUser>()
                    
                };

                newEvent.TimeSuggestions = em.TimeSuggestions;
                
                if (em.Invitees != null)
                {
                    var emailNotification = new EmailNotification();
                    foreach (string s in em.Invitees)
                    {
                        newEvent.Invitees.Add(db.Users.Find(s));

                        var invitee = db.Users.FirstOrDefault(u => u.Id == s);
                        emailNotification.SendEventInvitationEmail(invitee.Email);
                    }
                }

                db.RequestedEvents.Add(newEvent);
                db.SaveChanges();


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