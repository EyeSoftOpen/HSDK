namespace EyeSoft
{
	public static class Condition
	{
		public static IConditionVerifierExpression<T> On<T>(IBooleanCondition<T> condition)
		{
			return new ConditionVerifier<T>(condition);
		}
	}
}