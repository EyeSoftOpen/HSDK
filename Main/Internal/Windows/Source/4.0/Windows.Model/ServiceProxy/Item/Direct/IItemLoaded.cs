namespace EyeSoft.Windows.Model.ServiceProxy.Item.Direct
{
    using System;
    using Property;

    public interface IItemLoaded<out TService>
	{
		IItemFilled<T> Fill<T>(Func<TService, T> func);
	}
}