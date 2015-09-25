using System;

namespace EyeSoft.Windows.Model.ServiceProxy.With
{
	public interface IValueAction<out TService, TStart>
	{
		IValueExecuted<TStart> Execute<TServiceParameter>(Action<TService, TServiceParameter> func);
	}
}