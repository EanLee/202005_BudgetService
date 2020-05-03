using System;
using System.Linq;

namespace BudgetServiceTest
{
    public class BudgetService
    {
        private IBudgetRepo _budgetRepo;

        public BudgetService(IBudgetRepo budgetRepo)
        {
            this._budgetRepo = budgetRepo;
        }

        public decimal Query(DateTime start, DateTime end)
        {
            if (end < start)
                return 0m;

            decimal totalBudget = 0m;

            var curDate = new DateTime(start.Year, start.Month, 1);
            var budgets = this._budgetRepo.GetAll();

            while (curDate.Date <= end.Date)
            {
                var daysInMonth = DateTime.DaysInMonth(curDate.Year, curDate.Month);

                var startDate = curDate.Date < start.Date ? start : curDate;
                var endDate = new DateTime(curDate.Year, curDate.Month, daysInMonth);

                if (curDate.ToString("yyyyMM") == end.ToString("yyyyMM"))
                {
                    endDate = end;
                }

                var budget = budgets.Where(x => x.YearMonth == curDate.ToString("yyyyMM"))
                                    .Select(x => x.Amount).FirstOrDefault();
                var queryDays = (endDate - startDate).Days + 1;

                var money = budget / daysInMonth * queryDays;

                totalBudget += money;
                curDate = curDate.AddMonths(1);
            }

            return totalBudget;
        }
    }
}
