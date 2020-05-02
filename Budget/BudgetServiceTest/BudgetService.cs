using System;

namespace BudgetServiceTest
{
    public class BudgetService
    {
        public IBudgetRepo StubBudgetRepo { get; }

        public BudgetService(IBudgetRepo stubBudgetRepo)
        {
            this.StubBudgetRepo = stubBudgetRepo;
        }

        public decimal  Query(DateTime start, DateTime end)
        {
            if (end < start)
                return 0m;

            return 0;
        }
    }
}
