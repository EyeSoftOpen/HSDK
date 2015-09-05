namespace Model
{
	using System;

	using EyeSoft.Windows.Model;

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