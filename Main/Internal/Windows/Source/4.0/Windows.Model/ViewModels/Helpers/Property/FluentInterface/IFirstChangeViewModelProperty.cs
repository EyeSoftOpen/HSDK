namespace EyeSoft.Windows.Model
{
	using System;

	public interface IFirstChangeViewModelProperty<out TProperty> : IAfterFirstChangeViewModelProperty<TProperty>
	{
		IAfterFirstChangeViewModelProperty<TProperty> OnFirstChange(Action<TProperty> func);
	}
}