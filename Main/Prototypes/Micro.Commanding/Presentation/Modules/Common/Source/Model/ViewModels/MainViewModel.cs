namespace EyeSoft.Architecture.Prototype.Windows.Model.ViewModels
{
	using System;
	using System.Windows.Input;

	using EyeSoft.Architecture.Prototype.Windows.Model.ViewModels.Estimate;

	public class MainViewModel : FlyoutShellViewModel
	{
		public MainViewModel(string baseApiUrl)
		{
			Estimate = new EstimateViewModel();

			SwaggerUrl = new Uri(new Uri(baseApiUrl), "/swagger").ToString();

			//throw new InvalidOperationException();
		}

		public string SwaggerUrl { get; }

		public EstimateViewModel Estimate { get; }

		public ICommand OpenFlyoutCommand { get; set; }

		protected override void Dispose(bool disposing)
		{
			Estimate.Dispose();
			base.Dispose(disposing);
		}

		protected void OpenFlyout()
		{
			var flyoutDataContext = new FlyoutViewModel { Title ="Flyout Title" };

			FlyoutService.Show(flyoutDataContext);
		}
	}
}