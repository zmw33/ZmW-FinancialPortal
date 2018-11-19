using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ZmW_FinancialPortal.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using ZmW_FinancialPortal.ViewModels;
using System;
using System.Web;

namespace ZmW_FinancialPortal.Controllers
{
    public class MorrisBarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult GetBudgetDataForBarChart()
        {
            var budgetData = new List<MorrisBudgetBar>();

            //Get UserId...
            var userId = User.Identity.GetUserId();

            //Get HouseholdId
            var houseId = db.Users.Find(userId).HouseholdId;
            if (houseId == null)
                return Content(JsonConvert.SerializeObject(budgetData), "application/json");

            foreach (var budget in db.Households.Find(houseId).Budgets.ToList())
            {
                budgetData.Add(new MorrisBudgetBar
                {
                    Label = budget.Name,
                    Target = budget.SpendingTarget,
                    Actual = budget.CurrentBalance
                });
            }

            return Content(JsonConvert.SerializeObject(budgetData), "application/json");
        }

        [Authorize]
        public ActionResult GetBudgetItemDataForBarChart()
        {
            var budgetItemData = new List<MorrisBudgetItemBar>();

            //Get UserId...
            var userId = User.Identity.GetUserId();

            //Get HouseholdId
            var houseId = db.Users.Find(userId).HouseholdId;
            if (houseId == null)
                return Content(JsonConvert.SerializeObject(budgetItemData), "application/json");

            foreach (var budget in db.Households.Find(houseId).Budgets.ToList())
            {
                foreach (var budgetItem in budget.BudgetItems.ToList())
                {
                    budgetItemData.Add(new MorrisBudgetItemBar
                    {
                        Label = budgetItem.Name,
                        Actual = budgetItem.CurrentBalance
                    });
                }
            }

            return Content(JsonConvert.SerializeObject(budgetItemData), "application/json");
        }

        [Authorize]
        public ActionResult GetPositiveBudgetItemDataForBarChart()
        {
            var budgetItemData = new List<MorrisBudgetItemBar>();

            //Get UserId
            var userId = User.Identity.GetUserId();

            //Get HouseholdId
            var houseId = db.Users.Find(userId).HouseholdId;
            if (houseId == null)
                return Content(JsonConvert.SerializeObject(budgetItemData), "application/json");

            foreach (var budget in db.Households.Find(houseId).Budgets.ToList())
            {
                foreach (var budgetItem in budget.BudgetItems.ToList())
                {
                    if (budgetItem.CurrentBalance > 0)
                    {
                        budgetItemData.Add(new MorrisBudgetItemBar
                        {
                            Label = budgetItem.Name,
                            Actual = budgetItem.CurrentBalance
                        });
                    }
                }
            }

            return Content(JsonConvert.SerializeObject(budgetItemData), "application/json");
        }
    }
}

