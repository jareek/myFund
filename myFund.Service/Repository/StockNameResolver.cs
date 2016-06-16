using System;
using System.Collections.Generic;
using System.Linq;
using myFund.Common.Model;

namespace myFund.Service.Repository
{
    public class StockNameResolver : IStockNameResolver
    {
        public string GetStockName(Stock stock, IEnumerable<Stock> fund)
        {
            if (stock == null)
            {
                throw new InvalidOperationException("Stock name could not be resolved for null stock object.");
            }

            if (stock.Type == StockType.None)
            {
                throw new InvalidOperationException("Stock name could not be resolved for unknown stock.");
            }

            if (fund == null)
            {
                throw new InvalidOperationException("Stock name could not be resolved for null fund object.");
            }
            
            var stocksCount = fund.Count(s => s.Type == stock.Type);
            var stockName = $"{stock.Type}{stocksCount + 1}";

            return stockName;
        }
    }
}
