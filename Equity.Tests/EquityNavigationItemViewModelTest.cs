using System;
using Equity.ViewModels;
using myFund.Common.Platform;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Prism.Regions;

namespace Equity.Tests
{
    [TestClass]
    public class EquityNavigationItemViewModelTest
    {
        [TestMethod]
        public void WhenModuleNameCalled_ThenEquityModuleReturned()
        {
            var regionManagerMock = new Mock<IRegionManager>();
            var viewModel = new EquityNavigationItemViewModel(regionManagerMock.Object);

            Assert.AreEqual(ModuleNames.EquityModule, viewModel.ModuleName);
        }

        [TestMethod]
        public void WhenExecutingRequestModuleLoadCommand_ThenNavigatesToEquityView()
        {
            var regionManagerMock = new Mock<IRegionManager>();
            regionManagerMock.Setup(x => x.RequestNavigate(RegionNames.StockMainContentRegion, new Uri("EquityView", UriKind.Relative))).Verifiable();
            var viewModel = new EquityNavigationItemViewModel(regionManagerMock.Object);

            viewModel.RequestModuleLoadCommand.Execute(null);

            regionManagerMock.VerifyAll();
        }
    }
}
