using Bond.Views;
using myFund.Common.Platform;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace Bond
{
    [Module(ModuleName = ModuleNames.BondModule)]
    public class BondModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;

        public BondModule(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }

        public void Initialize()
        {
            this.container.RegisterType<BondView>(new ContainerControlledLifetimeManager());
            this.container.RegisterType<BondModule>(new ContainerControlledLifetimeManager());
            this.regionManager.RegisterViewWithRegion(RegionNames.StockMainContentRegion, typeof(BondView));
            this.regionManager.RegisterViewWithRegion(RegionNames.StockNavigationRegion, typeof(BondNavigationItemView));
        }
    }
}
