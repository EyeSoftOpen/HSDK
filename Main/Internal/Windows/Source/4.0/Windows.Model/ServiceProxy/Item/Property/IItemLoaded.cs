using System;

namespace EyeSoft.Windows.Model.ServiceProxy.Item.Property
{
	public interface IItemLoaded<out TService, out TProperty>
	{
		IItemPropertyFilled<TProperty> Fill<T>(Func<TService, T> func);
	}
}