namespace EyeSoft.Wpf.Facilities.Demo.Configuration.Helpers
{
	using System.Collections.Generic;

	using EyeSoft.Windows.Model.Collections.ObjectModel;

	internal class SafeCollectionFactory : ICollectionFactory
	{
		public IObservableCollection<T> Create<T>()
		{
			return new SafeSyncedCollection<T>();
		}

		public IObservableCollection<T> Create<T>(IEnumerable<T> collection)
		{
			return new SafeSyncedCollection<T>(collection);
		}
	}
}