namespace EyeSoft
{
	using System;

	internal static class ConditionalException
	{
		public static void Throw(Exception defaultException, Exception customException)
		{
			if (customException != null)
			{
				customException.Throw();
			}

			defaultException.Throw();
		}
	}
}