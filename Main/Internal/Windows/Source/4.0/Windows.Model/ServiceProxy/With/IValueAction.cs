namespace EyeSoft.Windows.Model.With
{
	using System;

	public interface IValueAction<out TService, TStart>
	{
		IValueExecuted<TStart> Execute<TServiceParameter>(Action<TService, TServiceParameter> func);
	}
}