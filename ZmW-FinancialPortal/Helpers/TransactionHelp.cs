using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Util;
using ZmW_FinancialPortal.Models;

namespace ZmW_FinancialPortal.Helpers
{
    public class TransactionHelp
    {
        public static ApplicationDbContext db = new ApplicationDbContext();

        public static void VoidTransaction(int transId)
        {
            var trans = db.Transactions.Find(transId);
            var transType = db.TransactionTypes.Find(trans.TransactionTypeId);
            var accId = trans.MyAccountId;

            var acc = db.MyAccounts.Find(accId);
            db.MyAccounts.Attach(acc);

            acc.Balance += trans.Amount;
            trans.Void = true;

            db.SaveChanges();
        }

        public static void AdjustAccount(int transId)
        {
            var trans = db.Transactions.Find(transId);
            var transactionType = db.TransactionTypes.Find(trans.TransactionTypeId);
            var accId = trans.MyAccountId;
        
            var acc = db.MyAccounts.Find(accId);
            db.MyAccounts.Attach(acc);

            if (transactionType.Name == "Withdrawal")
            {
                acc.Balance -= trans.Amount;
            }
            else if (transactionType.Name == "Deposit")
            {
                acc.Balance += trans.Amount;
            }
            else if (transactionType.Name == "Adjust. Up")
            {
                acc.Balance += trans.Amount;
            }
            else if (transactionType.Name == "Adjust. Down")
            {
                acc.Balance -= trans.Amount;
            }
            db.SaveChanges();
        }

        public static void AdjustBudget(int transId)
        {
            var trans = db.Transactions.Find(transId);
            var transactionType = db.TransactionTypes.Find(trans.TransactionTypeId);
            var budId = trans.BudgetItemId;

            var bud = db.BudgetItems.Find(budId);
            db.BudgetItems.Attach(bud);

            if ( transactionType.Name == "Withdrawal")
            {
                bud.Budget.CurrentBalance += trans.Amount;
            }
            db.SaveChanges();
        }

        public static void AdjustBudgetItem(int transId)
        {
            var trans = db.Transactions.Find(transId);
            var transType = trans.TransactionType;

            var budIt = db.BudgetItems.Find(trans.BudgetItemId);
            db.BudgetItems.Attach(budIt);

            if (transType.Name == "Withdrawal")
            {
                budIt.CurrentBalance += trans.Amount;
            }
            db.SaveChanges();
        }
    }



}
