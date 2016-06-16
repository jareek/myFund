using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myFund.Common.Model;
using myFund.Service.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace myFund.Service.Tests
{
    [TestClass]
    public class StockNameResolverTest
    {
        [TestMethod]
        public void WhenExecutingGetStockNameForEquity_ThenStockNameIsReturnedBasedOnFundSet()
        {
            var equity = new Stock {Type = StockType.Equity};
            var fund = new[]
            {
                new Stock {Id = 1, Type = StockType.Equity, Name = "Equity1"},
                new Stock {Id = 2, Type = StockType.Bond, Name = "Bond1"}
            };
            var stockNameResolver = new StockNameResolver();

            var stockName = stockNameResolver.GetStockName(equity, fund);

            Assert.AreEqual("Equity2", stockName);
        }

        [TestMethod]
        public void WhenExecutingGetStockNameForBond_ThenStockNameIsReturnedBasedOnFundSet()
        {
            var bond = new Stock {Type = StockType.Bond};
            var fund = new[]
            {
                new Stock {Id = 1, Type = StockType.Equity, Name = "Equity1"},
                new Stock {Id = 2, Type = StockType.Bond, Name = "Bond1"}
            };
            var stockNameResolver = new StockNameResolver();

            var stockName = stockNameResolver.GetStockName(bond, fund);

            Assert.AreEqual("Bond2", stockName);
        }
    }
}
