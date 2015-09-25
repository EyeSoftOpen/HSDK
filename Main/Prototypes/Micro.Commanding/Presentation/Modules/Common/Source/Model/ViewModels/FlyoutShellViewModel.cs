namespace EyeSoft.Architecture.Prototype.Windows.Model.ViewModels
{
	using System.Windows.Input;

	using EyeSoft.Windows.Model.ViewModels;
	using EyeSoft.Windows.Model.ViewModels.Navigation;

	public abstract class FlyoutShellViewModel : ShellViewModel
	{
		public ICommand OpenFlyoutCommand { get; set; }

		public ViewModel FlyoutDataContext
		{
			get { return GetProperty<ViewModel>(); }
			set { SetProperty(value); }
		}

		public bool IsFlyoutOpen
		{
			get { return GetProperty<bool>(); }
			set { SetProperty(value); }
		}

		protected void OpenFlyout()
		{
			var flyoutViewModel = new FlyoutViewModel { Title = "Flyout Title" };

			FlyoutService.Show(flyoutViewModel);
		}
	}
}