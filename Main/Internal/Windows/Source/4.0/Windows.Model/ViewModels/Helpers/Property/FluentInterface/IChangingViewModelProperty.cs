namespace EyeSoft.Windows.Model
{
	using System;

	public interface IChangingViewModelProperty<out TProperty>
	{
		IChangeViewModelProperty<TProperty> OnChanged(Action<TProperty> func);
	}
}