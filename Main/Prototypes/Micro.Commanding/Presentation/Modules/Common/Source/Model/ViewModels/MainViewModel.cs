namespace EyeSoft.Architecture.Prototype.Windows.Model.ViewModels
{
	using System;

	using EyeSoft.Architecture.Prototype.Windows.Model.ViewModels.Estimate;

	public class MainViewModel : FlyoutShellViewModel
	{
		public MainViewModel(string baseApiUrl)
		{
			Estimate = new EstimateViewModel();

			SwaggerUrl = new Uri(new Uri(baseApiUrl), "/swagger").ToString();

			////throw new InvalidOperationException();
		}

		public string SwaggerUrl { get; }

		public EstimateViewModel Estimate { get; }

		protected override void Dispose(bool disposing)
		{
			Estimate.Dispose();
			base.Dispose(disposing);
		}
	}
}