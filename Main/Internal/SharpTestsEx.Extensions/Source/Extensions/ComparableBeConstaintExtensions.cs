namespace EyeSoft.SharpTests.Extensions
{
	using SharpTestsEx;

	public static class ComparableBeConstaintExtensions
	{
		public static void Zero(this IComparableBeConstraints<int> constraint)
		{
			constraint.AssertionInfo.Actual.Should().Be.EqualTo(0);
		}
	}
}
