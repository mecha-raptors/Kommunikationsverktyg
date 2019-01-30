using Kommunikationsverktyg.Models;
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

            var viewmodel = new RegisterViewModel();

            return View(viewmodel);
        }

        public JsonResult GetEvents()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var events = db.EventModels.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}