using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZmW_FinancialPortal.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int MyAccountId { get; set; }
        public string Type { get; set; }
        public int TransactionTypeId { get; set; }
        public int BudgetItemId { get; set; }
        public string EnteredById { get; set; }
       
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public bool Reconciled { get; set; }
        public decimal ReconciledAmount { get; set; }

        public virtual MyAccount MyAccount { get; set; }
        public virtual TransactionType TransactionType { get; set; }
        public virtual ApplicationUser EnteredBy { get; set; }
       
    }
}