namespace EyeSoft.Extensions
{
	using System;

	public static class BooleanExtensions
	{
		public static void OnTrueExecute(this IObjectExtender<bool> value, Action action)
		{
			if (!value.Instance)
			{
				return;
			}

			action();
		}
	}
}