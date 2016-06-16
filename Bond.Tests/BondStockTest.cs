using Bond.Model;
using myFund.Common.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bond.Tests
{
    [TestClass]
    public class BondStockTest
    {
        [TestMethod]
        public void WhenTypeCalled_ThenBondReturned()
        {
            var bond = new BondStock
            {
                Price = 10m,
                Quantity = 20m
            };

            Assert.AreEqual(StockType.Bond, bond.Type);
        }

        [TestMethod]
        public void WhenMarketPriceCalled_ThenPriceMultipliedByQuantity()
        {
            var bond = new BondStock
            {
                Price = 10m,
                Quantity = 20m
            };

            Assert.AreEqual(200m, bond.MarketValue);
        }

        [TestMethod]
        public void WhenToleranceCalled_Then_100000_Returned()
        {
            var bond = new BondStock();
            
            Assert.AreEqual(100000m, bond.Tolerance);
        }

        [TestMethod]
        public void WhenTransactionCostCalled_ThenMarketValueMultipliedByFeeFactor()
        {
            var bond = new BondStock
            {
                Price = 10m,
                Quantity = 20m
            };

            Assert.AreEqual(4m, bond.TransactionCost);
        }

        [TestMethod]
        public void WhenMarketValueBelowZeroAndTransactionCostBelowTolerance_ThenBondHasWarning()
        {
            var bond = new BondStock
            {
                Price = -10m,
                Quantity = 20m
            };

            Assert.IsTrue(bond.HasWarning);
        }

        [TestMethod]
        public void WhenMarketValueAboveZeroAndTransactionCostBelowTolerance_ThenBondHasNoWarning()
        {
            var bond = new BondStock
            {
                Price = 10m,
                Quantity = 20m
            };

            Assert.IsFalse(bond.HasWarning);
        }

        [TestMethod]
        public void WhenMarketValueAboveZeroAndTransactionCostAboveTolerance_ThenBondHasWarning()
        {
            var bond = new BondStock
            {
                Price = 300000m,
                Quantity = 200m
            };

            Assert.IsTrue(bond.HasWarning);
        }

        [TestMethod]
        public void WhenMarketValueBelowZeroAndTransactionCostAboveTolerance_ThenBondHasWarning()
        {
            var bond = new BondStock
            {
                Price = -300000m,
                Quantity = 200m
            };

            Assert.IsTrue(bond.HasWarning);
        }
    }
}
