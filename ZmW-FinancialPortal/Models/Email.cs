using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZmW_FinancialPortal.Models
{
    public class Email
    {
        public class EmailModel
        {
            [Required, Display(Name = "Name")]
            public string FromName { get; set; }

            [Required, Display(Name = "Email"), EmailAddress]
            public string FromEmail { get; set; }

            [Required]
            public string Subject { get; set; }

            [Required]
            [AllowHtml]
            public string Body { get; set; }
        }

        public class InviteEmailModel
        {
            [Required, Display(Name = "Name")]
            public string FromName { get; set; }

            [Required, Display(Name = "Email"), EmailAddress]
            public string FromEmail { get; set; }

            [EmailAddress]
            public string ToEmail { get; set; }

            [Required]
            public string Subject { get; set; }

            [Required]
            [AllowHtml]
            public string Body { get; set; }
        }
    }
}