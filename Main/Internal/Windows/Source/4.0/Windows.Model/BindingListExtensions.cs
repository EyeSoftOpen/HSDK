namespace EyeSoft.Windows.Model
{
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Windows;
    using Core.Collections.Generic;
    using Execution;

    public static class BindingListExtensions
	{
		public static void SafeAddRange<T>(this BindingList<T> bindingList, IEnumerable<T> data)
		{
			bindingList.RaiseListChangedEvents = false;
			bindingList.AddRange(data);

			bindingList.RaiseListChangedEvents = true;

			Application.Current.Dispatcher.BeginInvoke(bindingList.ResetBindings);
		}
	}
}