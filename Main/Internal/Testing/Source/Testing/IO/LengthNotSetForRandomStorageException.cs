namespace EyeSoft.Testing.IO
{
	using System;

	public class LengthNotSetForRandomStorageException
		: Exception
	{
		private readonly string message;

		public LengthNotSetForRandomStorageException(string message)
		{
			this.message = message;
		}

		public override string Message
		{
			get
			{
				return message;
			}
		}
	}
}