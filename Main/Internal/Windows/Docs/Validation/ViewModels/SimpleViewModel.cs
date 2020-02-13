namespace EyeSoft.Demo.Validation.Windows.ViewModels
{
    using System.Collections.Generic;

    using EyeSoft.Validation;
    using EyeSoft.Windows.Model;

    public class SimpleViewModel : ViewModel
    {
        public string FirstName
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        public override IEnumerable<ValidationError> Validate()
        {
            return new SimpleViewModelValidator().Validate(this);
        }
    }
}