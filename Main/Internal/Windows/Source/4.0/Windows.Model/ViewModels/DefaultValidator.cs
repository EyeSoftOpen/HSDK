namespace EyeSoft.Windows.Model.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Core.Validation;

    internal class DefaultValidator : Validator<object>
	{
		public override IEnumerable<ValidationError> Validate(object instance, string propertyName)
		{
			return Enumerable.Empty<ValidationError>();
		}
	}
}