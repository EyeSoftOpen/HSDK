namespace EyeSoft.Windows.Controls
{
	using System.Windows.Controls;

	public class EnsureVisibleListBox : ListBox
	{
		protected override void OnSelectionChanged(SelectionChangedEventArgs e)
		{
			if (SelectedItems.Count <= 0)
			{
				return;
			}

			ScrollIntoView(SelectedItems[0]);
		}
	}
}