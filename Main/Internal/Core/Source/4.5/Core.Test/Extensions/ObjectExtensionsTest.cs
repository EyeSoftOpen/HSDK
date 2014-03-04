namespace EyeSoft.Test
{
	using System;

	using EyeSoft.Extensions;
	using EyeSoft.Test.Helpers;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class ObjectExtensionsTest
	{
		[TestMethod]
		public void IsNullOnNullValueReturnsTrue()
		{
			object obj = null;

			obj.IsNull().Should().Be.True();
		}

		[TestMethod]
		public void UpCastToAnotherType()
		{
			3.Convert<double>()
				.Should().Be.EqualTo(3d);
		}

		[TestMethod]
		public void DownCastToAnotherType()
		{
			3d.Convert<int>()
				.Should().Be.EqualTo(3);
		}

		[TestMethod]
		public void ConvertFromString()
		{
			"3".Convert<double>()
				.Should().Be.EqualTo(3d);
		}

		[TestMethod]
		public void ConvertBaseTypeToDerivedTypeExpectedException()
		{
			Executing
				.This(() => new Person().Convert<Teacher>())
				.Should().Throw<InvalidCastException>();
		}

		[TestMethod]
		public void ConvertDerivedTypeToBaseType()
		{
			Executing
				.This(() => new Teacher().Convert<Person>())
				.Should().NotThrow();
		}

		[TestMethod]
		public void ConvertToString()
		{
			3d.Convert<string>()
				.Should().Be.EqualTo("3");
		}

		[TestMethod]
		public void OnNullValueActionMustNotBeCall()
		{
			CheckActionIsCalled(null, false);
		}

		[TestMethod]
		public void OnNullValueActionMustBeCall()
		{
			CheckActionIsCalled(new object(), true);
		}

		private static void CheckActionIsCalled(object obj, bool expected)
		{
			var actionCalled = false;

			obj.Extend().OnNotDefault(action => actionCalled = true);

			actionCalled.Should().Be.EqualTo(expected);
		}
	}
}