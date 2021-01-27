namespace SharpTestsEx.Test
{
    using NUnit.Framework;

    public class ClassConstraintsFixture
	{
		[Test]
		public void ShouldWork()
		{
			object something = null;
			something.Should().Not.Be.EqualTo(new object());
			something.Should().Be.Null();
			something = new object();
			something.Should().Be.EqualTo(something);
			something.Should().Not.Be.Null();
		}

		[Test]
		public void ShouldWorkUsingCustomMessage()
		{
			string title = "An instance can't be null";
			try
			{
				(new object()).Should(title).Be.Null();
			}
			catch (AssertException ae)
			{
				StringAssert.Contains(title, ae.Message);
			}
		}

		[Test]
		public void ShouldWorkUsingCustomTitleWithConstraintChain()
		{
			string title = "An instance can't be null";
			try
			{
				(new object()).Should(title).Not.Be.Null().And.Be.OfType<object>().And.Be.Null();
			}
			catch (AssertException ae)
			{
				StringAssert.Contains(title, ae.Message);
			}
		}

		[Test]
		public void SameAs_ConstraintShouldWork()
		{
			var something = new object();
			something.Should().Be.SameInstanceAs(something);
			something.Should().Not.Be.SameInstanceAs(new object());
		}

		private class B
		{
			public int MyInt { get; set; }
		}

		private class D1 : B { }

		private class D2 : D1
		{
			public string StringProperty;
		}

		[Test]
		public void InstanceOfShouldWork()
		{
			(new D2()).Should().Be.InstanceOf<B>();
			(new B()).Should().Not.Be.InstanceOf<D2>();
		}

		[Test]
		public void InstanceOfAndChainShouldWork()
		{
			(new D2()).Should().Be.InstanceOf<B>().And.ValueOf.MyInt.Should().Be.EqualTo(0);
		}

		[Test]
		public void TypeOfShouldWork()
		{
			(new D2()).Should().Be.OfType<D2>();
			(new D2()).Should().Not.Be.OfType<D1>();
		}

		[Test]
		public void AssignableFromShouldWork()
		{
			(new D1()).Should().Be.AssignableFrom<D1>();
			(new D1()).Should().Be.AssignableFrom<D2>();
			(new D1()).Should().Not.Be.AssignableFrom<B>();
		}

		[Test]
		public void AssignableToShouldWork()
		{
			(new D2()).Should().Be.AssignableTo<D1>();
			(new D2()).Should().Be.AssignableTo<B>();
			(new D1()).Should().Not.Be.AssignableTo<D2>();
		}

		[Test]
		public void AndChainShouldWork()
		{
			(new D1()).Should().Be.AssignableFrom<D1>().And.Be.AssignableFrom<D2>();
		}

		[Test]
		public void AndChainShouldWorkWithDoubleNegation()
		{
			var something = new object();
			something.Should().Not.Be.Null().And.Not.Be.EqualTo(new object());
		}

		[Test]
		public void TypeOfChainShouldWork()
		{
			var instanceOfClass = new D2();

			instanceOfClass.Should()
				.Be.AssignableTo<D1>()
				.And.Be.OfType<D2>()
				.And.ValueOf.StringProperty
					.Should().Be.Null();

			instanceOfClass.StringProperty = "assigning to get rid of compiler warning about never being assigned to...";
		}

		[Test]
		public void TypeOfChainValueShouldWork()
		{
			var instanceOfClass = new D2();

			var value = instanceOfClass.Should()
				.Be.OfType<D2>()
				.And.Value;
			value.Should().Be.SameInstanceAs(instanceOfClass);
		}

		[Test]
		public void WellFormatMessageForNull()
		{
			Executing.This(() => (new object()).Should().Be.Null()).Should().Throw().And.ValueOf.Message.ToLowerInvariant().Should().Be.EqualTo("system.object should be null.");
		}

		[Test]
		public void BeShortCut()
		{
			object something = null;
			something.Should().Not.Be(new object());
			Executing.This(() => something.Should().Be(new object())).Should().Throw<AssertException>();
			something = new object();
			something.Should().Be(something);
			Executing.This(() => something.Should().Not.Be(something)).Should().Throw<AssertException>();
		}
	}
}