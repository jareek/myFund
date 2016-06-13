using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using myFund.Common.Model;

namespace myFund.Service.Repository
{
    public class FundRepository: IFundRepository
    {
        private readonly List<Stock> fund;
        private readonly IStockNameResolver stockNameResolver;

        public FundRepository(IStockNameResolver stockNameResolver)
        {
            this.fund = new List<Stock>();
            this.stockNameResolver = stockNameResolver;
        }
        public Stock AddStock(Stock stock)
        {
            ////Contract.Requires<InvalidOperationException>(stock != null, "Empty stock could not be added.");
            ////Contract.Requires<InvalidOperationException>(stock.Type != StockType.None, "Stock has no type.");

            var stockName = this.stockNameResolver.GetStockName(stock, this.fund);
            var stockId = this.fund.Count + 1;

            var newStock = new Stock
            {
                Id = stockId,
                Name = stockName,
                Price = stock.Price,
                Quantity = stock.Quantity,
                Type = stock.Type,
                TransactionCost = stock.TransactionCost,
                Tolerance = stock.Tolerance
            };

            this.fund.Add(newStock);

            return newStock;
        }

        public Fund GetFund()
        {
            return new Fund
            {
                Stocks = this.fund.Select(stock => new Stock
                {
                    Id = stock.Id,
                    Name = stock.Name,
                    Price = stock.Price,
                    Quantity = stock.Quantity,
                    Type = stock.Type,
                    TransactionCost = stock.TransactionCost,
                    Tolerance = stock.Tolerance
                }).ToArray()
            };
        }
    }
}
