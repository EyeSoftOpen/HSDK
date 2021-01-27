namespace SharpTestsEx.Test
{
    using System;
    using NUnit.Framework;
    using SharpTestsEx.ExtensionsImpl;

    public class ExpressionActionConstraintsTest
	{
		private class MyClass
		{
			public void Method(string value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value","My message");
				}
			}
		}

		[Test]
		public void WhenCallMethodWithNoException_Throws_ShouldFail()
		{
			var actual = new MyClass();
			var constraint = new ExpressionActionConstraints<MyClass>(actual, mc => mc.Method("something"));
			try
			{
				constraint.Throws<ArgumentNullException>();
				Assert.Fail();
			}
			catch (AssertException e)
			{
				e.Message.Should("the failure message should contain the expression and the expected exception").Contain("mc.Method").And.Contain("ArgumentNullException");
			}
		}

		[Test]
		public void WhenCallMethodWithException_Throws_ShouldntFail()
		{
			var actual = new MyClass();
			var constraint = new ExpressionActionConstraints<MyClass>(actual, mc => mc.Method(null));
			Executing.This(() => constraint.Throws<ArgumentNullException>()).Should().NotThrow();
		}

		[Test]
		public void WhenCallMethodWithNoException_GenericThrows_ShouldFail()
		{
			var actual = new MyClass();
			var constraint = new ExpressionActionConstraints<MyClass>(actual, mc => mc.Method("something"));
			try
			{
				constraint.Throws();
				Assert.Fail();
			}
			catch (AssertException e)
			{
				e.Message.Should("the failure message should contain the expression").Contain("mc.Method");
			}
		}

		[Test]
		public void WhenCallMethodWithException_GenericThrows_ShouldntFail()
		{
			var actual = new MyClass();
			var constraint = new ExpressionActionConstraints<MyClass>(actual, mc => mc.Method(null));
			Executing.This(() => constraint.Throws()).Should().NotThrow();
		}

		[Test]
		public void WhenCallMethodWithException_NotThrows_ShouldFail()
		{
			var actual = new MyClass();
			var constraint = new ExpressionActionConstraints<MyClass>(actual, mc => mc.Method(null));
			try
			{
				constraint.NotThrows();
				Assert.Fail();
			}
			catch (AssertException e)
			{
				e.Message.Should("the failure message should contain the expression and the original message").Contain("mc.Method").And.Contain("My message");
			}
		}

		[Test]
		public void WhenCallMethodWithNoException_NotThrows_ShouldntFail()
		{
			var actual = new MyClass();
			var constraint = new ExpressionActionConstraints<MyClass>(actual, mc => mc.Method("something"));
			Executing.This(() => constraint.NotThrows()).Should().NotThrow();
		}

		[Test]
		public void UsingExtensions()
		{
			var actual = new MyClass();
			actual.Executing(a => a.Method(null)).Throws<ArgumentNullException>();
			actual.Executing(a => a.Method(null)).Throws();
			actual.Executing(a => a.Method("somethig")).NotThrows();
		}
	}
}