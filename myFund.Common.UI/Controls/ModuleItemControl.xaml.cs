using System.Windows;
using System.Windows.Input;

namespace myFund.Common.UI.Controls
{
    public partial class ModuleItemControl
    {
       public static readonly DependencyProperty RequestModuleLoadCommandProperty = DependencyProperty.Register(
            "RequestModuleLoadCommand", typeof(ICommand), typeof(ModuleItemControl), new PropertyMetadata(default(ICommand)));

        public ICommand RequestModuleLoadCommand
        {
            get { return (ICommand) this.GetValue(ModuleItemControl.RequestModuleLoadCommandProperty); }
            set { this.SetValue(ModuleItemControl.RequestModuleLoadCommandProperty, value); }
        }

        public ModuleItemControl()
        {
            this.InitializeComponent();
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (e.Handled || this.RequestModuleLoadCommand == null)
            {
                return;
            }

            this.RequestModuleLoadCommand.Execute(null);
        }

        private void OnNavigationItemClick(object sender, RoutedEventArgs e)
        {
            this.RequestModuleLoadCommand?.Execute(null);
        }
    }
}
