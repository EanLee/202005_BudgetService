using System.Collections.Generic;

namespace BudgetServiceTest
{
    public interface IBudgetRepo
    {
        public List<BudgetEntity> GetAll();
    }
}
