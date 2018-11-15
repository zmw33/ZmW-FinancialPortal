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
        //public string Type { get; set; }
        //public string Deposit { get; set; }
        //public string Withdrawal { get; set; }
        //public string AdjustUp { get; set; }
        //public string AdjustDown { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public TransactionType()
        {
            this.Transactions = new HashSet<Transaction>();
        }

    }
}