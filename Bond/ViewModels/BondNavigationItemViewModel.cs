using Bond.Views;
using myFund.Common.Platform;
using myFund.Common.UI.ViewModels;
using Prism.Regions;

namespace Bond.ViewModels
{
    public class BondNavigationItemViewModel : NavigationItemViewModel
    {
        public BondNavigationItemViewModel(IRegionManager regionManager)
            : base(regionManager)
        { }

        public override string ModuleName => ModuleNames.BondModule;

        protected override string NavigationItemMainViewName => nameof(BondView);

        protected override string NavigationItemMainViewReionName => RegionNames.StockMainContentRegion;
    }
}