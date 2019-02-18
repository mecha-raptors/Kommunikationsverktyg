using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kommunikationsverktyg.Repository;

namespace Kommunikationsverktyg.Models.DbInitializer
{
    public class ApplicationDbInitializer : System.Data.Entity.DropCreateDatabaseAlways<ApplicationDbContext>
    {
        public IEnumerable<object> ValidationErrors { get; private set; }

        protected override void Seed(ApplicationDbContext db)
        {
            var WordFilter = new WordFilter();

            var Event = new EventModel
            {
                EventId = 1,
                Title = "Exempelmöte",
                Description = "Vi ska ses och prata.",
                Start = new DateTime(2019, 1, 10, 14, 0, 0),
                End = new DateTime(2019, 1, 10, 16, 0, 0),
                Location = "N2063"
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
            var user = new ApplicationUser { Email = "admin@admin.se", UserName = "admin@admin.se", Firstname = "Admin", Lastname = "Adminsson", Title = "Hövding", PasswordHash = hasher.HashPassword("password") };
            userManager.Create(user);
            var user2 = new ApplicationUser { Email = "user@user.se",
                                              UserName = "user@user.se",
                                              PasswordHash = hasher.HashPassword("password"),
                                              Title = "Rektor",
                                              Firstname = "Darth",
                                              Lastname = "Vader"};
            var user3 = new ApplicationUser
            {
                Email = "user1@user.se",
                UserName = "user1@user.se",
                PasswordHash = hasher.HashPassword("password"),
                Title = "Lektor",
                Firstname = "James",
                Lastname = "RavenLord"
            };
            userManager.Create(user2);
            userManager.Create(user3);
            userManager.AddToRole(user.Id, "admin");
            userManager.AddToRole(user2.Id, "user");
            userManager.AddToRole(user3.Id, "user");
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

            var research = new PlacardTypeModel
            {
                Type = "Forskning"
            };
            var education = new PlacardTypeModel
            {
                Type = "Utbildning"
            };
            var category = new CategoryModel
            {
                Type = "Övrigt"
            };
            db.Categories.Add(category);
            db.PlacardTypes.Add(research);
            db.PlacardTypes.Add(education);

            db.SaveChanges();
            base.Seed(db);
        }
    }
}