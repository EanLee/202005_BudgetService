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
            if (start == end)
            {
                var days = DateTime.DaysInMonth(start.Year, start.Month);
                var budget = StubBudgetRepo.GetAll()
                    .Where(x => x.Date == start.ToString("yyyyMM"))
                    .Select(x => x.Budget).FirstOrDefault();                
                return budget / days;
            }

            return 0;
        }
    }
}
