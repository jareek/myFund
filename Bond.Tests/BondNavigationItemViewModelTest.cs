using System;
using Bond.ViewModels;
using myFund.Common.Platform;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Prism.Regions;

namespace Bond.Tests
{
    [TestClass]
    public class BondNavigationItemViewModelTest
    {
        [TestMethod]
        public void WhenModuleNameCalled_ThenBondModuleReturned()
        {
            var regionManagerMock = new Mock<IRegionManager>();
            var viewModel = new BondNavigationItemViewModel(regionManagerMock.Object);

            Assert.AreEqual(ModuleNames.BondModule, viewModel.ModuleName);
        }

        [TestMethod]
        public void WhenExecutingRequestModuleLoadCommand_ThenNavigatesToBondView()
        {
            var regionManagerMock = new Mock<IRegionManager>();
            regionManagerMock.Setup(x => x.RequestNavigate(RegionNames.StockMainContentRegion, new Uri("BondView", UriKind.Relative))).Verifiable();
            var viewModel = new BondNavigationItemViewModel(regionManagerMock.Object);

            viewModel.RequestModuleLoadCommand.Execute(null);

            regionManagerMock.VerifyAll();
        }
    }
}
