namespace EyeSoft.Windows.Model.Input
{
	using System;
	using System.ComponentModel;
	using System.Reflection;
	using System.Windows.Input;

	using EyeSoft.Reflection;

	internal class NonGenericCommandFactory : IFactory<ICommand>
	{
		private const BindingFlags NonPublic = BindingFlags.Instance | BindingFlags.NonPublic;

		private static readonly MethodInfo createActionMethod =
			typeof(NonGenericCommandFactory).GetMethod("GiveAction", NonPublic);

		private static readonly MethodInfo createCanExecuteMethod =
			typeof(NonGenericCommandFactory).GetMethod("GiveCanExecute", NonPublic);

		private readonly INotifyPropertyChanged viewModel;

		private readonly CommandMethods methods;

		private readonly ICommandFactory commandFactory;

		public NonGenericCommandFactory(
			ICommandFactory commandFactory,
			INotifyPropertyChanged viewModel,
			CommandMethods methods)
		{
			this.viewModel = viewModel;
			this.methods = methods;
			this.commandFactory = commandFactory;
		}

		public ICommand Create()
		{
			ICommand command;

			var action = CreateAction();

			if (methods.CanExecuteMethod == null)
			{
				command = CreateCommand(action);
			}
			else
			{
				var canExecute = CreateCanExecute();

				command = CreateCommand(action, canExecute);
			}

			return command;
		}

		private ICommand CreateCommand(params object[] args)
		{
			var createMethodName = "Create";

			if (methods.ActionMethod.IsAsync)
			{
				createMethodName = createMethodName.Concatenate("Async");
			}

			var command = (ICommand)commandFactory.Invoke(createMethodName, args);

			return command;
		}

		private object CreateAction()
		{
			var action = createActionMethod.Invoke(this, new object[] { methods.ActionMethod.MethodInfo });

			return action;
		}

		private object CreateCanExecute()
		{
			var canExecute = createCanExecuteMethod.Invoke(this, new object[] { methods.CanExecuteMethod.MethodInfo });

			return canExecute;
		}

		private Action GiveAction(MethodInfo method)
		{
			var action = (Action)Delegate.CreateDelegate(typeof(Action), viewModel, method);

			return action;
		}

		private Func<bool> GiveCanExecute(MethodInfo method)
		{
			var canExecute = (Func<bool>)Delegate.CreateDelegate(typeof(Func<bool>), viewModel, method);

			return canExecute;
		}
	}
}