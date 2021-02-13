namespace EyeSoft.Demo.Validation.Windows.ViewModels
{
    using System.Collections.Generic;
    using EyeSoft.Validation;
    using EyeSoft.Windows.Model;
    using Validators;

    public class SimpleViewModel : ViewModel
    {
        public string FirstName
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        public override IEnumerable<ValidationError> Validate()
        {
            return new SimpleViewModelValidator().Validate(this);
        }
    }
}