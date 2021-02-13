namespace EyeSoft.Windows.Model.Item.Property
{
	using System;

	public interface IItemFilled<out TProperty>
	{
		void Completed(Action<TProperty> action);

		void Completed<TConveted>(Action<TConveted> action);
	}
}