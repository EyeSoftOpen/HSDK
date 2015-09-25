using System;

namespace EyeSoft.Windows.Model.ViewModels.Helpers.Property.FluentInterface
{
	public interface IAfterFirstChangeViewModelProperty<out TProperty> :
		IChangeViewModelProperty<TProperty>,
		IChangingViewModelProperty<TProperty>
	{
		IChangingViewModelProperty<TProperty> OnChanging(Action<TProperty> onChangeAction);
	}
}