namespace EyeSoft.Windows.Model.Collection.Property
{
	using System;

	using EyeSoft.Windows.Model.Collections.ObjectModel;

	public interface ICollectionFilled<TCollectionType>
	{
		void Completed(Action<IObservableCollection<TCollectionType>> action);

		void Completed<TConverted>(Action<IObservableCollection<TConverted>> action);
	}
}