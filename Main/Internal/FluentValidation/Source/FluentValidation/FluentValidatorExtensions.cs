namespace EyeSoft.FluentValidation
{
	using global::FluentValidation;

	public static class FluentValidatorExtensions
	{
		public static IRuleBuilderOptions<T, string> OnlyCharacters<T>(this IRuleBuilder<T, string> ruleBuilder)
		{
			return ruleBuilder.SetValidator(new OnlyCharactersValidator());
		}

		public static IRuleBuilderOptions<T, string> OnlyCharactersOrSpaces<T>(this IRuleBuilder<T, string> ruleBuilder)
		{
			return ruleBuilder.SetValidator(new OnlyCharactersOrSpacesValidator());
		}
	}
}