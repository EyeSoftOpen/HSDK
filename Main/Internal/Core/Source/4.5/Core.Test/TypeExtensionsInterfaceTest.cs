namespace EyeSoft.Core.Test
{
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SharpTestsEx;

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
				.Should().Be.True();
		}

		[TestMethod]
		public void CheckTypeImplementsGenericDefinitionInterface()
		{
			typeof(TypeThatImplementsInterface).Implements(typeof(IInterface<>))
				.Should().Be.True();
		}

		[TestMethod]
		public void CheckTypeDoesNotImplementsOtherGenericDefinitionInterface()
		{
			typeof(TypeThatImplementsInterface).Implements(typeof(IInterface<int>))
				.Should().Be.False();
		}

		[TestMethod]
		public void CheckTypeDoesNotImplementsInterface()
		{
			typeof(TypeThatImplementsInterface).Implements(typeof(INotImplementedInterface<string>))
				.Should().Be.False();
		}

		[TestMethod]
		public void CheckTypeDoesNotImplementsGenericDefinitionInterface()
		{
			typeof(TypeThatImplementsInterface).Implements(typeof(INotImplementedInterface<>))
				.Should().Be.False();
		}

		private class TypeThatImplementsInterface : IInterface<string>
		{
		}
	}
}
