namespace EyeSoft.Windows.Model.Input
{
	using System.ComponentModel;
	using System.Linq;
	using System.Windows.Input;

	using EyeSoft.Extensions;

	internal class CommandBuilder
	{
		private readonly ICommandFactory commandFactory;

		public CommandBuilder(ICommandFactory commandFactory)
		{
			this.commandFactory = commandFactory;
		}

		public ICommand Create(INotifyPropertyChanged viewModel, CommandMethods methods)
		{
			if (methods.HasErrors)
			{
				return null;
			}

			var parameter = methods.ActionMethod.MethodInfo.GetParameters().SingleOrDefault();

			var factory =
				parameter.IsNull() ?
					new NonGenericCommandFactory(commandFactory, viewModel, methods) :
					(IFactory<ICommand>)new GenericCommandFactory(commandFactory, viewModel, methods, parameter.ParameterType);

			return factory.Create();
		}
	}
}