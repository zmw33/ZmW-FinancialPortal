using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZmW_FinancialPortal.Models;

namespace ZmW_FinancialPortal.Helpers
{
    public class MembersHelp
    {
        private UserManager<ApplicationUser>userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        private ApplicationDbContext db = new ApplicationDbContext();

        public bool IsUserInRole(string userId, string roleName)
        {
            return userManager.IsInRole(userId, roleName);
        }

        public ICollection<string> ListUserRoles(string userId)
        {
            return userManager.GetRoles(userId).ToList();
        }

        public bool AddUserToRole(string userId, string roleName)
        {
            var result = userManager.AddToRole(userId, roleName);
            return result.Succeeded;
        }

        public bool RemoveUserFromRole(string userId, string roleName)
        {
            var result = userManager.RemoveFromRole(userId, roleName);
            return result.Succeeded;
        }

        public ICollection<ApplicationUser> UsersInRole(string roleName)
        {
            var resultList = new List<ApplicationUser>();
            var list = userManager.Users.ToList();
            foreach (var user in list)
            {
                if (IsUserInRole(user.Id, roleName)) resultList.Add(user);
            }

            return resultList;
        }

        public ICollection<ApplicationUser> UsersNotInRole(string roleName)
        {
            var resultList = new List<ApplicationUser>();
            var List = userManager.Users.ToList();
            foreach (var user in List)
            {
                if (!IsUserInRole(user.Id, roleName))
                    resultList.Add(user);
            }

            return resultList;
        }

        public string AvatarPath(string userId)
        {
            var avatarPath = db.Users.Find(userId).AvatarPath;

            if (avatarPath == null)
            {
                db.Users.Find(userId).AvatarPath = "~/Img/avatar-01.jpg";
            }
            else
            {
                db.Users.Find(userId).AvatarPath = avatarPath;
            }

            return db.Users.Find(userId).AvatarPath;
        }
    }
}