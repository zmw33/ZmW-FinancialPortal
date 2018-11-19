using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZmW_FinancialPortal.Models
{
    public class BudgetItem
    {
        public int? Id { get; set; }
        public int BudgetId { get; set; }
        public string Name { get; set; }
        public decimal CurrentBalance { get; set; }
        public bool Deleted { get; set; }

        public virtual Budget Budget { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public BudgetItem()
        {
            Transactions = new HashSet<Transaction>();
        }
    }
}