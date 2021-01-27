namespace EyeSoft.Demo.Validation.Windows.ViewModels
{
    using System.Collections.Generic;
    using Core.Validation;
    using EyeSoft.Windows.Model;
    using EyeSoft.Windows.Model.ViewModels;
    using Validators;

    public class SubjectViewModel : ViewModel
    {
        public SubjectViewModel()
        {
            Address = new AddressViewModel();
        }

        public string FirstName
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        public AddressViewModel Address
        {
            get { return GetProperty<AddressViewModel>(); }
            set { SetProperty(value); }
        }

        public override IEnumerable<ValidationError> Validate()
        {
            return new SubjectViewModelValidator().Validate(this);
        }
    }
}