using Kommunikationsverktyg.Models;
using Kommunikationsverktyg.Models.ViewModels;
using Kommunikationsverktyg.Repository;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
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
            ApplicationDbContext db = new ApplicationDbContext();

            RequestedEventViewModel viewModel = new RequestedEventViewModel
            {
                InvitableUsers = new Dictionary<string, string>(),
                Invitees = new List<string>()
            };

            foreach (var u in db.Users)
            {
                if (u.Id != User.Identity.GetUserId())
                {
                    viewModel.InvitableUsers.Add(u.Id, u.Firstname + " " + u.Lastname);
                }
            }

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

        public ActionResult NotificationsView()
        {
            return View();
        }

        public JsonResult GetEventNotifications()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            
                var events = db.RequestedEvents.ToList();
                var testtest = new List<JsonEventRequestModel>();
                foreach (var e in events)
                {
                    if (e.Invitees != null)
                {
                    foreach (var u in e.Invitees)
                    {
                        if (u.Id.Equals(User.Identity.GetUserId()))
                        {
                            JsonEventRequestModel t = new JsonEventRequestModel
                            {
                                Title = e.Title,
                                Description = e.Description,
                                TimeSuggestions = e.TimeSuggestions
                            };
                            testtest.Add(t);
                            
                        }

                    }
                }
                }
            return new JsonResult { Data = testtest, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            
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

        //[HttpPost]
        //public ActionResult DeletePost(ListFormalBlogViewModel blogModel)
        //{
        //    var helper = new FormalBlogRepository();
        //    helper.DeletePost(blogModel);
        //    var model = helper.GetFormalPosts();
            
        //    return RedirectToAction("FormalBlog");
        //}

        public JsonResult SaveData(string getepassdata)//WebMethod to Save the data  
        {
            try
            {
                //var serializeData = JsonConvert.DeserializeObject<List<FormalBlogModel>>(getepassdata);
                //Console.WriteLine(serializeData);
            }
            catch (Exception)
            {
                return Json("fail");
            }

            return Json("success");
        }

        public ActionResult InformalBlog()
        {

            try
            {
                var helper = new InformalBlogRepository();
                var model = helper.GetInformalPosts();
                return View(model);
            }
            catch
            {
                var model = new ListInformalBlogViewModel
                {
                    Posts = new List<InformalBlogViewModel>()
                };
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult InformalBlog(ListInformalBlogViewModel BlogList)
        {
            var helper = new InformalBlogRepository();

            if (!ModelState.IsValid)
            {
                var model = helper.GetInformalPosts();
                return View(model);
            }

            try
            {
                BlogList.SenderId = User.Identity.GetUserId();
                helper.SavePost(BlogList);
                var model = helper.GetInformalPosts();
                return RedirectToAction("InformalBlog");
            }
            catch
            {
                ModelState.AddModelError("", "Något gick fel.");
                var model = helper.GetInformalPosts();
                return View(model);

            }
        }

        public ActionResult Search()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var model = new ListUsersByRoleViewModel
            {
                ActiveUsers = new List<ApplicationUser>()
            };
            var currUser = User.Identity.GetUserId();
            model.ActiveUsers = db.Users.Where(u => u.Id != currUser).ToList();

            return View(model);
        }
        
    }
}