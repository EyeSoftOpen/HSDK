using System;
using EyeSoft.Windows.Model.Collections.ObjectModel;

namespace EyeSoft.Windows.Model.ServiceProxy.Collection.Direct
{
	public interface ICollectionFilled<TCollectionType>
	{
		void Completed(Action<IObservableCollection<TCollectionType>> completed);

		void Completed<TConverted>(Action<IObservableCollection<TConverted>> completed);
	}
}