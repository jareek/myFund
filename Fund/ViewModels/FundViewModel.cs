using Prism.Mvvm;

namespace Fund.ViewModels
{
    public class FundViewModel : BindableBase
    {
        private string title = "Hello Fund";
        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        } 
    }
}