namespace EyeSoft.Demo.Validation.Windows.ViewModels
{
    using System.Collections.Generic;
    using EyeSoft.Validation;
    using EyeSoft.Windows.Model;
    using Validators;

    public class SubjectViewModel : ViewModel
    {
        public SubjectViewModel()
        {
            Address = new AddressViewModel();
        }

        public string FirstName
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        public AddressViewModel Address
        {
            get => GetProperty<AddressViewModel>();
            set => SetProperty(value);
        }

        public override IEnumerable<ValidationError> Validate()
        {
            return new SubjectViewModelValidator().Validate(this);
        }
    }
}