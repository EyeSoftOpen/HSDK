namespace EyeSoft.Windows.Model.Input
{
	using System;
	using System.ComponentModel;
	using System.Reflection;
	using System.Windows.Input;

	using EyeSoft.Reflection;

	internal class GenericCommandFactory : IFactory<ICommand>
	{
		private const BindingFlags NonPublic = BindingFlags.Instance | BindingFlags.NonPublic;

		private static readonly MethodInfo createActionMethod =
			typeof(GenericCommandFactory).GetMethod("GiveAction", NonPublic);

		private static readonly MethodInfo createCanExecuteMethod =
			typeof(GenericCommandFactory).GetMethod("GiveCanExecute", NonPublic);

		private readonly INotifyPropertyChanged viewModel;

		private readonly CommandMethods methods;

		private readonly ICommandFactory commandFactory;

		private readonly Type parameterType;

		public GenericCommandFactory(
			ICommandFactory commandFactory,
			INotifyPropertyChanged viewModel,
			CommandMethods methods,
			Type parameterType)
		{
			this.viewModel = viewModel;
			this.methods = methods;
			this.parameterType = parameterType;
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

			var command =
				(ICommand)commandFactory
					.Invoke(createMethodName, new[] { parameterType }, args);

			return command;
		}

		private object CreateAction()
		{
			var creaActionGeneric = createActionMethod.MakeGenericMethod(parameterType);

			var action = creaActionGeneric.Invoke(this, new object[] { methods.ActionMethod.MethodInfo });

			return action;
		}

		private object CreateCanExecute()
		{
			var creaCanExecuteGeneric = createCanExecuteMethod.MakeGenericMethod(parameterType);

			var canExecute = creaCanExecuteGeneric.Invoke(this, new object[] { methods.CanExecuteMethod.MethodInfo });

			return canExecute;
		}

		private Action<T> GiveAction<T>(MethodInfo method)
		{
			var action = (Action<T>)Delegate.CreateDelegate(typeof(Action<T>), viewModel, method);

			return action;
		}

		private Func<T, bool> GiveCanExecute<T>(MethodInfo method)
		{
			var canExecute = (Func<T, bool>)Delegate.CreateDelegate(typeof(Func<T, bool>), viewModel, method);

			return canExecute;
		}
	}
}