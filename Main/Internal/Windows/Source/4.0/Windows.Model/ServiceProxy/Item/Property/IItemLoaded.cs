namespace EyeSoft.Windows.Model.ServiceProxy.Item.Property
{
    using System;

    public interface IItemLoaded<out TService, out TProperty>
	{
		IItemPropertyFilled<TProperty> Fill<T>(Func<TService, T> func);
	}
}