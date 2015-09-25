using System;

namespace EyeSoft.Windows.Model.ServiceProxy.Item.Property
{
	public interface IItemPropertyFilled<out TProperty>
	{
		void Completed(Action<TProperty> action);
	}
}