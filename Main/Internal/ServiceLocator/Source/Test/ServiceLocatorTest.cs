namespace EyeSoft.ServiceLocator.Test
{
	using System.Collections;
	using System.Collections.Generic;
    using EyeSoft;
    using EyeSoft.ServiceLocator.Test.Helpers;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public abstract class ServiceLocatorTest
	{
		private ILocator locator;

		[TestInitialize]
		public virtual void Inizialize()
		{
			locator = CreateServiceLocator();

			locator
				.Singleton<ILogger, SimpleLogger>()
				.Singleton<ILogger, AdvancedLogger>();
		}

		[TestMethod]
		public virtual void GetInstance()
		{
			var instance = locator.Resolve<ILogger>();

			instance.Should("Instance should not be null").Not.Be.Null();
		}

		[TestMethod]
		public virtual void AskingForNotRegisteredComponentShouldRaiseComponentResolutionException()
		{
			Executing.This(() => locator.Resolve<IDictionary>())
				.Should().Throw<ComponentResolutionException>();
		}

		[TestMethod]
		public virtual void GetNamedInstance()
		{
			locator.Resolve<ILogger>(typeof(AdvancedLogger).FullName)
				.Should("Should be an AdvancedLogger.").Be.OfType<AdvancedLogger>();
		}

		[TestMethod]
		public virtual void GetNamedInstance2()
		{
			var instance = locator.Resolve<ILogger>(typeof(SimpleLogger).FullName);

			instance.Should("Should be a SimpleLogger.").Be.OfType<SimpleLogger>();
		}

		[TestMethod]
		public virtual void GetNamedInstanceWithEmptyName()
		{
			Executing.This(() => locator.Resolve<ILogger>(string.Empty))
				.Should().Throw<ComponentResolutionException>();
		}

		[TestMethod]
		public virtual void GetUnknownInstance2()
		{
			Executing.This(() => locator.Resolve<ILogger>("test"))
				.Should().Throw<ComponentResolutionException>();
		}

		[TestMethod]
		public virtual void GetAllInstances()
		{
			var instances = locator.ResolveAll<ILogger>();
			var list = new List<ILogger>(instances);

			list.Count.Should().Be.EqualTo(2);
		}

		[TestMethod]
		public virtual void GetlAllInstanceForUnknownTypeReturnEmptyEnumerable()
		{
			var instances = locator.ResolveAll<IDictionary>();
			var list = new List<IDictionary>(instances);

			list.Count.Should().Be.EqualTo(0);
		}

		[TestMethod]
		public virtual void GenericOverloadGetInstance()
		{
			locator.Resolve<ILogger>().GetType()
				.Should("Should get the same type.")
				.Be.EqualTo(locator.Resolve(typeof(ILogger)).GetType());
		}

		[TestMethod]
		public virtual void GenericOverloadGetInstanceWithName()
		{
			locator.Resolve<ILogger>().GetType()
				.Should("Should get the same type.")
				.Be.EqualTo(locator.Resolve(typeof(ILogger), typeof(SimpleLogger).FullName).GetType());
		}

		[TestMethod]
		public virtual void OverloadGetInstanceNoNameAndNullName()
		{
			Executing.This(() => locator.Resolve<ILogger>((string)null))
				.Should("Should get the same type.").Throw<ComponentResolutionException>();
		}

		[TestMethod]
		public virtual void GenericOverloadGetAllInstances()
		{
			var genericLoggers = new List<ILogger>(locator.ResolveAll<ILogger>());
			var plainLoggers = new List<object>(locator.ResolveAll(typeof(ILogger)));

			genericLoggers.Count.Should().Be.EqualTo(plainLoggers.Count);

			for (var i = 0; i < genericLoggers.Count; i++)
			{
				genericLoggers[i].GetType()
					.Should("Instances (" + i + ") should give the same type.")
					.Be.EqualTo(plainLoggers[i].GetType());
			}
		}

		protected abstract ILocator CreateServiceLocator();
	}
}