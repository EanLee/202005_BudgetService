using System;
using System.Diagnostics;
using NUnit.Framework;

namespace BudgetServiceTest
{
    public class Tests
    {
        private BudgetService _budgetService;

        public Tests()
        {
            IBudgetRepo _stubBudgetRepo =  NSubstitute.Substitute.For<IBudgetRepo>();
            _budgetService = new BudgetService(_stubBudgetRepo);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void 起迄日相反()
        {
            var budget = _budgetService.Query(new DateTime(2020, 4, 28), new DateTime(2020, 4, 1));

            Assert.AreEqual(0m, budget);
        }

        [Test]
        public void 單一日期()
        {
            var budget = _budgetService.Query(new DateTime(2020, 4, 28), new DateTime(2020, 4, 1));

            Assert.AreEqual(0m, budget);
 
        }
    }
}
