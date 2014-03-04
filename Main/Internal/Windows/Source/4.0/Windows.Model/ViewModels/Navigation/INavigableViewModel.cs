namespace EyeSoft.Windows.Model
{
	public interface INavigableViewModel
	{
		void Navigate(NavigableViewModel navigable);

		void Close();
	}
}