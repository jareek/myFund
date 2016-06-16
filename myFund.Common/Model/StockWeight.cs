using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using myFund.Common.Annotations;

namespace myFund.Common.Model
{
    public class StockWeight : ICalculatable<Fund>, INotifyPropertyChanged
    {
        private readonly Stock stock;
        private double value;

        public event PropertyChangedEventHandler PropertyChanged;

        public StockWeight(Stock stock)
        {
            if (stock == null)
            {
                throw new ArgumentNullException(nameof(stock), $"{nameof(stock)} cannot be null");
            }

            this.stock = stock;
            this.value = double.NaN;
        }

        public double Value
        {
            get { return this.value; }

            set
            {
                if (this.value == value)
                {
                    return;
                }

                this.value = value;
                this.OnPropertyChanged(nameof(this.Value));
            }
        }

        public void Calculate(Fund context)
        {
            if (context == null || context.Stocks == null)
            {
                return;
            }

            if (!context.Stocks.Any(s => s.Id == this.stock.Id))
            {
                throw new InvalidOperationException($"{this.stock.Type} stock is not a part of the fund.");
            }

            var totalFundMarketValue = context.Stocks.Sum(s => s.MarketValue.GetValueOrDefault());
            if (totalFundMarketValue == 0m || !this.stock.MarketValue.HasValue)
            {
                this.Value = double.NaN;
                return;
            }

            this.Value = (double) (this.stock.MarketValue.GetValueOrDefault() / totalFundMarketValue);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
