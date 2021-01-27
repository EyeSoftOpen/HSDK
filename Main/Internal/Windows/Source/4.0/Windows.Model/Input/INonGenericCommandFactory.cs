namespace EyeSoft.Windows.Model.Input
{
	using System;
	using System.Windows.Input;
    using ViewModels;

    public interface INonGenericCommandFactory
	{
		ICommand Create(IViewModel viewModel, Action action);

		ICommand Create(IViewModel viewModel, Action action, Func<bool> canExecute);

		ICommand CreateAsync(IViewModel viewModel, Action action);

		ICommand CreateAsync(IViewModel viewModel, Action action, Func<bool> canExecute);
	}
}