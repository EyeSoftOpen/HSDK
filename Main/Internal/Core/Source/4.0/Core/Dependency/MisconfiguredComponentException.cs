namespace EyeSoft.Core
{
    using System;

    public class MisconfiguredComponentException : Exception
	{
		public MisconfiguredComponentException(string message) : base(message)
		{
		}

		public MisconfiguredComponentException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}