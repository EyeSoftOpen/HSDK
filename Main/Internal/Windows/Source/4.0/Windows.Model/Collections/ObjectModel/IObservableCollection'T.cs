namespace EyeSoft.Windows.Model.Collections.ObjectModel
{
	using System.Collections.Generic;

	public interface IObservableCollection<T> : IList<T>, IReadOnlyObservableCollection<T>
	{
		void Move(int oldIndex, int newIndex);

		void AddRange(IEnumerable<T> collection);

		void ReplaceOrAdd(T item);

		void Remove(IEnumerable<T> collection);
	}
}