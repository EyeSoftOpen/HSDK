namespace EyeSoft.Windows.Model.ServiceProxy.Collection.Property
{
    using System;
    using Collections.ObjectModel;

    public interface ICollectionFilled<TCollectionType>
	{
		void Completed(Action<IObservableCollection<TCollectionType>> action);

		void Completed<TConverted>(Action<IObservableCollection<TConverted>> action);
	}
}