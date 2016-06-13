using System.ComponentModel;
using System.Threading.Tasks;
using MvvmValidation;
using Prism.Mvvm;

namespace myFund.Common.UI.ViewModels
{
    public abstract class ValidatableViewModel : BindableBase, IValidatable, IDataErrorInfo
    {
        private bool? isValid;

        protected ValidatableViewModel()
        {
            this.Validator = new ValidationHelper();
            this.DataErrorInfoAdapter = new DataErrorInfoAdapter(this.Validator);

            this.WireUpValidationNotification();
        }

        private DataErrorInfoAdapter DataErrorInfoAdapter { get; set; }

        public string this[string columnName] => this.DataErrorInfoAdapter[columnName];

        public string Error => this.DataErrorInfoAdapter.Error;

        protected ValidationHelper Validator { get; private set; }

        Task<ValidationResult> IValidatable.Validate()
        {
            return this.Validator.ValidateAllAsync();
        }

        public bool? IsValid
        {
            get { return this.isValid; }
            private set
            {
                this.isValid = value;
                this.OnPropertyChanged(nameof(this.IsValid));
            }
        }

        protected async void Validate()
        {
            var validationResult = await ((IValidatable) this).Validate();
            this.IsValid = validationResult.IsValid;
            this.OnPropertyChanged(string.Empty);
        }

        private void WireUpValidationNotification()
        {
            this.Validator.ResultChanged += (o, e) =>
            {
                var propertyName = e.Target as string;
                if (!string.IsNullOrEmpty(propertyName))
                {
                    this.OnPropertyChanged(propertyName);
                }
            };
            this.Validator.ResultChanged += this.OnValidationResultChanged;
        }

        private void OnValidationResultChanged(object sender, ValidationResultChangedEventArgs e)
        {
            if (this.IsValid.GetValueOrDefault(true))
            {
                return;
            }

            var validationResult = this.Validator.GetResult();
            this.IsValid = validationResult.IsValid;
        }
    }
}
