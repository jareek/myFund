using System;
using System.Diagnostics.Contracts;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace myFund.Common.UI.ViewModels
{
    public abstract class NavigationItemViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        private Uri viewUri;

        protected NavigationItemViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            this.InitializeCommands();
        }

        public ICommand RequestModuleLoadCommand { get; private set; }

        public abstract string ModuleName { get; }

        protected abstract string NavigationItemMainViewName { get; }

        protected abstract string NavigationItemMainViewReionName { get; }

        private Uri ViewUri => this.viewUri ?? (this.viewUri = new Uri(this.NavigationItemMainViewName, UriKind.Relative));

        private void InitializeCommands()
        {
            this.RequestModuleLoadCommand = new DelegateCommand(this.OnRequestModuleLoadCommandExecuted);
        }

        private void OnRequestModuleLoadCommandExecuted()
        {
            this.regionManager.RequestNavigate(this.NavigationItemMainViewReionName, this.ViewUri);
        }
    }
}
