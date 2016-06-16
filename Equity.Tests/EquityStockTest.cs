using Equity.Model;
using myFund.Common.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Equity.Tests
{
    [TestClass]
    public class EquityStockTest
    {
        [TestMethod]
        public void WhenTypeCalled_ThenEquityReturned()
        {
            var equity = new EquityStock
            {
                Price = 10m,
                Quantity = 20m
            };

            Assert.AreEqual(StockType.Equity, equity.Type);
        }

        [TestMethod]
        public void WhenMarketPriceCalled_ThenPriceMultipliedByQuantity()
        {
            var equity = new EquityStock
            {
                Price = 10m,
                Quantity = 20m
            };

            Assert.AreEqual(200m, equity.MarketValue);
        }

        [TestMethod]
        public void WhenToleranceCalled_Then_200000_Returned()
        {
            var equity = new EquityStock();
            
            Assert.AreEqual(200000m, equity.Tolerance);
        }

        [TestMethod]
        public void WhenTransactionCostCalled_ThenMarketValueMultipliedByFeeFactor()
        {
            var equity = new EquityStock
            {
                Price = 10m,
                Quantity = 20m
            };

            Assert.AreEqual(1m, equity.TransactionCost);
        }

        [TestMethod]
        public void WhenMarketValueBelowZeroAndTransactionCostBelowTolerance_ThenEquityHasWarning()
        {
            var equity = new EquityStock
            {
                Price = -10m,
                Quantity = 20m
            };

            Assert.IsTrue(equity.HasWarning);
        }

        [TestMethod]
        public void WhenMarketValueAboveZeroAndTransactionCostBelowTolerance_ThenEquityHasNoWarning()
        {
            var equity = new EquityStock
            {
                Price = 10m,
                Quantity = 20m
            };

            Assert.IsFalse(equity.HasWarning);
        }

        [TestMethod]
        public void WhenMarketValueAboveZeroAndTransactionCostAboveTolerance_ThenEquityHasWarning()
        {
            var equity = new EquityStock
            {
                Price = 300000m,
                Quantity = 200m
            };

            Assert.IsTrue(equity.HasWarning);
        }

        [TestMethod]
        public void WhenMarketValueBelowZeroAndTransactionCostAboveTolerance_ThenEquityHasWarning()
        {
            var equity = new EquityStock
            {
                Price = -300000m,
                Quantity = 200m
            };

            Assert.IsTrue(equity.HasWarning);
        }
    }
}
