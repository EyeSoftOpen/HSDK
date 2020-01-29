using System;
using NUnit.Framework;

namespace SharpTestsEx.Tests
{
	public class MyTypeConstraintsAttribute : Attribute {}

	public class TypeConstraintsTest
	{
		private class B { }

		[MyTypeConstraints]
		private class D1 : B { }

		private interface ID2 { }
		private class D2 : D1, ID2 { }

		[Test]
		public void SubClassOf()
		{
			typeof(D1).Should().Be.SubClassOf<B>();
			typeof(D1).Should().Not.Be.SubClassOf<D2>();
		}

		[Test]
		public void ShouldWorkUsingCustomMessage()
		{
			var title = "My message";
			try
			{
				typeof(D1).Should(title).Be.SubClassOf<D2>();
			}
			catch (AssertException ae)
			{
				ae.Message.Contains(title);
			}
		}

		[Test]
		public void AssignableFrom()
		{
			typeof(D1).Should().Be.AssignableFrom<D2>();
			typeof(D1).Should().Not.Be.AssignableFrom<B>();
		}

		[Test]
		public void EqualTo()
		{
			typeof(D1).Should().Be.EqualTo<D1>();
			typeof(D1).Should().Not.Be.EqualTo<B>();
		}

		[Test]
		public void Null()
		{
			Type t = null;
			t.Should().Be.Null();
			typeof(D1).Should().Not.Be.Null();
		}

		[Test]
		public void Attribute()
		{
			typeof(D1).Should().Have.Attribute<MyTypeConstraintsAttribute>();
			typeof(B).Should().Not.Have.Attribute<MyTypeConstraintsAttribute>();
		}

		[Test]
		public void WhenTypeIsNullThenShouldFail()
		{
			Type type = null;
			type.Executing(a => a.Should().Have.Attribute<MyTypeConstraintsAttribute>()).Throws<AssertException>();
		}

		[Test]
		public void AssignableTo()
		{
			typeof(D2).Should().Be.AssignableTo<ID2>();

			Executing.This(() => typeof(D1).Should().Be.AssignableTo<ID2>()).Should().Throw<AssertException>().And.ValueOf
				.Message.Should().Contain("Assignable To");

			typeof(D1).Should().Not.Be.AssignableTo<ID2>();
		}

		[Test]
		public void BeShortcut()
		{
			typeof(D1).Should().Be<D1>();
			Executing.This(() => typeof(D1).Should().Not.Be<D1>()).Should().Throw<AssertException>();
			typeof(D1).Should().Be(typeof(D1));
			Executing.This(() => typeof(D1).Should().Not.Be(typeof(D1))).Should().Throw<AssertException>();

			typeof(D1).Should().Not.Be<B>();
			Executing.This(() => typeof(D1).Should().Be<B>()).Should().Throw<AssertException>();
			typeof(D1).Should().Not.Be(typeof(B));
			Executing.This(() => typeof(D1).Should().Be(typeof(B))).Should().Throw<AssertException>();
		}
	}
}