using myFund.Common.Model;

namespace Equity.Model
{
    public class EquityStock : Stock
    {
        private const decimal FeePercent = 0.005m;

        public EquityStock()
        {
            this.Type = StockType.Equity;
        }

        public override decimal? TransactionCost => this.MarketValue * FeePercent;

        public override decimal Tolerance => 200000m;
    }
}
