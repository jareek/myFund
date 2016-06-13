using System.Windows;
using myFund.Service.Repository;
using myFund.Service.Service;
using myFund.ViewModels;
using myFund.Views;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Modularity;
using Prism.Unity;

namespace myFund
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            var shell = this.Container.Resolve<Shell>();
            var shellViewModel = this.Container.Resolve<ShellViewModel>();
            shell.DataContext = shellViewModel;
            return shell;
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            this.Container.RegisterType<Shell>();
            this.Container.RegisterType<ShellViewModel>();
            this.Container.RegisterType<IStockNameResolver, StockNameResolver>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IFundRepository, FundRepository>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IFundService, FundService>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IEventAggregator, EventAggregator>(new ContainerControlledLifetimeManager());
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }
    }
}