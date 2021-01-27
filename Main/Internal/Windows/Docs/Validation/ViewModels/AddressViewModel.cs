namespace EyeSoft.Demo.Validation.Windows.ViewModels
{
    using System.Collections.Generic;
    using Core.Validation;
    using EyeSoft.Windows.Model;
    using EyeSoft.Windows.Model.ViewModels;
    using Validators;

    public class AddressViewModel : ViewModel
    {
        public string Street
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        public string City
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        public override IEnumerable<ValidationError> Validate()
        {
            return new AddressViewModelValidator().Validate(this);
        }
    }
}