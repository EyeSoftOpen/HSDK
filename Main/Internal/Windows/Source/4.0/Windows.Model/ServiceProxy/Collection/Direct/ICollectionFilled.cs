namespace EyeSoft.Windows.Model.ServiceProxy.Collection.Direct
{
    using System;
    using Collections.ObjectModel;

    public interface ICollectionFilled<TCollectionType>
	{
		void Completed(Action<IObservableCollection<TCollectionType>> completed);

		void Completed<TConverted>(Action<IObservableCollection<TConverted>> completed);
	}
}