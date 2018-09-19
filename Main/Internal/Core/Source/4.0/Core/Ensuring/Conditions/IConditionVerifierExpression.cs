namespace EyeSoft
{
	using System;

	public interface IConditionVerifierExpression<T>
	{
		IConditionVerifierArgumentVerifiedException<T> Expression(Func<T, bool> expression);
	}
}