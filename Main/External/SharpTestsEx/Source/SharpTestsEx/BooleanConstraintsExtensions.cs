namespace SharpTestsEx
{
	public static class BooleanConstraintsExtensions
	{
		public static void Be(this IBooleanConstraints constraint, bool expected)
		{
			constraint.Be.EqualTo(expected);
		}
	}
}