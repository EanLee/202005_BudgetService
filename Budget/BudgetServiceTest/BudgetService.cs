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

            var days = DateTime.DaysInMonth(start.Year, start.Month);
            var budget = StubBudgetRepo.GetAll()
                .Where(x => x.Date == start.ToString("yyyyMM"))
                .Select(x => x.Budget).FirstOrDefault();

            var queryDays = (end.Day - start.Day) + 1;

            return (budget / days) * queryDays;
        }
    }
}
