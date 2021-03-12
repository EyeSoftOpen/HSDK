namespace EyeSoft.Core.Test
{
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class TypeExtensionsRootTypeTest
	{
		private interface ITest
		{
		}

		[TestMethod]
		public void CheckRootTypeOfBaseClass()
		{
			typeof(Person).RootType().Should().BeOfType<Person>();
		}

		[TestMethod]
		public void CheckRootTypeOfDerivedClass()
		{
			typeof(Teacher).RootType().Should().BeOfType<Person>();
		}

		[TestMethod]
		public void CheckRootTypeOfValueType()
		{
			typeof(int).RootType().Should().BeOfType<int>();
		}

		[TestMethod]
		public void CheckRootTypeOfObjectType()
		{
			typeof(object).RootType().Should().BeOfType<object>();
		}

		[TestMethod]
		public void CheckRootTypeOfInterfaceype()
		{
			typeof(ITest).RootType().Should().BeOfType<ITest>();
		}

		private class Person
		{
		}

		private class Teacher : Person
		{
		}
	}
}