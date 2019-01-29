using Kommunikationsverktyg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Kommunikationsverktyg.Repository
{
    public class RoleRepository
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        public List<ApplicationUser> GetUsersByRole(string role)
        {
            var users = Roles.GetUsersInRole(role).ToList();

            var list = new List<ApplicationUser>();
            foreach(var item in users)
            {
                var user = _db.Users.Single(i => i.UserName == item);
                list.Add(user);
            }
            return list;
        }

        public void RemoveUserFromRole(string username, string role)
        {
            Roles.RemoveUserFromRole(username, role);
        }

        public void AddUserToRole(string username, string role)
        {
            Roles.AddUserToRole(username, role);
        }

        private bool IsUserInRole(string username, string role)
        {
            return Roles.IsUserInRole(username, role);
        }
    }
}