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

            for (var curDate = start;
                curDate.AddDays(-curDate.Day + 1).Date <= end.AddDays(-end.Day + 1).Date;
                curDate = curDate.AddMonths(1))
            {
                var daysInMonth = DateTime.DaysInMonth(curDate.Year, curDate.Month);

                var startDay = curDate.Month == start.Month && curDate.Year == start.Year ? start.Day : 1;
                var endDay = curDate.Month == end.Month && curDate.Year == end.Year ? end.Day : daysInMonth;

                var queryDays = (endDay - startDay) + 1;

                var budget = this._budgetRepo.GetAll()
                                 .Where(x => x.YearMonth == curDate.ToString("yyyyMM"))
                                 .Select(x => x.Amount).FirstOrDefault();

                var money = budget / daysInMonth * queryDays;

                totalBudget += money;
            }

            return totalBudget;
        }
    }
}
