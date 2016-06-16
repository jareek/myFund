using System;
using myFund.Common.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace myFund.Common.Tests
{
    [TestClass]
    public class StockWeightTest
    {
        [TestMethod]
        public void WhenExecutingCalculate_ThenStockWeightValueIsCalculatedAndUpdated()
        {
            var equity = new Stock { Id = 1, Type = StockType.Equity, Price = 100m, Quantity = 100m};
            var bond = new Stock { Id = 2, Type = StockType.Bond, Price = 300m, Quantity = 100m };
            var fund = new Fund { Stocks = new[] {equity, bond} };

            equity.StockWeight.Calculate(fund);
            bond.StockWeight.Calculate(fund);

            Assert.AreEqual(0.25, equity.StockWeight?.Value);
            Assert.AreEqual(0.75, bond.StockWeight?.Value);
        }
    }
}
