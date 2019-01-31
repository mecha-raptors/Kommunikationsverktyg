using Kommunikationsverktyg.Models;
using Kommunikationsverktyg.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kommunikationsverktyg.Controllers
{
    
    [Authorize(Roles = "user,admin")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var viewModel = new EventViewModel();
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            var viewModel = new RegisterViewModel();
            var profileModel = new ProfileViewModel();
            profileModel.RegisterViewModel = viewModel;

            using (ApplicationDbContext _db = new ApplicationDbContext())
            {
                var currUser = _db.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
                viewModel.Email = currUser.Email;
                viewModel.Firstname = currUser.Firstname;
                viewModel.Lastname = currUser.Lastname;
                viewModel.Phone = currUser.Phone;
            }

            return View(profileModel);
        }

        public JsonResult GetEvents()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var events = db.EventModels.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [AllowAnonymous]
        public ActionResult WaitForVerification()
        {
            return View();
        }
    }
}