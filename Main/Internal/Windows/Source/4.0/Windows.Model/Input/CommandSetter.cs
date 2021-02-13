namespace EyeSoft.Windows.Model.Input
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Input;
    using Extensions;

    public class CommandSetter : ICommandSetter
	{
		private readonly ICommandConvention commandConvention;
		private readonly ICommandBuilder commandBuilder;

		public CommandSetter(
			ICommandConvention commandConvention,
			ICommandBuilder commandBuilder)
		{
			this.commandConvention = commandConvention;
			this.commandBuilder = commandBuilder;
		}

		public virtual void AssignAllCommands(IViewModel viewModel)
		{
			var viewModelType = viewModel.GetType();

			var commands =
				viewModelType
					.GetProperties(BindingFlags.Instance | BindingFlags.Public)
					.Where(property =>
						property.PropertyType.EqualsOrSubclassOf<ICommand>() &&
						property.GetValue(viewModel, null) == null)
					.ToArray();

			var errors = commands.SelectMany(property => AssignCommand(viewModel, property)).ToList();

			if (!errors.Any())
			{
				return;
			}

			errors.Insert(0, $"Issues found on type {viewModel.GetType().Name}:");
			var message = errors.JoinMultiLine();

			throw new InvalidOperationException(message);
		}

		protected virtual IEnumerable<string> AssignCommand(
			IViewModel viewModel,
			PropertyInfo commandProperty)
		{
            try
            {
                var methods = commandConvention.Get(commandProperty.ReflectedType, commandProperty);

                var command = commandBuilder.Create(viewModel, methods);

                if (command == null)
                {
                    return methods.Errors;
                }

                if (commandProperty.DeclaringType != commandProperty.ReflectedType)
                {
                    commandProperty = commandProperty?.DeclaringType?.GetProperty(commandProperty.Name);
                }

                commandProperty.SetValue(viewModel, command);

                return methods.Errors;
            }
            catch (Exception exception)
            {
                var message = $"Cannot assign the command {commandProperty?.Name} of viewmodel {viewModel.GetType().FullName}. Check the Set method exists.";
                
                throw new InvalidOperationException(message, exception);
            }
        }
	}
}