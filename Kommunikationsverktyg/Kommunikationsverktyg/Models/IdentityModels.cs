using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Kommunikationsverktyg.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual string Firstname { get; set; }
        public virtual string Lastname { get; set; }
        public virtual string Description { get; set; }
        public virtual string Image { get; set; } = @"Images\avatar.jpg";
        public virtual string Phone { get; set; }
        public string Title { get; set; }
        public ICollection<FormalBlogModel> Posts { get; set; }

        //Konstruktor
        public ApplicationUser() : base()
        {
            Posts = new List<FormalBlogModel>();
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<EventModel> EventModels { get; set; }
        public virtual DbSet<FormalBlogModel> FormalBlogPosts { get; set; }
        public virtual DbSet<BadWords> BadWords { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}