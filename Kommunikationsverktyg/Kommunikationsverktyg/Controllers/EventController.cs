using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kommunikationsverktyg.Models;

namespace Kommunikationsverktyg.Controllers
{
    public class EventController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddEvent(EventViewModel em)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var newEvent = new EventModel
                {
                    Title = em.Title,
                    Description = em.Description,
                    Start = em.Start,
                    End = em.End
                };

                db.EventModels.Add(newEvent);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}