//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using ZmW_FinancialPortal.Models;

//namespace ZmW_FinancialPortal.Helpers
//{
//    public class MyAccountHelp
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();

//        public void AdjustBalance(int transactionId)
//        {
//            var transaction = db.Transactions.Find(transactionId);
//            var transactionType = transaction.Type;
//            var bankId = transaction.MyAccountId;
//            var bankAccount = db.MyAccount.Find(bankId);
//            db.MyAccounts.Attach(bankAccount);

//            switch(transactionType)
//            {
//                case TransactionType.Deposit:
//                    bankAccount.CurrentBalance -= transaction.Amount;
//                    break;
//                case TransactionType.Withdrawal:
//                    bankAccount.CurrentBalance += transaction.Amount;
//                    break;
//                case TransactionType.AdjustUp:
//                    //bankAccount.CurrentBalance -= transaction.Amount;
//                    break;
//                case TransactionType.AdjustDown:
//                    //bankAccount.CurrentBalance -= transaction.Amount;
//                    break;
//            }
//            db.SaveChanges();
//        }
//    }
//}