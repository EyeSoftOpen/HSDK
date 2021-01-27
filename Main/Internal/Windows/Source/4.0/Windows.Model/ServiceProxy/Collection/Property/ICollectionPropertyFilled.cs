namespace EyeSoft.Windows.Model.ServiceProxy.Collection.Property
{
    using System;
    using Collections.ObjectModel;

    public interface ICollectionPropertyFilled<TCollectionType>
	{
		void Completed(Action<IObservableCollection<TCollectionType>> action);
	}
}