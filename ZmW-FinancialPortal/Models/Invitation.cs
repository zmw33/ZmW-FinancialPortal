using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZmW_FinancialPortal.Models
{
    public class Invitation
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
        public int HouseholdId { get; set; }
        public Guid Code { get; set; }
        public bool Accepted { get; set; }
        public string Email { get; set; }
    }
}