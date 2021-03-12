namespace EyeSoft.FluentValidation
{
	using System.Linq;

	using global::FluentValidation.Validators;

	public class OnlyCharactersOrSpacesValidator : PropertyValidator
	{
        protected override bool IsValid(PropertyValidatorContext context)
		{
			return
				context.PropertyValue == null || ((string)context.PropertyValue).All(x => char.IsLetter(x) || x == ' ');
		}

        protected override string GetDefaultMessageTemplate()
        {
            return "The value is not a text.";

		}
    }
}