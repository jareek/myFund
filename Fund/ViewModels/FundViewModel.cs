using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Fund.Model;
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
        private FundSummary fundSummary;


        public FundViewModel(IFundService fundService, IEventAggregator eventAggregator)
        {
            this.Fund = new ObservableCollection<Stock>();
            this.FundSummary = new FundSummary();
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

        public FundSummary FundSummary
        {
            get { return this.fundSummary; }
            set { this.SetProperty(ref this.fundSummary, value, nameof(this.FundSummary)); }
        }

        // Reload would have more sense whithin multi client scenario with many users without any duplex communication like signalr.
        public ICommand ReloadCommand { get; private set; }

        private bool IsReloading { get; set; }

        private void InitializeCommands()
        {
            this.ReloadCommand = new DelegateCommand(this.OnReloadCommandExecuted, () => !this.IsReloading );
        }

        private async void OnReloadCommandExecuted()
        {
            this.IsReloading = true;
            ((DelegateCommand)this.ReloadCommand).RaiseCanExecuteChanged();
            try
            {
                var fundObject = await this.fundService.GetFundAsync();
                this.Fund = new ObservableCollection<Stock>(fundObject.Stocks);

                // For now calculated synchronously. If will be time consuming should be parallerized or moved to separate thread. 
                // In case you want to move it to another thread keep in mind to update UI in main thread.
                this.UpdateFundSummary(fundObject);
            }
            catch (AggregateException ex)
            {
                // log exception
            }
            finally
            {
                this.IsReloading = false;
                ((DelegateCommand)this.ReloadCommand).RaiseCanExecuteChanged();
            }
        }

        private void WireUpEvents()
        {
            this.eventAggregator.GetEvent<StockAddedEvent>().Subscribe(this.OnStockAdded, ThreadOption.UIThread, false);
        }

        private void OnStockAdded(Stock stock)
        {
            if (stock == null || this.Fund == null)
            {
                return;
            }

            this.Fund.Add(stock);

            var fundObject = new myFund.Common.Model.Fund {Stocks = this.Fund};

            // For now calculated synchronously. If will be time consuming should be parallerized or moved to separate thread. 
            // In case you want to move it to another thread keep in mind to update UI in main thread. 
            foreach (var s in this.Fund)
            {
                s.StockWeight.Calculate(fundObject);
            }

            // Same as above
            this.UpdateFundSummary(fundObject);
        }

        private void UpdateFundSummary(myFund.Common.Model.Fund fundObject)
        {
            var fundSummaryValue = new FundSummary();
            fundSummaryValue.Calculate(fundObject);
            this.FundSummary = fundSummaryValue;
        }
    }
}