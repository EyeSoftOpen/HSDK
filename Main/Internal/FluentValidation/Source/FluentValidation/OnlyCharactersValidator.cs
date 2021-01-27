namespace EyeSoft.FluentValidation
{
	using System.Linq;

	using global::FluentValidation.Validators;

	public class OnlyCharactersValidator : PropertyValidator
	{
		protected override bool IsValid(PropertyValidatorContext context)
		{
			return
				context.PropertyValue == null ||
				((string)context.PropertyValue).All(char.IsLetter);
		}

        protected override string GetDefaultMessageTemplate()
        {
            return "The value is not a text.";

        }
    }
}