namespace EyeSoft.Windows.Model
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;

	public class OpenedWindowsCollection
	{
		private readonly HashSet<Window> openedWindows;

		internal OpenedWindowsCollection(HashSet<Window> openedWindows)
		{
			this.openedWindows = openedWindows;
		}

		public Window Window<TViewModel>() where TViewModel : ViewModel
		{
			var window = openedWindows.Single(x => x.DataContext.GetType() == typeof(TViewModel));

			return window;
		}
	}
}