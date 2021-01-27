namespace EyeSoft.Windows.Model.Input
{
	using System;
	using System.Reflection;
	using System.Windows.Input;
    using Core;
    using Core.Reflection;
    using ViewModels;

    public class NonGenericCommandFactory : IFactory<ICommand>
	{
		private const BindingFlags NonPublic = BindingFlags.Instance | BindingFlags.NonPublic;

		private static readonly MethodInfo createActionMethod =
			typeof(NonGenericCommandFactory).GetMethod(nameof(GiveAction), NonPublic);

		private static readonly MethodInfo createCanExecuteMethod =
			typeof(NonGenericCommandFactory).GetMethod(nameof(GiveCanExecute), NonPublic);

		private readonly IViewModel viewModel;

		private readonly CommandMethods methods;

		private readonly ICommandFactory commandFactory;

		public NonGenericCommandFactory(
			ICommandFactory commandFactory,
			IViewModel viewModel,
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
				command = CreateCommand(viewModel, action);
			}
			else
			{
				var canExecute = CreateCanExecute();

				command = CreateCommand(viewModel, action, canExecute);
			}

			return command;
		}

		private ICommand CreateCommand(params object[] args)
		{
			var createMethodName = methods.ActionMethod.IsAsync ?
				nameof(ICommandFactory.CreateAsync) :
				nameof(ICommandFactory.Create);

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