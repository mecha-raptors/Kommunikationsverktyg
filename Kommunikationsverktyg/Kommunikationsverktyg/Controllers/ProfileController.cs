using Kommunikationsverktyg.Repository;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kommunikationsverktyg.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewProfile(string id)
        {
            var userRepository = new UserRepository();
            var user = userRepository.GetUser(id);

            if (user == null)
            {
                return View("Error");
            }
            else
            {
                return View(user);
            }
        }

        public ActionResult ProfileRedirect()
        {
            var user = User.Identity.GetUserId();
            return RedirectToAction("ViewProfile", new { id = user });
        }
    }
}