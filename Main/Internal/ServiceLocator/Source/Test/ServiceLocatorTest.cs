namespace EyeSoft.ServiceLocator.Test
{
    using System;
    using System.Collections;
	using System.Collections.Generic;
    using EyeSoft;
    using EyeSoft.ServiceLocator.Test.Helpers;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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

			instance.Should().NotBeNull("Instance should not be null");
		}

		[TestMethod]
		public virtual void AskingForNotRegisteredComponentShouldRaiseComponentResolutionException()
		{
            Action action = () => locator.Resolve<IDictionary>();

            action.Should().Throw<ComponentResolutionException>();
		}

		[TestMethod]
		public virtual void GetNamedInstance()
		{
			locator.Resolve<ILogger>(typeof(AdvancedLogger).FullName)
				.Should().BeOfType<AdvancedLogger>("Should be an AdvancedLogger.");
		}

		[TestMethod]
		public virtual void GetNamedInstance2()
		{
			var instance = locator.Resolve<ILogger>(typeof(SimpleLogger).FullName);

			instance.Should().BeOfType<SimpleLogger>("Should be a SimpleLogger.");
		}

		[TestMethod]
		public virtual void GetNamedInstanceWithEmptyName()
		{
            Action action = () => locator.Resolve<ILogger>(string.Empty);
            
            action.Should().Throw<ComponentResolutionException>();
		}

		[TestMethod]
		public virtual void GetUnknownInstance2()
		{
           Action action  = () => locator.Resolve<ILogger>("test");

           action.Should().Throw<ComponentResolutionException>();
		}

		[TestMethod]
		public virtual void GetAllInstances()
		{
			var instances = locator.ResolveAll<ILogger>();
			var list = new List<ILogger>(instances);

			list.Count.Should().Be(2);
		}

		[TestMethod]
		public virtual void GetlAllInstanceForUnknownTypeReturnEmptyEnumerable()
		{
			var instances = locator.ResolveAll<IDictionary>();
			var list = new List<IDictionary>(instances);

			list.Count.Should().Be(0);
		}

		[TestMethod]
		public virtual void GenericOverloadGetInstance()
		{
			locator.Resolve<ILogger>().GetType()
				.Should()
				.Be(locator.Resolve(typeof(ILogger)).GetType(), "Should get the same type.");
		}

		[TestMethod]
		public virtual void GenericOverloadGetInstanceWithName()
		{
			locator.Resolve<ILogger>().GetType()
				.Should()
				.Be(locator.Resolve(typeof(ILogger), typeof(SimpleLogger).FullName).GetType(), "Should get the same type.");
		}

		[TestMethod]
		public virtual void OverloadGetInstanceNoNameAndNullName()
		{
			Action action = () => locator.Resolve<ILogger>((string)null);
                
            action.Should().Throw<ComponentResolutionException>("Should get the same type.");
		}

		[TestMethod]
		public virtual void GenericOverloadGetAllInstances()
		{
			var genericLoggers = new List<ILogger>(locator.ResolveAll<ILogger>());
			var plainLoggers = new List<object>(locator.ResolveAll(typeof(ILogger)));

			genericLoggers.Count.Should().Be(plainLoggers.Count);

			for (var i = 0; i < genericLoggers.Count; i++)
			{
				genericLoggers[i].GetType()
					.Should()
					.Be(plainLoggers[i].GetType(), $"Instances ({i}) should give the same type.");
			}
		}

		protected abstract ILocator CreateServiceLocator();
	}
}