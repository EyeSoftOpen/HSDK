namespace Model
{
	using EyeSoft.FluentValidation;

	using FluentValidation;

	public class EstimateViewModelValidator : FluentValidator<EstimateViewModel>
	{
		public EstimateViewModelValidator()
		{
			RuleFor(x => x.Description).NotEmpty().Length(3, 20);
		}
	}
}