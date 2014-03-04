namespace EyeSoft.Windows.Model.Collections.ObjectModel
{
	using System.Collections.Generic;
	using System.Collections.Specialized;
	using System.ComponentModel;

	public interface IReadOnlyObservableCollection<out T> : IEnumerable<T>, INotifyCollectionChanged, INotifyPropertyChanged
	{
	}
}