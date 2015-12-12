namespace EyeSoft.Windows.Model.Item.Property
{
	using System;

	public interface IItemPropertyFilled<out TProperty>
	{
		void Completed(Action<TProperty> action);
	}
}