namespace EyeSoft.FluentValidation
{
	using System.Linq;

	using global::FluentValidation.Validators;

	public class OnlyCharactersOrSpacesValidator : PropertyValidator
	{
		public OnlyCharactersOrSpacesValidator() : base("The value is not a text.")
		{
		}

		protected override bool IsValid(PropertyValidatorContext context)
		{
			return
				context.PropertyValue == null || ((string)context.PropertyValue).All(x => char.IsLetter(x) || x == ' ');
		}
	}
}