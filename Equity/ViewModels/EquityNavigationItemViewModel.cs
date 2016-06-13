using Equity.Views;
using myFund.Common.Platform;
using myFund.Common.UI.ViewModels;
using Prism.Regions;

namespace Equity.ViewModels
{
    public class EquityNavigationItemViewModel : NavigationItemViewModel
    {
        public EquityNavigationItemViewModel(IRegionManager regionManager)
            : base(regionManager)
        { }

        public override string ModuleName => ModuleNames.EquityModule;

        protected override string NavigationItemMainViewName => nameof(EquityView);

        protected override string NavigationItemMainViewReionName => RegionNames.StockMainContentRegion;
    }
}
