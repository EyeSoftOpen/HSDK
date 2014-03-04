namespace EyeSoft.Windows.Model.Collections.ObjectModel
{
	using System.Collections.Generic;

	internal class DefaultCollectionFactory : ICollectionFactory
	{
		public IObservableCollection<T> Create<T>()
		{
			return new ConcurrentObservableCollection<T>();
		}

		public IObservableCollection<T> Create<T>(IEnumerable<T> collection)
		{
			return new ConcurrentObservableCollection<T>(collection);
		}
	}
}