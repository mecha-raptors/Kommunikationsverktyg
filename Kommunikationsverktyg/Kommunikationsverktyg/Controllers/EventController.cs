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
                    TimeSuggestions = new List<DateModel>()
                };

                foreach (DateModel dm in em.TimeSuggestions)
                {
                    newEvent.TimeSuggestions.Add(dm);
                }

                db.RequestedEvents.Add(newEvent);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}