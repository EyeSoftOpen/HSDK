namespace EyeSoft.Windows.Model.ServiceProxy.With
{
    using System;
    using Collections.ObjectModel;

    public interface IValueExecuted<TStart>
	{
		void Completed(Action<TStart> action);

		void UpdateCollection(IObservableCollection<TStart> collection);
	}
}