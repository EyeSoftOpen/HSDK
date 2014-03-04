namespace EyeSoft.Test
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class TypeExtensionsRootTypeTest
	{
		private interface ITest
		{
		}

		[TestMethod]
		public void CheckRootTypeOfBaseClass()
		{
			typeof(Person).RootType().Should().Be.EqualTo<Person>();
		}

		[TestMethod]
		public void CheckRootTypeOfDerivedClass()
		{
			typeof(Teacher).RootType().Should().Be.EqualTo<Person>();
		}

		[TestMethod]
		public void CheckRootTypeOfValueType()
		{
			typeof(int).RootType().Should().Be.EqualTo<int>();
		}

		[TestMethod]
		public void CheckRootTypeOfObjectType()
		{
			typeof(object).RootType().Should().Be.EqualTo<object>();
		}

		[TestMethod]
		public void CheckRootTypeOfInterfaceype()
		{
			typeof(ITest).RootType().Should().Be.EqualTo<ITest>();
		}

		private class Person
		{
		}

		private class Teacher : Person
		{
		}
	}
}