namespace EyeSoft.Windows.Model.Input
{
	using System;
	using System.Windows.Input;

	public interface IGenericCommandFactory
	{
		ICommand Create<T>(Action<T> action);

		ICommand Create<T>(Action<T> action, Func<T, bool> canExecute);

		ICommand CreateAsync<T>(Action<T> action);

		ICommand CreateAsync<T>(Action<T> action, Func<T, bool> canExecute);
	}
}