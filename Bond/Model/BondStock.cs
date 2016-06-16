using myFund.Common.Model;

namespace Bond.Model
{
    public class BondStock : Stock
    {
        private const decimal FeePercent = 0.02m;

        public BondStock()
        {
            this.Type = StockType.Bond;
        }

        public override decimal? TransactionCost => this.MarketValue * FeePercent;

        public override decimal Tolerance => 100000m;
    }
}
