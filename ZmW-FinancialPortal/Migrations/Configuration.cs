namespace ZmW_FinancialPortal.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ZmW_FinancialPortal.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ZmW_FinancialPortal.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ZmW_FinancialPortal.Models.ApplicationDbContext context)
        {
            //create a few roles (Admin, Submitter)
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "HoH"))
            {
                roleManager.Create(new IdentityRole { Name = "HoH" });
            }

            if (!context.Roles.Any(m => m.Name == "Member"))
            {
                roleManager.Create(new IdentityRole { Name = "Member" });
            }

            if (!context.Roles.Any(m => m.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            //create a few users (i.e. me and instructor)
            var userManager = new UserManager<ApplicationUser>(
                    new UserStore<ApplicationUser>(context));

            //assign users to roles (me and instructor)
            // "u => u.email" means that u "goes to u.email"
            //use example email
            if (!context.Users.Any(u => u.Email == "zacharywilsonm@gmail.com"))
            {
                //any user will be identified by ApplicationUser
                userManager.Create(new ApplicationUser
                {
                    UserName = "zacharywilsonm@gmail.com",
                    Email = "zacharywilsonm@gmail.com",
                    FirstName = "Zachary",
                    LastName = "Wilson",
                    DisplayName = "ZwAdmin",
                    AvatarPath = ""
                }, "Pigbull2017!");
            }

            if (!context.Users.Any(u => u.Email == "demomember1@mailinator.com"))
            {
                //any user will be identified by ApplicationUser
                userManager.Create(new ApplicationUser
                {
                    UserName = "demomember1@mailinator.com",
                    Email = "demomember1@mailinator.com",
                    FirstName = "Mim",
                    LastName = "Burr",
                    DisplayName = "MemI",
                    AvatarPath = ""
                }, "password1");
            }

            if (!context.Users.Any(u => u.Email == "demomember2@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "demomember2@mailinator.com",
                    Email = "demomember2@mailinator.com",
                    FirstName = "Mim",
                    LastName = "Burr II",
                    DisplayName = "MemII",
                    AvatarPath = ""
                }, "password1");
            }

            if (!context.Users.Any(u => u.Email == "demohoh@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "demohoh@mailinator.com",
                    Email = "demohoh@mailinator.com",
                    FirstName = "Hedov",
                    LastName = "Houzil",
                    DisplayName = "HoH",
                    AvatarPath = ""
                }, "password1");
            }

            //assign role to user
            var adminId = userManager.FindByEmail("zacharywilsonm@gmail.com").Id;
            userManager.AddToRole(adminId, "Admin");

            var pmId = userManager.FindByEmail("demohoh@mailinator.com").Id;
            userManager.AddToRole(pmId, "HoH");

            var devId = userManager.FindByEmail("demomember1@mailinator.com").Id;
            userManager.AddToRole(devId, "Member");

            var subId = userManager.FindByEmail("demomember2@mailinator.com").Id;
            userManager.AddToRole(subId, "Member");

            context.TransactionTypes.AddOrUpdate(TransactionType => TransactionType.Name,
               new TransactionType { Name = "Withdrawal" },
               new TransactionType { Name = "Deposit" },
               new TransactionType { Name = "AdjustUp" },
               new TransactionType { Name = "AdjustDown" }
               );

            //context.BudgetItems.AddOrUpdate(BudgetItem => BudgetItem.Name,
            //    new BudgetItem { Name = "Food" },
            //    new BudgetItem { Name = "Transportation" },
            //    new BudgetItem { Name = "Utilities" },
            //    new BudgetItem { Name = "Phone/Internet" },
            //    new BudgetItem { Name = "Miscellaneous" },
            //    new BudgetItem { Name = "School" },
            //    new BudgetItem { Name = "Pets" }
            //    );
        }

    }
}
