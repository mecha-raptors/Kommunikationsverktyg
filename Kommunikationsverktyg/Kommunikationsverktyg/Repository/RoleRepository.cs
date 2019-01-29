using Kommunikationsverktyg.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Kommunikationsverktyg.Repository
{
    public class RoleRepository
    {
        private static ApplicationDbContext _db = new ApplicationDbContext();
        private static UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(_db);
        private static UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);

        public List<ApplicationUser> GetUsersByRole(string role)
        {
            var Role = _db.Roles.Single(i => i.Name == role);
            var list = new List<ApplicationUser>();

            foreach(var user in _db.Users.ToList())
            {
                if (user.Roles.Any(i => i.RoleId == Role.Id))
                {
                    list.Add(user);
                }

            }
            return list;
            
        }

        public void RemoveUserFromRole(string userId, string role)
        {
            userManager.RemoveFromRole(userId, role);            
            _db.SaveChanges();
        }

        public void AddUserToRole(string userId, string role)
        {
            userManager.AddToRole(userId, role);
            _db.SaveChanges();
        }

        private bool IsUserInRole(string username, string role)
        {
            return Roles.IsUserInRole(username, role);
        }
    }
}