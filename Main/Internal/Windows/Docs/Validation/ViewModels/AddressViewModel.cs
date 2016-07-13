namespace EyeSoft.Demo.Validation.Windows.ViewModels
{
    using System.Collections.Generic;

    using EyeSoft.Validation;
    using EyeSoft.Windows.Model;

    public class AddressViewModel : ViewModel
    {
        public string Street
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        public string City
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        protected override IEnumerable<ValidationError> Validate()
        {
            return new AddressViewModelValidator().Validate(this);
        }
    }
}