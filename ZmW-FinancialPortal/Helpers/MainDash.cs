using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZmW_FinancialPortal.Models;

namespace ZmW_FinancialPortal.Helpers
{
    public class MainDash
    {
        public static int GetHouseCount()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return db.Households.Count();
        }

        public static int GetAccountCount()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return db.MyAccounts.Count();
        }

        public static int GetUserCount()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return db.Users.Count();
        }
    }
}