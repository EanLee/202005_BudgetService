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

            for (var beginDate = start; beginDate.AddDays(-beginDate.Day + 1).Date <= end.AddDays(-end.Day + 1).Date; beginDate = beginDate.AddMonths(1))
            {
                var daysInMonth = DateTime.DaysInMonth(beginDate.Year, beginDate.Month);

                var startDay = 1;
                if (beginDate.Month == start.Month)
                {
                    startDay = start.Day;
                }

                var endDay = daysInMonth;
                if (beginDate.Month == end.Month)
                {
                    endDay = end.Day;
                }

                var queryDays = (endDay - startDay) + 1;

                var budget = this._budgetRepo.GetAll()
                                 .Where(x => x.Date == beginDate.ToString("yyyyMM"))
                                 .Select(x => x.Budget).FirstOrDefault();

                var money = budget / daysInMonth * queryDays;

                totalBudget += money;
            }

            return totalBudget;
        }
    }
}
