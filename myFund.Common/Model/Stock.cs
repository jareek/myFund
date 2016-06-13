namespace myFund.Common.Model
{
    public class Stock
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public decimal? Price { get; set; }

        public decimal? Quantity { get; set; }

        public StockType Type { get; set; }

        public decimal? MarketValue => this.Price * this.Quantity;

        public virtual decimal? TransactionCost { get; set; }

        public decimal? StockWeight { get; set; }

        public virtual decimal Tolerance { get; set; }
    }
}
