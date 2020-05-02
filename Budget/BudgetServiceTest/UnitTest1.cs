using System;
using System.Diagnostics;
using NSubstitute;
using NUnit.Framework;
using System.Linq;


namespace BudgetServiceTest
{
    public class Tests
    {
        private BudgetService _budgetService;

        public Tests()
        {
            //List<BudgetEntity>
            IBudgetRepo _stubBudgetRepo = NSubstitute.Substitute.For<IBudgetRepo>();
            _stubBudgetRepo.GetAll().Returns(new System.Collections.Generic.List<BudgetEntity>
            {
                new BudgetEntity
                {
                     Date = "202004",
                      Budget = 300,
                }
            });
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
            var budget = _budgetService.Query(new DateTime(2020, 4, 1), new DateTime(2020, 4, 1));

            Assert.AreEqual(10, budget);

        }

        [Test]
        public void 同一月多天()
        {
            var budget = _budgetService.Query(new DateTime(2020, 4, 1), new DateTime(2020, 4, 5));

            Assert.AreEqual(50, budget);

        }


    }
}
