using Equity.Views;
using Equity.ViewModels;
using myFund.Common.Platform;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace Equity
{
    [Module(ModuleName = ModuleNames.EquityModule)]
    public class EquityModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;

        public EquityModule(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }

        public void Initialize()
        {
            this.container.RegisterType<EquityView>(new ContainerControlledLifetimeManager());
            this.container.RegisterType<EquityViewModel>(new ContainerControlledLifetimeManager());
            this.regionManager.RegisterViewWithRegion(RegionNames.StockMainContentRegion, typeof(EquityView));
            this.regionManager.RegisterViewWithRegion(RegionNames.StockNavigationRegion, typeof(EquityNavigationItemView));
        }
    }
}
