namespace EyeSoft.Architecture.Prototype.Windows.Model.ViewModels.Estimate
{
	using EyeSoft.FluentValidation;

	using global::FluentValidation;

	public class EstimateViewModelValidator : FluentValidator<EstimateViewModel>
	{
		public EstimateViewModelValidator()
		{
			RuleFor(x => x.Description).NotEmpty().Length(3, 20);
		}
	}
}