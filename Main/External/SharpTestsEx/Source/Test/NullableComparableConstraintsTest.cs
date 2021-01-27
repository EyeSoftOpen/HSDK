namespace SharpTestsEx.Test
{
    using NUnit.Framework;

    public class NullableComparableConstraintsTest
	{
		[Test]
		public void ShouldWork()
		{
			int? nullable = null;
			nullable.Should().Not.Have.Value();

			nullable = 5;
			nullable.Should().Have.Value();
		}

		[Test]
		public void NegationOfDefaultBehaviuor()
		{
			int? nullable = 5;
			nullable.Should().Not.Be.GreaterThan(6);
		}

		[Test]
		public void CompareTwoNullable()
		{
			int? less = 5;
			int? greater = 6;
			greater.Should().Be.GreaterThan(less);
			less = null;
			greater.Should().Be.GreaterThan(less);
			greater = null;
			greater.Should().Be.EqualTo(less);
		}
	}
}