//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Util;
//using ZmW_FinancialPortal.Models;

//namespace ZmW_FinancialPortal.Helpers
//{
//    public class TransactionHelp
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();

//        public ICollection<Transactions> ListHouseholdTransactions(int userId)
//        {
//            return db.Users.Find(userId).Household.MyAccounts.SelectMany(t => t.Transactions).ToList();
//        }


//    }
//}