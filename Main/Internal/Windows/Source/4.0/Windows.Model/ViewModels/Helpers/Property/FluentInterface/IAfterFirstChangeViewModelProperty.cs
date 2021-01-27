namespace EyeSoft.Windows.Model.ViewModels.Helpers.Property.FluentInterface
{
    using System;

    public interface IAfterFirstChangeViewModelProperty<out TProperty> :
		IChangeViewModelProperty<TProperty>,
		IChangingViewModelProperty<TProperty>
	{
		IChangingViewModelProperty<TProperty> OnChanging(Action<TProperty> onChangeAction);
	}
}