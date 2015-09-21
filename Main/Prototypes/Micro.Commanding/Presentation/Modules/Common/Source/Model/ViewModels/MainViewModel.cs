using EyeSoft.Architecture.Prototype.Windows.Model.ViewModels.Estimate;
using EyeSoft.Windows.Model.ViewModels.Navigation;

namespace EyeSoft.Architecture.Prototype.Windows.Model.ViewModels
{
	public class MainViewModel : ShellViewModel
	{
		public MainViewModel()
		{
			Estimate = new EstimateViewModel();

			//throw new InvalidOperationException();
		}

		public EstimateViewModel Estimate { get; }

		protected override void Dispose(bool disposing)
		{
			Estimate.Dispose();
			base.Dispose(disposing);
		}
	}
}