using System;
using EyeSoft.Windows.Model.Collections.ObjectModel;

namespace EyeSoft.Windows.Model.ServiceProxy.With
{
	public interface IValueExecuted<TStart>
	{
		void Completed(Action<TStart> action);

		void UpdateCollection(IObservableCollection<TStart> collection);
	}
}