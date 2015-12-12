namespace EyeSoft.Windows.Model.Item.Property
{
	using System;

	public interface IItemLoaded<out TService, out TProperty>
	{
		IItemPropertyFilled<TProperty> Fill<T>(Func<TService, T> func);
	}
}