namespace EyeSoft
{
	using System;

	using EyeSoft.Extensions;
	using EyeSoft.Implementations;

	public static class TypeConditionExtensions
	{
		public static IEnsuring Interface(this IBooleanCondition<Type> condition)
		{
			var concreteCondition = condition.Convert<ConditionTree<Type>>();

			if (concreteCondition.IsNegated)
			{
				if (concreteCondition.Value.IsInterface)
				{
					if (concreteCondition.IsArgument)
					{
						var defaultException = new ArgumentException("The type is not an interface.", concreteCondition.ArgumentName);

						ConditionalException
							.Throw(defaultException, concreteCondition.Exception);
					}

					ConditionalException
						.Throw(new TypeIsNotInterfaceException(), concreteCondition.Exception);
				}
			}
			else
			{
				if (!concreteCondition.Value.IsInterface)
				{
					if (concreteCondition.IsArgument)
					{
						var defaultException = new ArgumentException("The type is an interface.", concreteCondition.ArgumentName);

						ConditionalException
							.Throw(defaultException, concreteCondition.Exception);
					}

					ConditionalException
						.Throw(new TypeIsInterfaceException(), concreteCondition.Exception);
				}
			}

			return EnsuringWrapper.Create();
		}

		public static IEnsuring Enumerable(this IBooleanCondition<Type> condition)
		{
			var concreteCondition = condition.Convert<ConditionTree<Type>>();

			var isEnumerable = concreteCondition.Value.IsEnumerable();

			if (!concreteCondition.IsNegated)
			{
				if (!isEnumerable)
				{
					if (concreteCondition.IsArgument)
					{
						var defaultException = new ArgumentException("The type is not enumerable.", concreteCondition.ArgumentName);

						ConditionalException
							.Throw(defaultException, concreteCondition.Exception);
					}

					ConditionalException
						.Throw(new TypeIsNotEnumerableException(), concreteCondition.Exception);
				}
			}
			else
			{
				if (isEnumerable)
				{
					if (concreteCondition.IsArgument)
					{
						var defaultException = new ArgumentException("The type is enumerable.", concreteCondition.ArgumentName);

						ConditionalException
							.Throw(defaultException, concreteCondition.Exception);
					}

					ConditionalException
						.Throw(new TypeIsEnumerableException(), concreteCondition.Exception);
				}
			}

			return EnsuringWrapper.Create();
		}
	}
}