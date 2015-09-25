using System;
using EyeSoft.Windows.Model.ServiceProxy.Item.Property;

namespace EyeSoft.Windows.Model.ServiceProxy.Item.Direct
{
	public interface IItemLoaded<out TService>
	{
		IItemFilled<T> Fill<T>(Func<TService, T> func);
	}
}