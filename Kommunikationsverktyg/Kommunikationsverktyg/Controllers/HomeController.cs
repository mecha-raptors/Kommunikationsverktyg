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
    
    [Authorize(Roles = "user,admin")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var viewModel = new EventViewModel();
            return View(viewModel);
        }

        public ActionResult FormalBlog()
        {

            try
            {
                var helper = new FormalBlogRepository();
                var model = helper.GetFormalPosts();
                return View(model);
            }
            catch
            {
                var model = new ListFormalBlogViewModel
                {
                    Posts = new List<FormalBlogViewModel>()
                };
                return View(model);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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
        [HttpPost]
        public ActionResult FormalBlog(ListFormalBlogViewModel BlogList)
        {
            var helper = new FormalBlogRepository();

            if (!ModelState.IsValid) {
                var model = helper.GetFormalPosts();
                return View(model);
            }

            try
            {
                BlogList.SenderId = User.Identity.GetUserId();
                helper.SavePost(BlogList);
                var model = helper.GetFormalPosts();
                return RedirectToAction("FormalBlog");
            }
            catch
            {
                ModelState.AddModelError("", "Något gick fel. Tänk på att du inte kan ladda upp bildfiler i den formella boggen.");
                var model = helper.GetFormalPosts();
                return View(model);
                
            }
        }
    }
}