namespace EyeSoft.Windows.Model.Item.Direct
{
	using System;

	using EyeSoft.Windows.Model.Item.Property;

	public interface IItemLoaded<out TService>
	{
		IItemFilled<T> Fill<T>(Func<TService, T> func);
	}
}