namespace EyeSoft.Windows.Model.Input
{
	using System;
	using System.Windows.Input;

	public interface INonGenericCommandFactory
	{
		ICommand Create(Action action);

		ICommand Create(Action action, Func<bool> canExecute);

		ICommand CreateAsync(Action action);

		ICommand CreateAsync(Action action, Func<bool> canExecute);
	}
}