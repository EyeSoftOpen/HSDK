using NUnit.Framework;

namespace SharpTestsEx.Tests
{
	
	public class BoleanConstraintsTest
	{
		[Test]
		public void ShouldWork()
		{
			true.Should().Be.True();
			false.Should().Be.False();
		}

		[Test]
		public void WrongAssertionOnTrue()
		{
			Executing.This(
				() => false.Should().Be.True()).Should().Throw<AssertException>();
		}

		[Test]
		public void WrongAssertionOnFalse()
		{
			Executing.This(
				() => true.Should().Be.False()).Should().Throw<AssertException>();
		}

		[Test]
		public void ShouldWorkUsingCustomMessage()
		{
			const bool started = true;
			var title = "The UoW should be started";
			try
			{
				(!started).Should(title).Be.True();
			}
			catch (AssertException ae)
			{
				StringAssert.Contains(title, ae.Message);
			}
		}

		[Test]
		public void FailureMessageIsOk()
		{
			Executing.This(() => true.Should().Be.False()).Should().Throw<AssertException>().And.ValueOf
				.Message.ToLower().Should().Be.EqualTo("true should be false.");
			Executing.This(() => false.Should().Be.True()).Should().Throw<AssertException>().And.ValueOf
				.Message.ToLower().Should().Be.EqualTo("false should be true.");
		}

		[Test]
		public void EqualToVariable()
		{
			var variable = true;
			true.Should().Be.EqualTo(variable);
			variable = false;
			false.Should().Be.EqualTo(variable);
		}

		[Test]
		public void BeShortcut()
		{
			var variable = true;
			true.Should().Be(variable);
			Executing.This(() => true.Should().Be(!variable)).Should().Throw<AssertException>();
		}
	}
}