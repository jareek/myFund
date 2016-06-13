using myFund.Common.Model;

namespace Bond.Model
{
    internal class BondStock : Stock
    {
        public BondStock()
        {
            this.Type = StockType.Bond;
        }

        public override decimal? TransactionCost => this.MarketValue * 0.02m;

        public override decimal Tolerance => 100000m;
    }
}
