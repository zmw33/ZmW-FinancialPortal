using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZmW_FinancialPortal.Models
{
    public class Household
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedByUserId { get; set; }

        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<MyAccount> MyAccounts { get; set; }
        public virtual ICollection<ApplicationUser> Members { get; set; }

        public bool Deleted { get; set; }
        public virtual ApplicationUser CreatedByUser { get; set; }

        public Household()
        {
            MyAccounts = new HashSet<MyAccount>();
            Budgets = new HashSet<Budget>();
            Members = new HashSet<ApplicationUser>();
        }
    }
}