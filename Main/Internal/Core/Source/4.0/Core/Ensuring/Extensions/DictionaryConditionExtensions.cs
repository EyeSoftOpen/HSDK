namespace EyeSoft
{
	using System;
	using System.Collections.Generic;

	public static class DictionaryConditionExtensions
	{
		public static IEnsuring Containing<TKey, TValue>(this IBooleanCondition<IDictionary<TKey, TValue>> condition, TKey key)
		{
			Func<string, ArgumentException> argumentException =
				argumentName =>
					new ArgumentException("Key not found.", argumentName, new KeyNotFoundException());

			return
				Condition
					.On(condition)
					.Expression(value => value.ContainsKey(key))
					.ArgumentVerifiedException(argumentException)
					.VerifiedException(() => new KeyNotFoundException())
					.ArgumentNotVerifiedException(argumentException)
					.NotVerifiedException(() => new KeyNotFoundException())
					.Evaluate();
		}
	}
}