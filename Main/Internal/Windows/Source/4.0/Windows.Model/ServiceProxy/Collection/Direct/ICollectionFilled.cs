namespace EyeSoft.Windows.Model.Collection.Direct
{
	using System;

	using EyeSoft.Windows.Model.Collections.ObjectModel;

	public interface ICollectionFilled<TCollectionType>
	{
		void Completed(Action<IObservableCollection<TCollectionType>> completed);

		void Completed<TConverted>(Action<IObservableCollection<TConverted>> completed);
	}
}