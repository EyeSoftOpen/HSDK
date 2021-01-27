namespace EyeSoft.Windows.Model.ServiceProxy.Item.Property
{
    using System;

    public interface IItemFilled<out TProperty>
	{
		void Completed(Action<TProperty> action);

		void Completed<TConveted>(Action<TConveted> action);
	}
}