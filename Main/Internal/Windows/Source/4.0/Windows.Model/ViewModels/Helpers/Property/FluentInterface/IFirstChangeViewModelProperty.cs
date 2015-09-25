using System;

namespace EyeSoft.Windows.Model.ViewModels.Helpers.Property.FluentInterface
{
	public interface IFirstChangeViewModelProperty<out TProperty> : IAfterFirstChangeViewModelProperty<TProperty>
	{
		IAfterFirstChangeViewModelProperty<TProperty> OnFirstChange(Action<TProperty> func);
	}
}