namespace EyeSoft.Windows.Model.Collection.Property
{
	using System;

	using EyeSoft.Windows.Model.Collections.ObjectModel;

	public interface ICollectionPropertyFilled<TCollectionType>
	{
		void Completed(Action<IObservableCollection<TCollectionType>> action);
	}
}