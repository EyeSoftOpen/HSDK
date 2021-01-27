namespace EyeSoft.Windows.Model.ViewModels.Helpers.Property.FluentInterface
{
    using System;

    public interface IChangingViewModelProperty<out TProperty>
	{
		IChangeViewModelProperty<TProperty> OnChanged(Action<TProperty> func);
	}
}