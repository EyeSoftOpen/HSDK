namespace EyeSoft
{
	using System;

	using EyeSoft.Implementations;

	public static class LongConditionExtensions
	{
		public static IEnsuring GreaterThan(this IBooleanCondition<long> condition, long value)
		{
			var concreteCondition = condition as ConditionTree<long>;

			if (!concreteCondition.IsNegated)
			{
				if (concreteCondition.Value <= value)
				{
					ConditionalException
						.Throw(new ArgumentOutOfRangeException(), concreteCondition.Exception);
				}
			}

			return EnsuringWrapper.Create();
		}

		public static IEnsuring GreaterThanZero(this IBooleanCondition<long> condition)
		{
			return
				GreaterThan(condition, 0);
		}
	}
}