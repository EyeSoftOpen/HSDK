namespace EyeSoft.ServiceLocator
{
	using System;
	using System.Linq.Expressions;

	using EyeSoft.Linq.Expressions;

	public static class DepdencyContainerExtensions
	{
		public static object GetInstance(
			this IDependencyContainer dependencyContainer,
			Type serviceType,
			params Expression<Func<object, object>>[] parameters)
		{
			var parameterDictionary = parameters.ToDictionary();

			return dependencyContainer.Resolve(serviceType, parameterDictionary);
		}

		public static T GetInstance<T>(this IDependencyContainer dependencyContainer, params Expression<Func<object, object>>[] parameters)
		{
			var parameterDictionary = parameters.ToDictionary();

			return dependencyContainer.Resolve<T>(parameterDictionary);
		}
	}
}