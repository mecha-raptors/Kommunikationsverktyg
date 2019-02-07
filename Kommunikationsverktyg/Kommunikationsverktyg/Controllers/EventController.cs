using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
                        //newEvent.Invitees.Add(db.Users.Find(s));
                        System.Diagnostics.Debug.WriteLine("Id: " + s);
                        System.Diagnostics.Debug.WriteLine(", Användare: " + db.Users.Find(s).Firstname);

                        var invitee = db.Users.FirstOrDefault(u => u.Id == s);
                        emailNotification.SendEventInvitationEmail(invitee.Email);
                    }
                }

                db.RequestedEvents.Add(newEvent);
                db.SaveChanges();


            }
            return RedirectToAction("Index", "Home");
        }
    }
}