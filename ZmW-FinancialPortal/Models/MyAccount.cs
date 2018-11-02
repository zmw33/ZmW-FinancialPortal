using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZmW_FinancialPortal.Models
{
    public class MyAccount
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public decimal ReconciledBalance { get; set; }

        public virtual Household Household { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

        public MyAccount()
        {
            this.Transactions = new HashSet<Transaction>();
        }
    }
}