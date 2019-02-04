﻿using System;
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
                var newEvent = new EventModel
                {
                    Title = em.Title,
                    Description = em.Description,
                    Start = em.TimeSuggestions[0].StartTime,
                    End = em.TimeSuggestions[0].EndTime
                };

                //foreach (DateModel dm in em.TimeSuggestions)
                //{
                //    newEvent.TimeSuggestions.Add(dm);
                //}

                if (em.Invitees != null)
                {
                    foreach (string s in em.Invitees)
                    {
                        //newEvent.Invitees.Add(db.Users.Find(s));
                        System.Diagnostics.Debug.WriteLine("Id: " + s);
                        System.Diagnostics.Debug.WriteLine(", Användare: " + db.Users.Find(s).Firstname);
                    }
                }

                db.EventModels.Add(newEvent);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}