using Kommunikationsverktyg.Models;
using Kommunikationsverktyg.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kommunikationsverktyg.Controllers
{
    public class PlacardController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Placard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Placard()
        {
            
            var dbList = _db.Placards.OrderByDescending(i => i.Timestamp).ToList();
            var list = new List<PlacardModel>();
            var viewModel = new ListPlacardViewModel();

            foreach (var item in dbList)
            {
                var pModel = new PlacardModel
                {
                    Type = item.Type,
                    Message = item.Message,
                    PlacardId = item.PlacardId,
                    Timestamp = item.Timestamp,
                    Title = item.Title,
                    User = item.User,
                    Fullname = item.User.Firstname + " " + item.User.Lastname,
                    TypeName = item.Type.Type
                    
                };
                list.Add(pModel);
            }
            viewModel.Placards = list;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Placard(ListPlacardViewModel model)
        {
            try
            {
                var pType = model.PlacardType.ToString();
                if (pType == "Forskning")
                {
                    model.PlacardTypeId = 1;
                }
                else
                {
                    model.PlacardTypeId = 2;
                }

                var userId = User.Identity.GetUserId();
                var user = _db.Users.FirstOrDefault(u => u.Id == userId);
                var category = _db.PlacardTypes.FirstOrDefault(c => c.PlacardTypeModelId == model.PlacardTypeId);

                var placardModel = new PlacardModel
                {
                    PlacardId = model.PlacardId,
                    Message = model.Message,
                    Timestamp = DateTime.Now,
                    Title = model.Title,
                    Type = category,
                    User = user
                };
                
                _db.Placards.Add(placardModel);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            return RedirectToAction("Placard");
        } 
    }
}