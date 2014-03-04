namespace EyeSoft.Windows.Model
{
	using System.Collections.Generic;
	using System.Linq;

	using EyeSoft.Validation;

	internal class DefaultValidator : Validator<object>
	{
		public override IEnumerable<ValidationError> Validate(object instance, string propertyName)
		{
			return Enumerable.Empty<ValidationError>();
		}
	}
}