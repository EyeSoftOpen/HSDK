namespace EyeSoft.Core.Test
{
    using System;
    using Extensions;
    using Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class ObjectExtensionsTest
	{
		[TestMethod]
		public void IsNullOnNullValueReturnsTrue()
		{
			object obj = null;

			obj.IsNull().Should().BeTrue();
		}

		[TestMethod]
		public void UpCastToAnotherType()
		{
			3.Convert<double>()
				.Should().Be(3d);
		}

		[TestMethod]
		public void DownCastToAnotherType()
		{
			3d.Convert<int>()
				.Should().Be(3);
		}

		[TestMethod]
		public void ConvertFromString()
		{
			"3".Convert<double>()
				.Should().Be(3d);
		}

		[TestMethod]
		public void ConvertBaseTypeToDerivedTypeExpectedException()
        {
            Action action = () => new Person().Convert<Teacher>();
            action.Should().Throw<InvalidCastException>();
		}

		[TestMethod]
		public void ConvertDerivedTypeToBaseType()
		{
            Action action = () => new Teacher().Convert<Person>();
			action.Should().NotThrow();
		}

		[TestMethod]
		public void ConvertToString()
		{
			3d.Convert<string>()
				.Should().Be("3");
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

			actionCalled.Should().Be(expected);
		}
	}
}