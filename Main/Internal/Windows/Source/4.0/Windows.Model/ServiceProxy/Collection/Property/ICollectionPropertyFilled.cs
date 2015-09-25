using System;
using EyeSoft.Windows.Model.Collections.ObjectModel;

namespace EyeSoft.Windows.Model.ServiceProxy.Collection.Property
{
	public interface ICollectionPropertyFilled<TCollectionType>
	{
		void Completed(Action<IObservableCollection<TCollectionType>> action);
	}
}