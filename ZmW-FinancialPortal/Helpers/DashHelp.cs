using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZmW_FinancialPortal.Models;

namespace ZmW_FinancialPortal.Helpers
{
    public class DashHelp
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static int GetHouseholdMemberCount()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var householdId = db.Users.Find(userId).HouseholdId;
            return db.Users.Where(u => u.HouseholdId == householdId).Count();
        }

        public static int GetHouseholdBankCount()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var householdId = db.Users.Find(userId).HouseholdId;
            return db.MyAccounts.Where(u => u.HouseholdId == householdId).Count();
        }

        public static int GetHouseholdTransactionCount()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var householdId = db.Users.Find(userId).HouseholdId;
            return db.Households.Find(householdId).MyAccounts.SelectMany(t => t.Transactions).Count();
        }

        public static int GetHouseholdBudgetCount()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var householdId = db.Users.Find(userId).HouseholdId;
            return db.Households.Find(householdId).Budgets.Count();
        }

        public static int GetHouseholdBudgetItemCount()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var householdId = db.Users.Find(userId).HouseholdId;
            return db.Households.Find(householdId).Budgets.SelectMany(b => b.BudgetItems).Count();
        }
    }
}