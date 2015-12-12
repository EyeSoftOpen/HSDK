namespace EyeSoft.Windows.Model
{
	using System;

	public interface IViewModelProperty<out TProperty> : IFirstChangeViewModelProperty<TProperty>
	{
		IFirstChangeViewModelProperty<TProperty> OnFirstChanging(Action<TProperty> onChangeAction);
	}
}