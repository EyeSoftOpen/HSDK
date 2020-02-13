namespace EyeSoft.Windows.Model.Input
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	public class CommandConvention : ICommandConvention
	{
		public CommandMethods Get(Type viewModelType, PropertyInfo commandProperty)
		{
			var actionMethod =
				GetMethod(
					viewModelType,
					GetActionName(commandProperty),
					typeof(void),
					false);

			var canExecuteMethod =
				GetMethod(
					viewModelType,
					GetCanExecuteName(commandProperty),
					typeof(bool),
					true);

			var canExecuteErrors =
				(canExecuteMethod == null) ?
					Enumerable.Empty<string>() :
					canExecuteMethod.Errors;

			var allErrors =
				actionMethod
					.Errors.Union(canExecuteErrors)
					.Where(x => x != null)
					.ToList();

			if (allErrors.Any())
			{
				allErrors.Insert(0, $"- command {commandProperty.Name}");
			}

			return new CommandMethods(actionMethod, canExecuteMethod, allErrors);
		}

		protected virtual string GetActionName(PropertyInfo commandProperty)
		{
			return commandProperty.Name.Replace("Command", null);
		}

		protected virtual string GetCanExecuteName(PropertyInfo commandProperty)
		{
			return string.Concat("Can", commandProperty.Name.Replace("Command", null));
		}

		protected virtual CommandMethod GetMethod(
			Type viewModelType,
			string actionName,
			Type returnType,
			bool isCanExecuteMethod)
		{
			var async = false;

			var errors = new List<string>();

			var syncActionMethodName = string.Concat("Sync", actionName);

			var actionMethod = GetMethod(viewModelType, syncActionMethodName);

			if (actionMethod == null)
			{
				actionMethod = GetMethod(viewModelType, actionName);
				async = true;
			}

			if ((actionMethod == null) && (!isCanExecuteMethod))
			{
			    var message = $" - missing action: looking for {actionName} or {syncActionMethodName} method";

				errors.Add(message);
			}

			if ((actionMethod == null) && isCanExecuteMethod)
			{
				return null;
			}

			var returnTypeError = CheckReturnType(actionMethod, returnType);

			if (returnTypeError != null)
			{
				errors.Add(returnTypeError);
			}

			var errorCollection =
				errors.Any() ?
					errors :
					Enumerable.Empty<string>();

			return new CommandMethod(actionMethod, async, errorCollection);
		}

		private string CheckReturnType(MethodInfo method, Type returnType)
		{
			if (method == null)
			{
				return null;
			}

			if (method.ReturnType == returnType)
			{
				return null;
			}

			var error =
                $" - wrong return type: the method {method.Name} must be of type {returnType.FullName}";

			return error;
		}

		private MethodInfo GetMethod(IReflect viewModelType, string actionName)
		{
			var methodInfo =
				viewModelType
					.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
					.SingleOrDefault(
						method =>
							method.Name == actionName &&
							method.GetParameters().Count() <= 1);

			return methodInfo;
		}
	}
}