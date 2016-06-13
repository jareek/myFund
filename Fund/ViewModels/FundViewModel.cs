using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using myFund.Common.Model;
using myFund.Common.UI.Events;
using myFund.Service.Service;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Fund.ViewModels
{
    public class FundViewModel : BindableBase
    {
        private readonly IFundService fundService;
        private readonly IEventAggregator eventAggregator;
        private ObservableCollection<Stock> fund;

        public FundViewModel(IFundService fundService, IEventAggregator eventAggregator)
        {
            this.Fund = new ObservableCollection<Stock>();
            this.fundService = fundService;
            this.eventAggregator = eventAggregator;
            this.InitializeCommands();
            this.WireUpEvents();
        }

        public ObservableCollection<Stock> Fund
        {
            get { return this.fund; }
            set
            {
                if (this.fund == value)
                {
                    return;
                }

                this.fund = value;
                this.OnPropertyChanged(nameof(this.Fund));
            }
        }

        public ICommand ReloadCommand { get; private set; }

        private bool IsReloading { get; set; }

        private void InitializeCommands()
        {
            this.ReloadCommand = new DelegateCommand(this.OnReloadCommandExecuted);
        }

        private async void OnReloadCommandExecuted()
        {
            this.IsReloading = true;
            try
            {
                var freshFund = await this.fundService.GetFundAsync();
                this.Fund = new ObservableCollection<Stock>(freshFund.Stocks);
            }
            catch (AggregateException ex)
            {
                // log exception
            }
            finally
            {
                this.IsReloading = false;
            }
        }

        private void WireUpEvents()
        {
            this.eventAggregator.GetEvent<StockAddedEvent>().Subscribe(this.OnStockAdded, ThreadOption.UIThread, false);
        }

        private void OnStockAdded(Stock stock)
        {
            if (stock == null)
            {
                return;
            }

            this.Fund?.Add(stock);
        }
    }
}