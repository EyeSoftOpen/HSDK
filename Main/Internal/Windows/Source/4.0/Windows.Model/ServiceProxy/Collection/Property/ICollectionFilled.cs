using System;
using EyeSoft.Windows.Model.Collections.ObjectModel;

namespace EyeSoft.Windows.Model.ServiceProxy.Collection.Property
{
	public interface ICollectionFilled<TCollectionType>
	{
		void Completed(Action<IObservableCollection<TCollectionType>> action);

		void Completed<TConverted>(Action<IObservableCollection<TConverted>> action);
	}
}