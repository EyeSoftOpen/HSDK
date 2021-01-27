namespace SharpTestsEx.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;

    public class SatisfyFailureMessagesTest
	{
		[Test]
		public void ShouldContaingTheStringFailureMagnifier()
		{
			var expected = "something";

			var e = Executing.This(() => "someXhing".Satisfies(a => a == expected)).Should().Throw<AssertException>().Exception;
			var lines = e.Message.Lines();
			lines[1].Should().Be.EqualTo("Strings differ at position 5.");
			lines[2].Should().Be.EqualTo("someXhing");
			lines[3].Should().Be.EqualTo("something");
			lines[4].Should().Be.EqualTo("____^____");
		}

		[Test]
		public void ActualEnumerable()
		{
			var expected = 4;
			var e = Executing.This(() => new[] { 1, 2, 3 }.Satisfies(a => a.Contains(expected))).Should().Throw<AssertException>().Exception;
			e.Message.Should().Contain("[1, 2, 3]");
		}

		[Test]
		public void WhenStringIsSubValueOfTestedActual_ShouldContaingTheStringFailureMagnifier()
		{
			var strings = new[] { "pizza", "someXhing" };
			var expected = "something";

			var e = Executing.This(() => strings.Satisfies(a => a[1] == expected)).Should().Throw<AssertException>().Exception;
			var lines = e.Message.Lines();
			lines[1].Should().Be.EqualTo("Strings differ at position 5.");
			lines[2].Should().Be.EqualTo("someXhing");
			lines[3].Should().Be.EqualTo("something");
			lines[4].Should().Be.EqualTo("____^____");
		}

		[Test]
		public void WhenStringIsSubValueOfTestedActual_ShouldContaingTheStringFailureMagnifierForEachEquality()
		{
			var strings = new[] { "pizza", "someXhing" };

			var e = Executing.This(() => strings.Satisfies(a => a[0] == "puzza" && a[1] == "something")).Should().Throw<AssertException>().Exception;
			var lines = e.Message.Lines();
			lines.Where(l => l.StartsWith("Strings differ at position")).Should().Have.Count.EqualTo(2);
		}

		[Test]
		public void WhenStringIsSubValueOfTestedActual_ShouldContaingTheStringFailureMagnifierOnlyForDifference()
		{
			var strings = new[] { "pizza", "someXhing" };

			var e = Executing.This(() => strings.Satisfies(a => a[0] == "pizza" && a[1] == "something")).Should().Throw<AssertException>().Exception;
			var lines = e.Message.Lines();
			lines.Where(l => l.StartsWith("Strings differ at position")).Should().Have.Count.EqualTo(1);
		}

		[Test]
		public void MethodsCalls_WithNegation()
		{
			var e = Executing.This(() => "TH".Satisfies(a => !a.ToUpperInvariant().Contains("TH"))).Should().Throw<AssertException>().Exception;
			e.Message.Should().Contain("a => !(a.ToUpperInvariant().Contains(\"TH\"))");
		}

		[Test]
		public void ShouldContainTheSameSequenceFailureMagnifier()
		{
			Executing.This(() => new[] {1, 2, 3}.Satisfies(a => a.SequenceEqual(new[] {3, 2, 1}))).Should().Throw().And.ValueOf.
				Message.Should().Contain("Values differ");
		}

		private class FirstCharEqualityComparer: IEqualityComparer<string>
		{
			#region Implementation of IEqualityComparer<string>

			public bool Equals(string x, string y)
			{
				if(ReferenceEquals(x, null) && ReferenceEquals(y, null))
				{
					return true;
				}
				if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
				{
					return false;
				}
				if(string.Empty.Equals(x) && string.Empty.Equals(y))
				{
					return true;
				}
				if(string.Empty.Equals(x) || string.Empty.Equals(y))
				{
					return false;
				}
				return x[0] == y[0];
			}

			public int GetHashCode(string obj)
			{
				if (ReferenceEquals(obj, null))
				{
					return 0;
				}
				if (string.Empty.Equals(obj))
				{
					return string.Empty.GetHashCode();
				}
				return obj[0].GetHashCode();
			}

			#endregion
		}

		[Test]
		public void WhenUseCustomComparer_ShouldContainTheSameSequenceFailureMagnifier()
		{
			var firstCharEqualityComparer = new FirstCharEqualityComparer();
				Executing.This(
					() =>
					new[] {"1xx", "2xx", "3xx"}.Satisfies(a => a.SequenceEqual(new[] {"1yy", "2yy", "4xx"}, firstCharEqualityComparer))).
					Should().Throw().And.ValueOf.Message.Should().Contain("Values differ");
		}

		[Test]
		public void ArrayLengthNodeTypeShouldNotReturnEmptyString()
		{
			var e = Executing.This(() =>
				new int[1].Satisfies(a => a != null && a.Length == 2)).Should().Throw<AssertException>().Exception;

			var lines = e.Message.Lines();
			lines[0].Should().Be.EqualTo("[0] Should Satisfy (a => a.Length == 2)");
		}

		[Test]
		public void WhenValueTypeComparison_ShouldShowComparedValues()
		{
			var e = Executing.This(() =>
				(new DateTime(1840,1,1)).Satisfies(a => a.Year == 1940)).Should().Throw<AssertException>().Exception;

			var lines = e.Message.Lines();
			lines.Should().Contain("Compared values was: 1840 == 1940");
		}

		[Test]
		public void WhenValueTypeComparisonForNotEquality_ShouldShowComparedValues()
		{
			var e = Executing.This(() =>
				(new DateTime(1840, 1, 1)).Satisfies(a => a.Year >= 1940)).Should().Throw<AssertException>().Exception;

			var lines = e.Message.Lines();
			lines.Should().Contain("Compared values was: 1840 >= 1940");
		}

		private class MyDynamicClass
		{
			private int i;
			public int Next => ++i;
        }

		[Test]
		public void WhenPropertyChangesItsValueThenMessageIsCoherent()
		{
			var myClass = new MyDynamicClass();
			var e = Executing.This(() =>
				myClass.Next.Satisfies(a => a < 0)).Should().Throw<AssertException>().Exception;

			var lines = e.Message.Lines();
			lines.Should().Contain("1 Should Satisfy (a => a < 0)");
			lines.Should().Contain("Compared values was: 1 < 0");
		}

		//[Test]
		//[Explicit("The exception does not show the actual value of the variable.")]
		//public void WhenObjectPropertyChangesItsValueThenShouldNotExecuteTheGetterMoreThenOnce()
		//{
		//	// TODO : Fix know issue
		//	var myClass = new MyDynamicClass();
		//	var e = Executing.This(() =>
		//		myClass.Satisfies(a => a.Next < 0)).Should().Throw<AssertException>().Exception;

		//	var lines = e.Message.Lines();
		//	lines.Should().Contain("Compared values was: 1 < 0");
		//}
	}
}