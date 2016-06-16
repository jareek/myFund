using System.Linq;
using myFund.Common.Model;

namespace Fund.Model
{
    public class FundSummary : ICalculatable<myFund.Common.Model.Fund>
    {
        public int TotalEquitiesCount { get; private set; }

        public float TotalEquitiesStockWeight { get; private set; }

        public decimal TotalEquitiesMarketValue { get; private set; }

        public int TotalBondCount { get; private set; }

        public float TotalBondStockWeight { get; private set; }

        public decimal TotalBondMarketValue { get; private set; }

        public int TotalStockCount { get; private set; }

        public float TotalStockWeight { get; private set; }

        public decimal TotalMarketValue { get; private set; }

        public void Calculate(myFund.Common.Model.Fund context)
        {
            if (context == null || context.Stocks == null)
            {
                return;
            }

            var equities = context.Stocks.Where(stock => stock.Type == StockType.Equity);
            this.TotalEquitiesCount = equities.Count();
            this.TotalEquitiesStockWeight = (float)equities.Sum(equity => equity.StockWeight.Value);
            this.TotalEquitiesMarketValue = equities.Sum(equity => equity.MarketValue.GetValueOrDefault());

            var bonds = context.Stocks.Where(stock => stock.Type == StockType.Bond);
            this.TotalBondCount = bonds.Count();
            this.TotalBondStockWeight = (float)bonds.Sum(bond => bond.StockWeight.Value);
            this.TotalBondMarketValue = bonds.Sum(bond => bond.MarketValue.GetValueOrDefault());

            this.TotalStockCount = context.Stocks.Count();
            this.TotalStockWeight = (float)context.Stocks.Sum(stock => stock.StockWeight.Value);
            this.TotalMarketValue = context.Stocks.Sum(stock => stock.MarketValue.GetValueOrDefault());
        }
    }
}
