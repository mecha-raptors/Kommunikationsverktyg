﻿using Kommunikationsverktyg.Models.ViewModels;
using Kommunikationsverktyg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kommunikationsverktyg.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private RoleRepository roleManager = new RoleRepository();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult ListPendingUsers()
        {
            var model = new ListUsersByRoleViewModel {
                PendingUsers = roleManager.GetUsersByRole("pending"),
                ActiveUsers = roleManager.GetUsersByRole("user")
            };
            return View(model);
        }

        public ActionResult ListCategory()
        {
            var helper = new CategoryRepository();
            var model = helper.FillModel();
            return View(model);
        }


    }
}