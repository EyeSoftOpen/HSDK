namespace EyeSoft
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public static class EnumerableConditionExtensions
	{
		public static IEnsuring Empty<T>(this IBooleanCondition<IEnumerable<T>> condition)
		{
			return
				Condition
					.On(condition)
					.Expression(value => !value.Any())
					.ArgumentVerifiedException(argumentName => new ArgumentException("The collection is not empty.", argumentName))
					.VerifiedException(() => new CollectionIsNotEmptyException())
					.ArgumentNotVerifiedException(argumentName => new ArgumentException("The collection is empty.", argumentName))
					.NotVerifiedException(() => new CollectionIsEmptyException())
					.Evaluate();
		}

		public static IEnsuring Containing<T>(this IBooleanCondition<IEnumerable<T>> condition, T element)
		{
			const string VerifiedMessage = "The collection already contains the element.";
			const string NofVerifiedMessage = "The collection does not contain the element.";

			return
				Condition
					.On(condition)
					.Expression(value => value.Contains(element))
					.ArgumentVerifiedException(argumentName => new ArgumentException(VerifiedMessage, argumentName))
					.VerifiedException(() => new ArgumentException(VerifiedMessage))
					.ArgumentNotVerifiedException(argumentName => new ArgumentException(NofVerifiedMessage, argumentName))
					.NotVerifiedException(() => new ArgumentException(NofVerifiedMessage))
					.Evaluate();
		}
	}
}