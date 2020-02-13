namespace EyeSoft.Windows.Model.Input
{
	using System;
	using System.Windows.Input;

	public interface IGenericCommandFactory
	{
		ICommand Create<T>(IViewModel viewModel, Action<T> action);

		ICommand Create<T>(IViewModel viewModel, Action<T> action, Func<T, bool> canExecute);

		ICommand CreateAsync<T>(IViewModel viewModel, Action<T> action);

		ICommand CreateAsync<T>(IViewModel viewModel, Action<T> action, Func<T, bool> canExecute);
	}
}