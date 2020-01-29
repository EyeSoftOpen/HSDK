using System;
using NUnit.Framework;
using SharpTestsEx.ExtensionsImpl;

namespace SharpTestsEx.Tests
{
	
	public class ComparableConstraintsFixture
	{
		[Test]
		public void ShouldWork()
		{
			DateTime.Today.Should().Be.LessThan(DateTime.Now);
			(5.0).Should().Be.EqualTo((double)5);
			((uint)10).Should().Be.GreaterThan((short)4);
			5.Should().Be.LessThan(10);
			(23.0m).Should().Be.LessThan(double.MaxValue);

			int? negableInt = 10;
			negableInt.Should().Be.EqualTo(10);
			5.Should().Be.LessThan(negableInt);
			11.Should().Be.GreaterThan(negableInt);

			// Example
			negableInt.HasValue.Should().Be.True();
		}

		private const string customTitle = "my title";
		private static void AssertUsingCustomTitle(Action assertion)
		{
			try
			{
				assertion();
			}
			catch (AssertException ae)
			{
				StringAssert.Contains(customTitle, ae.Message);
			}
		}

		[Test]
		public void ShouldWorkForShort()
		{
			var minvalue = short.MinValue;
			var maxvalue = short.MaxValue;

			minvalue.Should().Be.LessThan(maxvalue);
			maxvalue.Should().Be.GreaterThan(minvalue);
			minvalue.Should().Be.EqualTo(minvalue);
			minvalue.Should().Be.LessThanOrEqualTo(minvalue);
			minvalue.Should().Be.LessThanOrEqualTo(minvalue + 1);
			maxvalue.Should().Be.GreaterThanOrEqualTo(maxvalue);
			maxvalue.Should().Be.GreaterThanOrEqualTo(maxvalue - 1);
			minvalue.Should().Be.IncludedIn(minvalue, maxvalue);
			minvalue.Should().Not.Be.IncludedIn(minvalue + 1, maxvalue);
			maxvalue.Should().Be.IncludedIn(minvalue, maxvalue);

			AssertUsingCustomTitle(() => minvalue.Should(customTitle).Be.EqualTo(maxvalue));
			AssertUsingCustomTitle(() => minvalue.Should(customTitle).Not.Be.LessThan(minvalue).And.Be.EqualTo(maxvalue));
		}

		[Test]
		public void WhenNotIncludedThenShouldThrows()
		{
			var minvalue = short.MinValue;
			var maxvalue = short.MaxValue;
			minvalue.Executing(a=>  a.Should().Be.IncludedIn(minvalue + 1, maxvalue)).Throws<AssertException>();
		}

		[Test]
		public void ShouldWorkForInt()
		{
			var minvalue = int.MinValue;
			var maxvalue = int.MaxValue;

			minvalue.Should().Be.LessThan(maxvalue);
			maxvalue.Should().Be.GreaterThan(minvalue);
			minvalue.Should().Be.EqualTo(minvalue);
			minvalue.Should().Be.LessThanOrEqualTo(minvalue);
			minvalue.Should().Be.LessThanOrEqualTo(minvalue + 1);
			maxvalue.Should().Be.GreaterThanOrEqualTo(maxvalue);
			maxvalue.Should().Be.GreaterThanOrEqualTo(maxvalue - 1);
			minvalue.Should().Be.IncludedIn(minvalue, maxvalue);
			(minvalue + 1).Should().Be.IncludedIn(minvalue, maxvalue);
			maxvalue.Should().Be.IncludedIn(minvalue, maxvalue);

			AssertUsingCustomTitle(() => minvalue.Should(customTitle).Be.EqualTo(maxvalue));
			AssertUsingCustomTitle(() => minvalue.Should(customTitle).Not.Be.LessThan(minvalue).And.Be.EqualTo(maxvalue));
		}

		[Test]
		public void ShouldWorkForLong()
		{
			var minvalue = long.MinValue;
			var maxvalue = long.MaxValue;

			minvalue.Should().Be.LessThan(maxvalue);
			maxvalue.Should().Be.GreaterThan(minvalue);
			minvalue.Should().Be.EqualTo(minvalue);
			minvalue.Should().Be.LessThanOrEqualTo(minvalue);
			minvalue.Should().Be.LessThanOrEqualTo(minvalue + 1);
			maxvalue.Should().Be.GreaterThanOrEqualTo(maxvalue);
			maxvalue.Should().Be.GreaterThanOrEqualTo(maxvalue - 1);
			minvalue.Should().Be.IncludedIn(minvalue, maxvalue);
			(minvalue + 1).Should().Be.IncludedIn(minvalue, maxvalue);
			maxvalue.Should().Be.IncludedIn(minvalue, maxvalue);

			AssertUsingCustomTitle(() => minvalue.Should(customTitle).Be.EqualTo(maxvalue));
			AssertUsingCustomTitle(() => minvalue.Should(customTitle).Not.Be.LessThan(minvalue).And.Be.EqualTo(maxvalue));
		}

		[Test]
		public void ShouldWorkForUShort()
		{
			var minvalue = ushort.MinValue;
			var maxvalue = ushort.MaxValue;

			minvalue.Should().Be.LessThan(maxvalue);
			maxvalue.Should().Be.GreaterThan(minvalue);
			minvalue.Should().Be.EqualTo(minvalue);
			minvalue.Should().Be.LessThanOrEqualTo(minvalue);
			minvalue.Should().Be.LessThanOrEqualTo(minvalue + 1);
			maxvalue.Should().Be.GreaterThanOrEqualTo(maxvalue);
			maxvalue.Should().Be.GreaterThanOrEqualTo(maxvalue - 1);
			minvalue.Should().Be.IncludedIn(minvalue, maxvalue);
			(minvalue + 1).Should().Be.IncludedIn(minvalue, maxvalue);
			maxvalue.Should().Be.IncludedIn(minvalue, maxvalue);

			AssertUsingCustomTitle(() => minvalue.Should(customTitle).Be.EqualTo(maxvalue));
			AssertUsingCustomTitle(() => minvalue.Should(customTitle).Not.Be.LessThan(minvalue).And.Be.EqualTo(maxvalue));
		}

		[Test]
		public void ShouldWorkForUInt()
		{
			var minvalue = uint.MinValue;
			var maxvalue = uint.MaxValue;

			minvalue.Should().Be.LessThan(maxvalue);
			maxvalue.Should().Be.GreaterThan(minvalue);
			minvalue.Should().Be.EqualTo(minvalue);
			minvalue.Should().Be.LessThanOrEqualTo(minvalue);
			minvalue.Should().Be.LessThanOrEqualTo(minvalue + 1);
			maxvalue.Should().Be.GreaterThanOrEqualTo(maxvalue);
			maxvalue.Should().Be.GreaterThanOrEqualTo(maxvalue - 1);
			minvalue.Should().Be.IncludedIn(minvalue, maxvalue);
			(minvalue + 1).Should().Be.IncludedIn(minvalue, maxvalue);
			maxvalue.Should().Be.IncludedIn(minvalue, maxvalue);

			AssertUsingCustomTitle(() => minvalue.Should(customTitle).Be.EqualTo(maxvalue));
			AssertUsingCustomTitle(() => minvalue.Should(customTitle).Not.Be.LessThan(minvalue).And.Be.EqualTo(maxvalue));
		}

		[Test]
		public void ShouldWorkForULong()
		{
			var minvalue = ulong.MinValue;
			var maxvalue = ulong.MaxValue;

			minvalue.Should().Be.LessThan(maxvalue);
			maxvalue.Should().Be.GreaterThan(minvalue);
			minvalue.Should().Be.EqualTo(minvalue);
			minvalue.Should().Be.LessThanOrEqualTo(minvalue);
			minvalue.Should().Be.LessThanOrEqualTo(minvalue + 1);
			maxvalue.Should().Be.GreaterThanOrEqualTo(maxvalue);
			maxvalue.Should().Be.GreaterThanOrEqualTo(maxvalue - 1);
			minvalue.Should().Be.IncludedIn(minvalue, maxvalue);
			(minvalue + 1).Should().Be.IncludedIn(minvalue, maxvalue);
			maxvalue.Should().Be.IncludedIn(minvalue, maxvalue);

			AssertUsingCustomTitle(() => minvalue.Should(customTitle).Be.EqualTo(maxvalue));
			AssertUsingCustomTitle(() => minvalue.Should(customTitle).Not.Be.LessThan(minvalue).And.Be.EqualTo(maxvalue));
		}

		[Test]
		public void ShouldWorkForFloat()
		{
			var minvalue = float.MinValue;
			var maxvalue = float.MaxValue;

			minvalue.Should().Be.LessThan(maxvalue);
			maxvalue.Should().Be.GreaterThan(minvalue);
			minvalue.Should().Be.EqualTo(minvalue);
			minvalue.Should().Be.LessThanOrEqualTo(minvalue);
			minvalue.Should().Be.LessThanOrEqualTo(minvalue + 1);
			maxvalue.Should().Be.GreaterThanOrEqualTo(maxvalue);
			maxvalue.Should().Be.GreaterThanOrEqualTo(maxvalue - 1);
			minvalue.Should().Be.IncludedIn(minvalue, maxvalue);
			(minvalue + 1).Should().Be.IncludedIn(minvalue, maxvalue);
			maxvalue.Should().Be.IncludedIn(minvalue, maxvalue);

			AssertUsingCustomTitle(() => minvalue.Should(customTitle).Be.EqualTo(maxvalue));
			AssertUsingCustomTitle(() => minvalue.Should(customTitle).Not.Be.LessThan(minvalue).And.Be.EqualTo(maxvalue));
		}

		[Test]
		public void ShouldWorkForDouble()
		{
			var minvalue = double.MinValue;
			var maxvalue = double.MaxValue;

			minvalue.Should().Be.LessThan(maxvalue);
			maxvalue.Should().Be.GreaterThan(minvalue);
			minvalue.Should().Be.EqualTo(minvalue);
			minvalue.Should().Be.LessThanOrEqualTo(minvalue);
			minvalue.Should().Be.LessThanOrEqualTo(minvalue + 1);
			maxvalue.Should().Be.GreaterThanOrEqualTo(maxvalue);
			maxvalue.Should().Be.GreaterThanOrEqualTo(maxvalue - 1);
			minvalue.Should().Be.IncludedIn(minvalue, maxvalue);
			(minvalue + 1).Should().Be.IncludedIn(minvalue, maxvalue);
			maxvalue.Should().Be.IncludedIn(minvalue, maxvalue);

			AssertUsingCustomTitle(() => minvalue.Should(customTitle).Be.EqualTo(maxvalue));
			AssertUsingCustomTitle(() => minvalue.Should(customTitle).Not.Be.LessThan(minvalue).And.Be.EqualTo(maxvalue));
		}

		[Test]
		public void ShouldWorkForDecimal()
		{
			var minvalue = decimal.MinValue;
			var maxvalue = decimal.MaxValue;

			minvalue.Should().Be.LessThan(maxvalue);
			maxvalue.Should().Be.GreaterThan(minvalue);
			minvalue.Should().Be.EqualTo(minvalue);
			minvalue.Should().Be.LessThanOrEqualTo(minvalue);
			minvalue.Should().Be.LessThanOrEqualTo(minvalue + 1);
			maxvalue.Should().Be.GreaterThanOrEqualTo(maxvalue);
			maxvalue.Should().Be.GreaterThanOrEqualTo(maxvalue - 1);
			minvalue.Should().Be.IncludedIn(minvalue, maxvalue);
			(minvalue + 1).Should().Be.IncludedIn(minvalue, maxvalue);
			maxvalue.Should().Be.IncludedIn(minvalue, maxvalue);

			AssertUsingCustomTitle(() => minvalue.Should(customTitle).Be.EqualTo(maxvalue));
			AssertUsingCustomTitle(() => minvalue.Should(customTitle).Not.Be.LessThan(minvalue).And.Be.EqualTo(maxvalue));
		}

		[Test]
		public void ShouldWorkForDateTime()
		{
			var minvalue = DateTime.MinValue;
			var maxvalue = DateTime.MaxValue;

			minvalue.Should().Be.LessThan(maxvalue);
			maxvalue.Should().Be.GreaterThan(minvalue);
			minvalue.Should().Be.EqualTo(minvalue);
			minvalue.Should().Be.LessThanOrEqualTo(minvalue);
			minvalue.Should().Be.LessThanOrEqualTo(minvalue.AddDays(1));
			maxvalue.Should().Be.GreaterThanOrEqualTo(maxvalue);
			maxvalue.Should().Be.GreaterThanOrEqualTo(maxvalue.AddDays(-1));
			minvalue.Should().Be.IncludedIn(minvalue, maxvalue);
			(minvalue.AddDays(1)).Should().Be.IncludedIn(minvalue, maxvalue);
			maxvalue.Should().Be.IncludedIn(minvalue, maxvalue);

			AssertUsingCustomTitle(() => minvalue.Should(customTitle).Be.EqualTo(maxvalue));
			AssertUsingCustomTitle(() => minvalue.Should(customTitle).Not.Be.LessThan(minvalue).And.Be.EqualTo(maxvalue));
		}

		[Test]
		public void WhenDateTimeWithDifferenceInTicksThenMessageShowDifference()
		{
			var now = DateTime.Now;
			var nowWithLittleDifference	 = now.AddTicks(2);
			
			Executing.This(() => now.Should().Be(nowWithLittleDifference)).Should().Throw<AssertException>()
				.And.Exception.Message.Should().Contain(now.TimeOfDay.ToString()).And.Contain(nowWithLittleDifference.TimeOfDay.ToString());
		}

		[Test]
		public void ShouldWorkUsingCustomMessage()
		{
			var title = "my title";

			DateTime? negableInt = null;
			try
			{
				negableInt.Should(title).Be.EqualTo(DateTime.Today);
			}
			catch (AssertException ae)
			{
				StringAssert.Contains(title, ae.Message);
			}
		}

		[Test]
		public void AndChainShouldWork()
		{
			5.Should().Be.GreaterThan(3).And.Not.Be.EqualTo(4);
			1.Should().Not.Be.GreaterThan(3).And.Not.Be.EqualTo(4);
		}

		[Test]
		public void BeShortCut()
		{
			5.Should().Be(5);
			5.Executing(x => x.Should().Not.Be(5)).Throws<AssertException>();
			5.Should().Not.Be(6);
			5.Executing(x => x.Should().Be(6)).Throws<AssertException>();

			int? nullableInt = 10;
			nullableInt.Should().Be(10);
			nullableInt.Executing(x => x.Should().Not.Be(10)).Throws<AssertException>();
		}

		[Test]
		public void WhenCompareNumbersThenShouldWorkAsInNet()
		{
			int x = 1;
			double y = 1.0;
			(NumericsComparer.Default.Compare(x,y) == 0).Should().Be(x == y);
			1.Executing(a => a.Should().Be(1.0d)).NotThrows();
		}

		[Test]
		public void WhenCompareNumbersBoxingThenShouldWorkAsInNet()
		{
			// Issue #5844
			int x = 1;
			double y = 1.0;
			object b = x;
			if(b.Equals(y)) // <== pure .NET comparison
			{
				b.Executing(a => a.Should().Be(y)).NotThrows();
			}
			else
			{
				b.Executing(a => a.Should().Be(y)).Throws<AssertException>()
					.And.ValueOf.Message.Should().Contain("System.Int32").And.Contain("System.Double");
			}
		}
	}
}