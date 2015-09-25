namespace EyeSoft.Windows.Model.ViewModels.Navigation
{
	public abstract class NavigableViewModel : AutoRegisterViewModel
	{
		protected readonly INavigableViewModel navigableViewModel;

		protected NavigableViewModel(INavigableViewModel navigableViewModel)
		{
			this.navigableViewModel = navigableViewModel;
		}

		protected void Navigate<TNavigableViewModel>() where TNavigableViewModel : NavigableViewModel, new()
		{
			Navigate(new TNavigableViewModel());
		}

		protected void Navigate(NavigableViewModel navigable)
		{
			navigableViewModel.Navigate(navigable);
		}

		protected override void Close()
		{
			navigableViewModel.Close();
		}
	}
}