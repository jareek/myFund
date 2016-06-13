﻿using System.Windows.Input;
using Equity.Model;
using myFund.Common.Model;
using myFund.Common.UI.ViewModels;
using myFund.Service.Service;
using MvvmValidation;
using Prism.Commands;
using Prism.Events;

namespace Equity.ViewModels
{
    public class EquityViewModel : ValidatableViewModel
    {
        private readonly IFundService fundService;
        private readonly IEventAggregator eventAggregator;
        private Stock stock;

        public EquityViewModel(IFundService fundService, IEventAggregator eventAggregator)
        {
            this.stock = new EquityStock();
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

            var addedStock = await this.fundService.AddStockAsync(this.stock);
            this.stock.Price = null;
            this.OnPropertyChanged(nameof(this.Price));
            this.stock.Quantity = null;
            this.OnPropertyChanged(nameof(this.Quantity));
            this.eventAggregator.GetEvent<PubSubEvent<Stock>>().Publish(addedStock);
        }

        private void ConfigureValidationRules()
        {
            this.Validator.AddRequiredRule(() => this.Price, "Price is required");
            this.Validator.AddRule(() => this.Price, () => RuleResult.Assert(this.Price <= int.MaxValue, "Price is too high"));
            this.Validator.AddRule(() => this.Price, () => RuleResult.Assert(this.Price >= int.MinValue, "Price is too low"));
            this.Validator.AddRequiredRule(() => this.Quantity, "Quantity is required");
            this.Validator.AddRule(() => this.Quantity, () => RuleResult.Assert(this.Price <= int.MaxValue, "Quantity is too high"));
            this.Validator.AddRule(() => this.Quantity, () => RuleResult.Assert(this.Price >= int.MinValue, "Quantity is too low"));
        }
    }
}