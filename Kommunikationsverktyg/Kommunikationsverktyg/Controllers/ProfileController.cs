using Kommunikationsverktyg.Models;
using Kommunikationsverktyg.Models.ViewModels;
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
            var rvm = new RegisterViewModel();
            var profileModel = new ProfileViewModel();

            profileModel.ApplicationUser = user;
            profileModel.RegisterViewModel = rvm;

            rvm.Email = user.Email;
            rvm.Firstname = user.Firstname;
            rvm.Lastname = user.Lastname;
            rvm.Phone = user.Phone;
            
            if (profileModel.ApplicationUser == null)
            {
                return View("Error");
            }
            else
            {
                return View(profileModel);
            }
        }

        public ActionResult ProfileRedirect()
        {
            var user = User.Identity.GetUserId();
            return RedirectToAction("ViewProfile", new { id = user });
        }
        
        public ActionResult ViewUserProfile(ApplicationUser user)
        {
            var pvm = new ProfileViewModel();
            pvm.ApplicationUser = user;
            return View("ViewProfile", pvm);
        }
    }
}