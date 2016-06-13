using Prism.Mvvm;

namespace myFund.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        public ShellViewModel()
        {
            this.Title = "Fund Manager";
        }

        public string Title { get; private set; }
    }
}
