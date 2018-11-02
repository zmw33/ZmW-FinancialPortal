using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZmW_FinancialPortal.ViewModels
{
    public class InvitationViewModel
    {
        [Required, Display(Name = "Email Address"), EmailAddress]
        public string Email { get; set; }
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
        public int HouseholdId { get; set; }
        public Guid Code { get; set; }
        public bool Accepted { get; set; }
    }
}