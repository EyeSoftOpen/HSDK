namespace EyeSoft.ServiceLocator.Windsor
{
	using System;

	using Castle.MicroKernel.Registration;
    using EyeSoft;

    internal static class ComponentRegistrationExtensions
	{
		public static IRegisterLocator Include<TContract>(
			this ComponentRegistration<TContract> componentRegistration,
			WindsorDependencyContainer container,
			LifeStyle lifeStyle,
			string key)
			where TContract : class
		{
			switch (lifeStyle)
			{
				case LifeStyle.Transient:
					componentRegistration = componentRegistration.LifeStyle.Transient;
					break;
				case LifeStyle.Singleton:
					componentRegistration = componentRegistration.LifeStyle.Singleton;
					break;
				default:
					throw new ArgumentOutOfRangeException("lifeStyle");
			}

			if (key != null)
			{
				componentRegistration = componentRegistration.Named(key);
			}

			container.Container.Register(componentRegistration);

			return container;
		}
	}
}