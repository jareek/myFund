using myFund.Common.Model;

namespace Equity.Model
{
    internal class EquityStock : Stock
    {
        public EquityStock()
        {
            this.Type = StockType.Equity;
        }

        public override decimal? TransactionCost => this.MarketValue * 0.005m;

        public override decimal Tolerance => 200000m;
    }
}
