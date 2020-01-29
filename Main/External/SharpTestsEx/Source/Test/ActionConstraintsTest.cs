using System;
using NUnit.Framework;

namespace SharpTestsEx.Tests
{
	
	public class ActionConstraintsTest
	{
		public class AClass
		{
			public AClass(object obj)
			{
				if (obj == null)
				{
					throw new ArgumentNullException("obj");
				}
			}
		}

		public class BClass
		{
			public BClass(object obj) { }
		}

		[Test]
		public void ShouldWork()
		{
			(new Action(() => new AClass(null)))
				.Should().Throw<ArgumentNullException>()
				.And.
				ValueOf.ParamName
					.Should().Be.EqualTo("obj");

			Executing.This(() => new AClass(null)).Should().Throw<ArgumentNullException>()
				.And.ValueOf
					.ParamName.Should().Be("obj");
		}

		[Test]
		public void ShouldWorkUsingCustomMessage()
		{
			const string title = "The ctor does not have parameter protection.";
			try
			{
				(new Action(() => new BClass(null))).Should(title).Throw<ArgumentNullException>();
			}
			catch (AssertException ae)
			{
				StringAssert.Contains(title, ae.Message);
			}
		}

		[Test]
		public void NotThrow()
		{
			(new Action(() => new object())).Should().NotThrow();
		}

		[Test]
		public void NotThrowShouldWorkUsingCustomMessage()
		{
			const string title = "The ctor has parameter protection.";
			try
			{
				(new Action(() => new AClass(null))).Should(title).NotThrow();
			}
			catch (AssertException ae)
			{
				StringAssert.Contains(title, ae.Message);
			}
		}

		[Test]
		public void ShouldWorkForNoSpecificType()
		{
			(new Action(() => new AClass(null)))
				.Should().Throw()
				.And.Exception.Should().Be.InstanceOf<ArgumentException>();

			Executing.This(() => new AClass(null)).Should().Throw();
		}

		[Test]
		public void ShouldFailsForNoException()
		{
			Executing.This(() => Executing.This(() => { }).Should().Throw()).Should().Throw<AssertException>();
		}
	}
}