namespace EyeSoft.Transfer.Service.Test.Helpers
{
	using System;

	internal static class BooleanConditionExtensions
	{
		public static void Throw<TException>(this BooleanCondition condition, TException exception)
			where TException : Exception
		{
			if (!condition.Value)
			{
				return;
			}

			throw exception;
		}
	}
}