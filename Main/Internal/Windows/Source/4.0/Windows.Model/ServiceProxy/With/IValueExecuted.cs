namespace EyeSoft.Windows.Model.With
{
	using System;

	using EyeSoft.Windows.Model.Collections.ObjectModel;

	public interface IValueExecuted<TStart>
	{
		void Completed(Action<TStart> action);

		void UpdateCollection(IObservableCollection<TStart> collection);
	}
}