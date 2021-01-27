namespace EyeSoft.Windows.Model.Input
{
    using System;
    using System.Linq;
	using System.Windows.Input;
    using Core;
    using Core.Extensions;
    using EyeSoft;
    using ViewModels;

    public class CommandBuilder : ICommandBuilder
	{
		private readonly Func<IViewModel, CommandMethods, IFactory<ICommand>> nonGenericCommandFactory;
		private readonly Func<IViewModel, CommandMethods, Type, IFactory<ICommand>> genericCommandFactory;

		public CommandBuilder(
			ICommandFactory commandFactory,
			Func<IViewModel, CommandMethods, IFactory<ICommand>> nonGenericCommandFactory = null,
			Func<IViewModel, CommandMethods, Type, IFactory<ICommand>> genericCommandFactory = null)
		{
			IFactory<ICommand> defaultNonGeneric(IViewModel viewModel, CommandMethods methods) =>
				new NonGenericCommandFactory(commandFactory, viewModel, methods);

			IFactory<ICommand> defaultGeneric(IViewModel viewModel, CommandMethods methods, Type parameterType) =>
				new GenericCommandFactory(commandFactory, viewModel, methods, parameterType);

			this.nonGenericCommandFactory = nonGenericCommandFactory ?? defaultNonGeneric;

			this.genericCommandFactory = genericCommandFactory ?? defaultGeneric;
		}

		public ICommand Create(IViewModel viewModel, CommandMethods methods)
		{
			if (methods.HasErrors)
			{
				return null;
			}

			var parameter = methods.ActionMethod.MethodInfo.GetParameters().SingleOrDefault();

			IFactory<ICommand> factory;

			if (parameter.IsNull())
			{
				factory = nonGenericCommandFactory(viewModel, methods);
			}
			else
			{
				factory = genericCommandFactory(viewModel, methods, parameter.ParameterType);
			}

			return factory.Create();
		}
	}
}