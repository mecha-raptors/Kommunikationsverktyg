using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kommunikationsverktyg.Repository;

namespace Kommunikationsverktyg.Models.DbInitializer
{
    public class ApplicationDbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext db)
        {
            var WordFilter = new WordFilter();

            var Event = new EventModel
            {
                EventId = 1,
                Title = "Exempelmöte",
                Description = "Vi ska ses och prata.",
                Start = new DateTime(2019, 1, 10, 14, 0, 0),
                End = new DateTime(2019, 1, 10, 16, 0, 0)
            };

            db.EventModels.Add(Event);

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

            List<string> words = WordFilter.Words();
            foreach (var i in words)
            {
                if (i == "")
                continue;

                var badWord = new BadWords
                {
                    Word = i
                };
            db.BadWords.Add(badWord);
            };
          

            db.SaveChanges();
            base.Seed(db);
        }
    }
}