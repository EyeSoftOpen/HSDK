namespace EyeSoft.Windows.Model.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using Core;
    using Core.Extensions;

    public class ViewModelFactory
	{
		private readonly IResolverLocator container;

		public ViewModelFactory()
		{
		}

		public ViewModelFactory(IResolverLocator container)
		{
			this.container = container;
		}

		public static TViewModel Create<TViewModel>() where TViewModel : ViewModel, new()
		{
			var viewModel = new TViewModel();

			return viewModel;
		}

		public static TViewModel CreateWithParameters<TViewModel>(params object[] arguments) where TViewModel : ViewModel
		{
			return (TViewModel)CreateViewModelWithParameters(typeof(TViewModel), arguments);
		}

		public static ViewModel CreateWithParameters(Type viewModelType, params object[] arguments)
		{
			return CreateViewModelWithParameters(viewModelType, arguments);
		}

		public Window AssignViewModelUsingReflectionOrLocator(
			ViewModel viewModel,
			Type viewModelType,
			Type viewType,
			IEnumerable<object> arguments)
		{
			var window = Activator.CreateInstance(viewType).Convert<Window>();

			if (viewModel != null)
			{
				window.DataContext = viewModel;

				return window;
			}

			var enumerableArguments = arguments?.ToArray();

			viewModel = CreateWithLocatorOrWithReflection(viewModelType, enumerableArguments);
			window.DataContext = viewModel;

			return window;
		}

		public ViewModel CreateWithLocatorOrWithReflection(Type viewModelType, params object[] arguments)
		{
			ViewModel viewModel;

			var argumentsList = arguments ?? Enumerable.Empty<object>().ToArray();

			if (container == null)
			{
				viewModel = viewModelType.CreateInstanceBuilder(argumentsList).Create<ViewModel>();

				return viewModel;
			}

			if (container.IsRegistered(viewModelType))
			{
				var argumentTypes = argumentsList.Select(x => x.GetType()).ToArray();
				var constructorMatchingWithParameters = viewModelType.ConstructorContainsParameters(argumentTypes);
				viewModel = CreateViewModelUsingLocator(viewModelType, argumentsList, constructorMatchingWithParameters);

				return viewModel;
			}

			viewModel = viewModelType.CreateInstanceBuilder(arguments).Create<ViewModel>();

			return viewModel;
		}

		private static ViewModel CreateViewModelWithParameters(Type viewModelType, params object[] arguments)
		{
			if (!viewModelType.IsSubclassOf(typeof(ViewModel)))
			{
				var message =
					$"The specified view model type '{viewModelType.Name}' must inheriths from the ViewModel class.";

				throw new ArgumentException(message);
			}

			var instanceBuilder = viewModelType.CreateInstanceBuilder(arguments);
			var instance = (ViewModel)instanceBuilder.Create();

			return instance;
		}

		private ViewModel CreateViewModelUsingLocator(
			Type viewModelType,
			IList<object> arguments,
			ConstructorInfo constructorMatchingWithParameters)
		{
			var constructorParameters = constructorMatchingWithParameters.GetParameters();
			var argumentsWithNameAndValue = GetArgumentsWithNameAndValue(arguments, constructorParameters);

			var viewModel = (ViewModel)container.Resolve(viewModelType, argumentsWithNameAndValue);

			return viewModel;
		}

		private Dictionary<string, object> GetArgumentsWithNameAndValue(
			IList<object> arguments,
			IEnumerable<ParameterInfo> constructorParameters)
		{
			var argumentsWithNameAndValue = new Dictionary<string, object>();

			if (arguments.Count == 0)
			{
				return argumentsWithNameAndValue;
			}

			var argumentIndex = 0;

			foreach (var constructorParameter in constructorParameters)
			{
				if (constructorParameter.ParameterType != arguments[argumentIndex].GetType())
				{
					if (argumentIndex != 0)
					{
						throw new ArgumentException("The runtime arguments must after all dependencies.");
					}

					continue;
				}

				argumentsWithNameAndValue.Add(constructorParameter.Name, arguments[argumentIndex]);
				argumentIndex++;
			}

			return argumentsWithNameAndValue;
		}
	}
}