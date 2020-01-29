using System;
using NUnit.Framework;
using SharpTestsEx.Assertions;

namespace SharpTestsEx.Tests.Assertions
{
	
	public class AssertionTest
	{
		[Test]
		public void CtorWithNullMatch()
		{
			Executing.This(
				() => new Assertion<int, int>("", 0, null, new DefaultMessageBuilder<int, int>())).Should().Throw<ArgumentNullException>();
		}

		[Test]
		public void CtorWithNullBuilder()
		{
			Executing.This(
				() => new Assertion<int, int>("", 0, i => false, (IMessageBuilder<int, int>)null)).Should().Throw<ArgumentNullException>();
		}

		[Test]
		public void CtorWithNullMatchAndFunc()
		{
			Executing.This(
				() => new Assertion<int, int>("", 0, null, mi => "")).Should().Throw<ArgumentNullException>();
		}

		[Test]
		public void CtorWithNullFuncBuilder()
		{
			Executing.This(
				() => new Assertion<int, int>("", 0, i => false, (Func<MessageBuilderInfo<int, int>, string>)null)).Should().Throw<ArgumentNullException>();
		}

		[Test]
		public void CtorNotThrowsWithNullValues()
		{
			new Assertion<int, object>(null, null, i => false, new DefaultMessageBuilder<int, object>());
		}

		[Test]
		public void RigthAssertionDoesNotThrows()
		{
			var c = new Assertion<int, int>(null, 0, a => a > 0);
			c.Assert(5, null);
		}

		[Test]
		public void WrongAssertionDoesNotThrows()
		{
			// Checking even that the match-delegate is running
			var c = new Assertion<int, int>(null, 0, a => a > 0);
			c.Executing(x => x.Assert(-5, null)).Throws<AssertException>();
		}

		[Test]
		public void WrongAssertionDoesNotThrowsAndCreateMessage()
		{
			var c = new Assertion<int, int>("Be Greater than", 0, a => a > 0);
			try
			{
				c.Assert(-5, null);
			}
			catch (AssertException afe)
			{
				StringAssert.Contains("Be Greater than", afe.Message);
			}
		}

		[Test]
		public void NegationOk()
		{
			var negated = !(new Assertion<int, int>("", 0, a => a > 0));
			negated.Assert(-1, null);// should not Throws
		}

		[Test]
		public void NegationWrong()
		{
			var negated = !(new Assertion<int, int>("", 0, a => a > 0));
			negated.Executing(x => x.Assert(5, null)).Throws<AssertException>();
		}

		[Test]
		public void WithMessageDelegate()
		{
			var c = new Assertion<int, int>("Be Greater than", 0, a => a > 0, mi => "MyMessage");
			try
			{
				c.Assert(-5, null);
			}
			catch (AssertException afe)
			{
				Assert.AreEqual("MyMessage", afe.Message);
			}
		}

		[Test]
		public void OrOperator()
		{
			IAssertion<int> assertion = (new Assertion<int, int>("", 0, a => a > 6)) | (new Assertion<int, int>("", 0, a => a < -6));
			assertion.Executing(x=> x.Assert(-7, null)).NotThrows();
			assertion.Executing(x => x.Assert(7, null)).NotThrows();
			assertion.Executing(x => x.Assert(0, null)).Throws<AssertException>();
		}

		[Test]
		public void AndOperator()
		{
			IAssertion<int> assertion = (new Assertion<int, int>("", 0, a => a < 6)) & (new Assertion<int, int>("", 0, a => a > -6));
			assertion.Executing(x => x.Assert(-7, null)).Throws<AssertException>();
			assertion.Executing(x => x.Assert(7, null)).Throws<AssertException>();
			assertion.Executing(x => x.Assert(0, null)).NotThrows();
		}

		[Test]
		public void OrOperatorWithUnary()
		{
			var assertion = new SubsetOfAssertion<int>(new[] {1, 2, 3}) | new OrderedAscendingAssertion<int>();
			assertion.Executing(x => x.Assert(new[] { 1, 2 }, null)).NotThrows();
			assertion.Executing(x => x.Assert(new[] { 2, 1 }, null)).NotThrows();
			assertion.Executing(x => x.Assert(new[] { 5, 4 }, null)).Throws<AssertException>();
		}

		[Test]
		public void AndOperatorWithUnary()
		{
			var assertion = new SubsetOfAssertion<int>(new[] { 1, 2, 3 }) & new OrderedAscendingAssertion<int>();
			assertion.Executing(x => x.Assert(new[] { 1, 2 }, null)).NotThrows();
			assertion.Executing(x => x.Assert(new[] { 2, 1 }, null)).Throws<AssertException>();
		}
	}
}