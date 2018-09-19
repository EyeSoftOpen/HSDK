namespace EyeSoft
{
	using EyeSoft.Implementations;

	public static class Ensure
	{
		public static INamedCondition<T> That<T>(T value)
		{
			return new ConditionTree<T>(value);
		}
	}
}