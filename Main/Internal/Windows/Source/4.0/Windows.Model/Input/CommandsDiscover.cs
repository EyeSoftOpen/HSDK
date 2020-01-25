namespace EyeSoft.Windows.Model.Input
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Reflection;
	using System.Windows.Input;

	using EyeSoft.Reflection;

	public class CommandsDiscover
	{
		private readonly ICommandFactory commandFactory;

		private readonly CommandConvention commandConvention;

		public CommandsDiscover() : this(new CommandFactory(), new CommandConvention())
		{
		}

		public CommandsDiscover(ICommandFactory commandFactory) : this(commandFactory, new CommandConvention())
		{
		}

		public CommandsDiscover(CommandConvention commandConvention) : this(new CommandFactory(), commandConvention)
		{
		}

		public CommandsDiscover(ICommandFactory commandFactory, CommandConvention commandConvention)
		{
			this.commandFactory = commandFactory;
			this.commandConvention = commandConvention;
		}

		public CommandConvention CommandConvention
		{
			get { return commandConvention; }
		}

		internal ICommandFactory CommandFactory
		{
			get { return commandFactory; }
		}

		public void Discover(INotifyPropertyChanged viewModel)
		{
			AssignAllCommands(viewModel);
		}

		private void AssignAllCommands(INotifyPropertyChanged viewModel)
		{
			var viewModelType = viewModel.GetType();

			var commands =
				viewModelType
					.GetProperties(BindingFlags.Instance | BindingFlags.Public)
					.Where(property =>
						property.PropertyType.EqualsOrSubclassOf<ICommand>() &&
						property.GetValue(viewModel, null) == null)
					.ToList();

			var errors = commands.SelectMany(property => AssignCommand(viewModel, property)).ToList();

			if (!errors.Any())
			{
				return;
			}

			errors.Insert(0, $"Issues found on type {viewModel.GetType().Name}:");
			var message = errors.JoinMultiLine();

			throw new InvalidOperationException(message);
		}

		private IEnumerable<string> AssignCommand(
			INotifyPropertyChanged viewModel,
			PropertyInfo commandProperty)
		{
			var methods = commandConvention.Get(commandProperty.ReflectedType, commandProperty);

			var command = new CommandBuilder(commandFactory).Create(viewModel, methods);

			if (command == null)
			{
				return methods.Errors;
			}

			if (commandProperty.DeclaringType != commandProperty.ReflectedType)
			{
				commandProperty = commandProperty.DeclaringType.GetProperty(commandProperty.Name);
			}

			commandProperty.SetValue(viewModel, command);

			return methods.Errors;
		}
	}
}