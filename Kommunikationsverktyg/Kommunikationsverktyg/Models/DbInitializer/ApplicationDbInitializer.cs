﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kommunikationsverktyg.Models.DbInitializer
{
    public class ApplicationDbInitializer : System.Data.Entity.DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext db)
        {
            var store = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(store);
            var adminRole = new IdentityRole { Name = "admin" };
            var userRole = new IdentityRole { Name = "user" };
            var pendingRole = new IdentityRole { Name = "pending" };

            roleManager.Create(adminRole);
            roleManager.Create(userRole);
            roleManager.Create(pendingRole);

            var hasher = new PasswordHasher();
            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var user = new ApplicationUser { Email = "admin@admin.se", UserName = "admin@admin.se", PasswordHash = hasher.HashPassword("password") };
            userManager.Create(user);
            userManager.AddToRole(user.Id, "admin");
            db.SaveChanges();
            base.Seed(db);
        }
    }
}