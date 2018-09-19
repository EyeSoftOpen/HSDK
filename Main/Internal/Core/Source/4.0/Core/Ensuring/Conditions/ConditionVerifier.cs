namespace EyeSoft
{
	using System;

	using EyeSoft.Extensions;
	using EyeSoft.Implementations;

	internal class ConditionVerifier<T> :
		IConditionVerifierExpression<T>,
		IConditionVerifierArgumentVerifiedException<T>,
		IConditionVerifierVerifiedException<T>,
		IConditionVerifierArgumentNotVerifiedException<T>, IConditionVerifierEvaluate<T>,
		IConditionVerifierNotVerifiedException<T>
	{
		private readonly IBooleanCondition<T> condition;

		private Func<T, bool> expression;

		private Func<object> notVerifiedException;

		private Func<string, object> argumentVerifiedException;

		private Func<object> verifiedException;

		private Func<string, object> argumentNotVerifiedException;

		public ConditionVerifier(IBooleanCondition<T> condition)
		{
			this.condition = condition;
		}

		IConditionVerifierArgumentVerifiedException<T> IConditionVerifierExpression<T>.Expression(Func<T, bool> expression)
		{
			this.expression = expression;
			return this;
		}

		IConditionVerifierVerifiedException<T>
			IConditionVerifierArgumentVerifiedException<T>.ArgumentVerifiedException<TArgumentException>(
				Func<string, TArgumentException> argumentVerifiedException)
		{
			this.argumentVerifiedException = argumentVerifiedException;
			return this;
		}

		IConditionVerifierArgumentNotVerifiedException<T>
			IConditionVerifierVerifiedException<T>.VerifiedException<TExcpetion>(
				Func<TExcpetion> verifiedException)
		{
			this.verifiedException = verifiedException;
			return this;
		}

		IConditionVerifierNotVerifiedException<T>
			IConditionVerifierArgumentNotVerifiedException<T>.ArgumentNotVerifiedException<TArgumentException>(
				Func<string, TArgumentException> argumentNotVerifiedException)
		{
			this.argumentNotVerifiedException = argumentNotVerifiedException;
			return this;
		}

		IConditionVerifierEvaluate<T>
			IConditionVerifierNotVerifiedException<T>.NotVerifiedException<TException>(Func<TException> notVerifiedException)
		{
			this.notVerifiedException = notVerifiedException;
			return this;
		}

		public IEnsuring Evaluate()
		{
			var concreteCondition = condition.Convert<ConditionTree<T>>();

			var conditionVerified = expression(concreteCondition.Value);

			if (concreteCondition.IsNegated)
			{
				if (conditionVerified)
				{
					ThrowException(
						concreteCondition,
						argumentNotVerifiedException,
						notVerifiedException);
				}

				return EnsuringWrapper.Create();
			}

			if (!conditionVerified)
			{
				ThrowException(
					concreteCondition,
					argumentVerifiedException,
					verifiedException);
			}

			return EnsuringWrapper.Create();
		}

		private void ThrowException(
			ConditionTree<T> concreteCondition,
			Func<string, object> argumentException,
			Func<object> exception)
		{
			if (concreteCondition.IsArgument)
			{
				argumentException(concreteCondition.ArgumentName)
					.Convert<Exception>().Throw();
			}

			ConditionalException
				.Throw(exception().Convert<Exception>(), concreteCondition.Exception);
		}
	}
}