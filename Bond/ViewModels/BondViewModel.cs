using System;
using System.Windows.Input;
using Bond.Model;
using myFund.Common.Model;
using myFund.Common.UI.Events;
using myFund.Common.UI.ViewModels;
using myFund.Service.Service;
using MvvmValidation;
using Prism.Commands;
using Prism.Events;

namespace Bond.ViewModels
{
    public class BondViewModel : ValidatableViewModel
    {
        private readonly IFundService fundService;
        private readonly IEventAggregator eventAggregator;
        private readonly Stock stock;

        public BondViewModel(IFundService fundService, IEventAggregator eventAggregator)
        {
            this.stock = new BondStock();
            this.fundService = fundService;
            this.eventAggregator = eventAggregator;
            this.InitializeCommands();

            this.ConfigureValidationRules();
        }

        public ICommand AddStockCommand { get; private set; }

        public decimal? Price
        {
            get { return this.stock.Price; }
            set
            {
                if (this.stock.Price == value)
                {
                    return;
                }

                this.stock.Price = value;
                this.OnPropertyChanged(nameof(this.Price));
                this.Validator.Validate(() => this.Price);
                this.OnPropertyChanged(nameof(this.MarketValue));
                this.OnPropertyChanged(nameof(this.TransactionCost));
            }
        }

        public decimal? Quantity
        {
            get { return this.stock.Quantity; }
            set
            {
                if (this.stock.Quantity == value)
                {
                    return;
                }

                this.stock.Quantity = value;
                this.OnPropertyChanged(nameof(this.Quantity));
                this.Validator.Validate(() => this.Quantity);
                this.OnPropertyChanged(nameof(this.MarketValue));
                this.OnPropertyChanged(nameof(this.TransactionCost));
            }
        }

        public StockType? Type => this.stock.Type;

        public decimal? MarketValue => this.stock.MarketValue;

        public decimal? TransactionCost => this.stock.TransactionCost;

        private void InitializeCommands()
        {
            this.AddStockCommand = new DelegateCommand(this.OnAddStockCommandExecuted);
        }

        private async void OnAddStockCommandExecuted()
        {
            this.Validate();
            if (!this.IsValid.GetValueOrDefault())
            {
                return;
            }

            try
            {
                var addedStock = await this.fundService.AddStockAsync(this.stock);
                this.stock.Price = null;
                this.stock.Quantity = null;
                this.OnPropertyChanged(nameof(this.Price));
                this.OnPropertyChanged(nameof(this.Quantity));
                this.eventAggregator.GetEvent<StockAddedEvent>().Publish(addedStock);
            }
            catch (AggregateException ex)
            {
                // log exception
            }      
        }

        private void ConfigureValidationRules()
        {
            this.Validator.AddRequiredRule(() => this.Price, "Price is required");
            this.Validator.AddRule(() => this.Price, () => RuleResult.Assert(this.Price.HasValue && this.Price <= int.MaxValue, "Price is too high"));
            this.Validator.AddRule(() => this.Price, () => RuleResult.Assert(this.Price.HasValue && this.Price >= int.MinValue, "Price is too low"));
            this.Validator.AddRequiredRule(() => this.Quantity, "Quantity is required");
            this.Validator.AddRule(() => this.Quantity, () => RuleResult.Assert(this.Quantity.HasValue && this.Quantity <= int.MaxValue, "Quantity is too high"));
            this.Validator.AddRule(() => this.Quantity, () => RuleResult.Assert(this.Quantity.HasValue && this.Quantity >= int.MinValue, "Quantity is too low"));
        }
    }
}
