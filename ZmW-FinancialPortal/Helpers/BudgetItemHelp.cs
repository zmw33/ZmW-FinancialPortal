//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using ZmW_FinancialPortal.Models;

//namespace ZmW_FinancialPortal.Helpers
//{
//    public class BudgetItemHelp
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();

//        public void AdjustBalance(int transactionId)
//        {
//            var transaction = db.Transactions.Find(transactionId);
//            var transactionType = transaction.Type;
//            var budgetItemId = transaction.BudgetItemId;
//            var budgetItem = db.BudgetItems.Find(transaction.BudgetItemId);
//            var budget = db.Budgets.Find(budgetItem.BudgetId);
//            db.BudgetItems.Attach(budget);

//            switch (transactionType)
//            {
//                case TransactionType.Deposit:
//                    budgetItem.CurrentBalance -= transaction.Amount;
//                    break;
//                case TransactionType.Withdrawal:
//                    budgetItem.CurrentBalance += transaction.Amount;
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