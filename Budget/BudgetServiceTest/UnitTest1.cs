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
            IBudgetRepo _stubBudgetRepo = NSubstitute.Substitute.For<IBudgetRepo>();
            _stubBudgetRepo.GetAll().Returns(new System.Collections.Generic.List<Budget>
            {
                new Budget
                {
                    YearMonth = "202004",
                    Amount = 300,
                },
                new Budget
                {
                    YearMonth = "202005",
                    Amount = 31,
                },
                new Budget
                {
                    YearMonth = "202006",
                    Amount = 3000,
                },
                new Budget
                {
                    YearMonth = "202012",
                    Amount = 3100,
                },
                new Budget
                {
                    YearMonth = "202101",
                    Amount = 310,
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

        [Test]
        public void 零預算()
        {
            var budget = _budgetService.Query(new DateTime(2020, 1, 1), new DateTime(2020, 1, 5));

            Assert.AreEqual(0, budget);
        }

        [Test]
        public void 跨月()
        {
            var budget = _budgetService.Query(new DateTime(2020, 4, 26), new DateTime(2020, 5, 5));

            Assert.AreEqual(55m, budget);
        }

        [Test]
        public void 跨年_月()
        {
            var budget = _budgetService.Query(new DateTime(2020, 12, 31), new DateTime(2021, 1, 1));

            Assert.AreEqual(110m, budget);
        }

        [Test]
        public void 跨年_出現同月份()
        {
            var budget = _budgetService.Query(new DateTime(2020, 1, 31), new DateTime(2021, 4, 1));

            Assert.AreEqual(6741m, budget);
        }
    }
}
