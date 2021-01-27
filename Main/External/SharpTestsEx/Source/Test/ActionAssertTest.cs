namespace SharpTestsEx.Test
{
    using System;
    using NUnit.Framework;

    public class ActionAssertTest
	{
		[Test]
		public void CatchException()
		{
			Executing.This(() => { throw new Exception("MyMessage"); }).Should().Throw<Exception>().And.Exception
				.Satisfies(ex => ex != null && ex.Message == "MyMessage");
		}

		[Test]
		public void CatchExactException()
		{
			Executing.This(() => Executing.This(() => { throw new Exception("arg"); }).Should().Throw<ArgumentNullException>()).
				Should().Throw<AssertException>();
		}

		[Test]
		public void FailureForNoException()
		{
			Executing.This(() => Executing.This(() => { }).Should().Throw<ArgumentNullException>()).
				Should().Throw<AssertException>();
		}

		[Test]
		public void FailureWithCustomMessage()
		{
			try
			{
				Executing.This(() => { }).Should("MyMess").Throw<ArgumentNullException>();
			}
			catch (AssertException afe)
			{
				StringAssert.Contains("MyMess", afe.Message);
			}
		}

		[Test]
		public void NotThrowException()
		{
			try
			{
				Executing.This(() => { throw new Exception(); }).Should().NotThrow();
			}
			catch (AssertException afe)
			{
				StringAssert.Contains("Not throw", afe.Message);
				StringAssert.Contains(typeof(Exception).FullName, afe.Message);
			}
		}

		[Test]
		public void NotThrowExceptionCustomMessage()
		{
			try
			{
				Executing.This(() => { throw new Exception(); }).Should("MyMessage").NotThrow();
			}
			catch (AssertException afe)
			{
				StringAssert.Contains("Not throw", afe.Message);
				StringAssert.Contains(typeof(Exception).FullName, afe.Message);
				StringAssert.Contains("MyMessage", afe.Message);
			}
		}

		[Test]
		public void NotThrowWithoutException()
		{
			Executing.This(() => { }).Should().NotThrow(); // should work
		}

		[Test]
		public void CatchExceptionWithoutSpecificType()
		{
			Executing.This(() => { throw new Exception("MyMessage"); }).Should().Throw().And.Exception.Satisfies(
				ex => ex != null && ex.Message == "MyMessage");
		}

		[Test]
		public void FailureForNoExceptionWithoutSpecificType()
		{
			Executing.This(() => Executing.This(() => { }).Should().Throw()).Should().Throw<AssertException>();
		}

		[Test]
		public void FailureNoExceptionWithoutSpecificTypeWithCustomMessage()
		{
			try
			{
				Executing.This(() => { }).Should("MyMess").Throw();
			}
			catch (AssertException afe)
			{
				StringAssert.Contains("MyMess", afe.Message);
			}
		}

		[Test]
		public void NotThrowMessageContainsOriginalExceptionMessage()
		{
			try
			{
				Executing.This(() => { throw new Exception("An exception was throws."); }).Should("MyMessage").NotThrow();
			}
			catch (AssertException afe)
			{
				StringAssert.Contains("Not throw", afe.Message);
				StringAssert.Contains(typeof(Exception).FullName, afe.Message);
				StringAssert.Contains("MyMessage", afe.Message);
				StringAssert.Contains("An exception was throws.", afe.Message);
			}
		}
	}
}