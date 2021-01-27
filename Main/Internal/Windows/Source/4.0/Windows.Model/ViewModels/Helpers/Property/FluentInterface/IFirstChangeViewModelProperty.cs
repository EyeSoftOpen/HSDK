namespace EyeSoft.Windows.Model.ViewModels.Helpers.Property.FluentInterface
{
    using System;

    public interface IFirstChangeViewModelProperty<out TProperty> : IAfterFirstChangeViewModelProperty<TProperty>
	{
		IAfterFirstChangeViewModelProperty<TProperty> OnFirstChange(Action<TProperty> func);
	}
}