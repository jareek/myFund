using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using myFund.Common.Model;

namespace myFund.Service.Repository
{
    public class StockNameResolver : IStockNameResolver
    {
        public string GetStockName(Stock stock, IEnumerable<Stock> fund)
        {
            ////Contract.Requires<InvalidOperationException>(stock != null, "Stock name could not be resolved for null stock object.");
            ////Contract.Requires<InvalidOperationException>(stock.Type != StockType.None, "Stock name could not be resolved for unknown stock.");
            ////Contract.Requires<InvalidOperationException>(fund != null, "Stock name could not be resolved for null fund object.");
            
            var stocksCount = fund.Count(s => s.Type == stock.Type);
            var stockName = $"{stock.Type}{stocksCount + 1}";

            return stockName;
        }
    }
}
