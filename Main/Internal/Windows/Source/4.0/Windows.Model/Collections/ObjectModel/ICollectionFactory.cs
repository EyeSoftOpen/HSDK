namespace EyeSoft.Windows.Model.Collections.ObjectModel
{
	using System.Collections.Generic;

	public interface ICollectionFactory
	{
		IObservableCollection<T> Create<T>();

		IObservableCollection<T> Create<T>(IEnumerable<T> collection);
	}
}