namespace EyeSoft.Core.Validation
{
    using System.Linq;
    using System.Text;

    public static class ValidatorExtensions
	{
		public static void ThrowOnErrors<T>(this IValidator<T> validator, T instance)
		{
			var errorCollection = validator.Validate(instance);

			var errors = errorCollection.ToArray();

			if (!errors.Any())
			{
				return;
			}

			var exceptionMessageBuilder = new StringBuilder();

			const string ErrorFormat = "\t- {0}: {1} - Value: {2}";

			exceptionMessageBuilder.AppendLine(string.Format("The instance of type '{0}' is not valid:", typeof(T).Name));

			foreach (var error in errors)
			{
				exceptionMessageBuilder.AppendFormat(ErrorFormat, error.PropertyName, error.Message, error.Value);
				exceptionMessageBuilder.AppendLine();
			}

			var message = exceptionMessageBuilder.ToString().TrimEnd();

			throw new ValidationException(message);
		}
	}
}