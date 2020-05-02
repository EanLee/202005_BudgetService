using System;
using System.Linq;

namespace BudgetServiceTest
{
    public class BudgetService
    {
        public IBudgetRepo StubBudgetRepo { get; }

        public BudgetService(IBudgetRepo stubBudgetRepo)
        {
            this.StubBudgetRepo = stubBudgetRepo;
        }

        public decimal Query(DateTime start, DateTime end)
        {
            if (end < start)
                return 0m;

            var beginMonth = start.Month;

            decimal totalBudget = 0m;

            for (var month = start.Month; month <= end.Month; month++)
            {
                var date = new DateTime(start.Year, month, 1);

                var daysInMonth = DateTime.DaysInMonth(start.Year, month);

                var startDay = 1;
                if (month == start.Month)
                {
                    startDay = start.Day;
                }

                var endDay = daysInMonth;
                if (month == end.Month)
                {
                    endDay = end.Day;
                }

                var queryDays = (endDay - startDay) + 1;

                var budget = StubBudgetRepo.GetAll()
                                           .Where(x => x.Date == date.ToString("yyyyMM"))
                                           .Select(x => x.Budget).FirstOrDefault();

                var money = budget / daysInMonth * queryDays;

                totalBudget += money;
            }

            return totalBudget;
        }
    }
}
