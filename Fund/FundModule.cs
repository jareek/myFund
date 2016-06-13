using Fund.ViewModels;
using Fund.Views;
using myFund.Common.Platform;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace Fund
{
    [Module(ModuleName = ModuleNames.FundModule)]
    public class FundModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;

        public FundModule(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }

        public void Initialize()
        {
            this.container.RegisterType<FundView>(new ContainerControlledLifetimeManager());
            this.container.RegisterType<FundViewModel>(new ContainerControlledLifetimeManager());
            this.regionManager.RegisterViewWithRegion(RegionNames.MainContentRegion, typeof(FundView));
        }
    }
}