namespace EyeSoft.Core.Test
{
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class TypeExtensionsInterfaceTest
	{
		private interface IInterface<T>
		{
		}

		private interface INotImplementedInterface<T>
		{
		}

		[TestMethod]
		public void CheckTypeImplementsInterface()
		{
			typeof(TypeThatImplementsInterface).Implements(typeof(IInterface<string>))
				.Should().BeTrue();
		}

		[TestMethod]
		public void CheckTypeImplementsGenericDefinitionInterface()
		{
			typeof(TypeThatImplementsInterface).Implements(typeof(IInterface<>))
				.Should().BeTrue();
		}

		[TestMethod]
		public void CheckTypeDoesNotImplementsOtherGenericDefinitionInterface()
		{
			typeof(TypeThatImplementsInterface).Implements(typeof(IInterface<int>))
				.Should().BeFalse();
		}

		[TestMethod]
		public void CheckTypeDoesNotImplementsInterface()
		{
			typeof(TypeThatImplementsInterface).Implements(typeof(INotImplementedInterface<string>))
				.Should().BeFalse();
		}

		[TestMethod]
		public void CheckTypeDoesNotImplementsGenericDefinitionInterface()
		{
			typeof(TypeThatImplementsInterface).Implements(typeof(INotImplementedInterface<>))
				.Should().BeFalse();
		}

		private class TypeThatImplementsInterface : IInterface<string>
		{
		}
	}
}
