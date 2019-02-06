using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kommunikationsverktyg.Models;
using Kommunikationsverktyg.Models.ViewModels;

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
                    foreach (var i in em.Invitees)
                    {
                        newEvent.Invitees.Add(db.Users.Find(i));
                    }
                }

                db.RequestedEvents.Add(newEvent);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}