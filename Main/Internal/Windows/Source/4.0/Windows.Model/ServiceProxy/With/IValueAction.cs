namespace EyeSoft.Windows.Model.ServiceProxy.With
{
    using System;

    public interface IValueAction<out TService, TStart>
	{
		IValueExecuted<TStart> Execute<TServiceParameter>(Action<TService, TServiceParameter> func);
	}
}