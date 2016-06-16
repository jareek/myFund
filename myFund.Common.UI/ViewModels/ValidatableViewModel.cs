using System.ComponentModel;
using MvvmValidation;
using Prism.Mvvm;

namespace myFund.Common.UI.ViewModels
{
    public abstract class ValidatableViewModel : BindableBase, IDataErrorInfo
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

        public bool? IsValid
        {
            get { return this.isValid; }
            private set
            {
                this.isValid = value;
                this.OnPropertyChanged(nameof(this.IsValid));
            }
        }

        protected void Validate()
        {
            var validationResult = this.Validator.ValidateAll();
            this.IsValid = validationResult.IsValid;
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
