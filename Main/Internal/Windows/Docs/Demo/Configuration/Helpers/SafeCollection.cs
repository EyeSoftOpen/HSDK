namespace EyeSoft.Wpf.Facilities.Demo.Configuration.Helpers
{
	using System.Collections.Generic;
	using System.Windows.Data;

	using EyeSoft.Windows.Model.Collections.ObjectModel;

	internal class SafeSyncedCollection<T> : ConcurrentObservableCollection<T>
	{
		public SafeSyncedCollection()
		{
			BindingOperations.EnableCollectionSynchronization(this, syncLock);
		}

		public SafeSyncedCollection(IEnumerable<T> collection) : base(collection)
		{
			BindingOperations.EnableCollectionSynchronization(this, syncLock);
		}
	}
}