using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZmW_FinancialPortal.Models;

namespace ZmW_FinancialPortal.Helpers
{
    public class HouseholdHelp
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public void AddUserToHousehold(string userId, int HouseholdId)
        {
            //Household household = db.Households.Find(HouseholdId);
            //var newUser = db.Users.Find(userId);

            //household.Members.Add(newUser);
            //db.SaveChanges();

            var user = db.Users.Find(userId);
            db.Users.Attach(user);
            user.HouseholdId = HouseholdId;
            db.SaveChanges();
        }

        public ICollection<ApplicationUser> UsersInHousehold(int householdId)
        {
            return db.Households.Find(householdId).Members;
        }

        //public ICollection<HouseholdHelp> IsUserInHousehold(string userId)
        //{
        //    if(string.IsNullOrEmpty)
        //    {

        //    }
        //}

        public static void RemoveUserFromHouse(string userId)
        {
            var user = db.Users.Find(userId);
            user.HouseholdId = null;
            db.SaveChanges();
        }
    }
}