namespace EyeSoft.Windows.Model.DialogService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using ViewModels;

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