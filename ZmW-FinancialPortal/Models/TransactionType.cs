using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZmW_FinancialPortal.Models
{
    public class TransactionType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public TransactionType()
        {
            this.Transactions = new HashSet<Transaction>();
        }

    }
}