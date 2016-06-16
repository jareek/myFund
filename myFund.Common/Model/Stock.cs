namespace myFund.Common.Model
{
    public class Stock
    {
        public Stock()
        {
            this.StockWeight = new StockWeight(this);
        }

        public int? Id { get; set; }

        public string Name { get; set; }

        public decimal? Price { get; set; }

        public decimal? Quantity { get; set; }

        public StockType Type { get; set; }

        public decimal? MarketValue => this.Price * this.Quantity;

        public virtual decimal? TransactionCost { get; set; }

        public StockWeight StockWeight { get; private set; }

        public virtual decimal Tolerance { get; set; }

        public bool HasWarning
        {
            get
            {
                var hasWarning = this.MarketValue.GetValueOrDefault() < 0 || this.TransactionCost > this.Tolerance;
                return hasWarning;
            }
        }
    }
}
