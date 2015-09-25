using System;

namespace EyeSoft.Windows.Model.ViewModels.Helpers.Property.FluentInterface
{
	public interface IChangingViewModelProperty<out TProperty>
	{
		IChangeViewModelProperty<TProperty> OnChanged(Action<TProperty> func);
	}
}