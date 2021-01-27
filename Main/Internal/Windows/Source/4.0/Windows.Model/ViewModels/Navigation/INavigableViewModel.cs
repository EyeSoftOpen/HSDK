namespace EyeSoft.Windows.Model.ViewModels.Navigation
{
	public interface INavigableViewModel
	{
		void Navigate(NavigableViewModel navigable);

		void Close();
	}
}